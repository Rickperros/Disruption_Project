using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaivour : MonoBehaviour {

    public enum EFacingPolicy {LWYG, LWYGI, FT, FTI, NONE };

    protected KinematicState m_ownKS;

    protected static GameObject SURROGATE_TARGET = null;
    protected static SteeringOutput NULL_STEERING;

	protected virtual void Awake ()
    {
        m_ownKS = GetComponent<KinematicState>();

        if(SURROGATE_TARGET == null)
        {
            SURROGATE_TARGET = new GameObject("surrogate target");
            SURROGATE_TARGET.AddComponent<KinematicState>();
        }

        if(NULL_STEERING == null)
        {
            NULL_STEERING = new SteeringOutput();
            NULL_STEERING.m_linearActive = false;
        }
	}
	
	void Update ()
    {
        SteeringOutput l_steering = GetSteering();

        if(l_steering == null)
        {
            m_ownKS.m_linearVelocity = Vector3.zero;
            m_ownKS.m_angularSpeed = 0f;
            return;
        }

        float dt = Time.deltaTime;

        if (l_steering.m_linearActive)
        {
            m_ownKS.m_linearVelocity = m_ownKS.m_linearVelocity + l_steering.m_linearAcceleration * dt;
            m_ownKS.m_linearVelocity = Clip(m_ownKS.m_linearVelocity, m_ownKS.m_maxLinearSpeed);

            m_ownKS.m_position += m_ownKS.m_linearVelocity * dt + 0.5f * l_steering.m_linearAcceleration * dt * dt;
            transform.position = m_ownKS.m_position;
        }
        else
            m_ownKS.m_linearVelocity = Vector3.zero;

        if (l_steering.m_angularActive)
        {
            m_ownKS.m_angularSpeed = m_ownKS.m_angularSpeed + l_steering.m_angularAcceleration * dt;
            if (Mathf.Abs(m_ownKS.m_angularSpeed) > m_ownKS.m_maxAngularSpeed)
                m_ownKS.m_angularSpeed = m_ownKS.m_maxAngularSpeed * Mathf.Sign(m_ownKS.m_angularSpeed);

            m_ownKS.m_orientation += m_ownKS.m_angularSpeed * dt + 0.5f * l_steering.m_angularAcceleration * dt * dt;
            transform.rotation = Quaternion.Euler(0f, 0f, m_ownKS.m_orientation);
        }
        else
            m_ownKS.m_angularSpeed = 0f;
	}

    protected virtual SteeringOutput GetSteering()
    {
        return NULL_STEERING;
    }

    protected float Clip(float current, float max)
    {
        if (current > max)
            return max;
        else
            return current;
    }

    protected Vector3 Clip (Vector3 current, float max)
    {
        if (current.magnitude < max)
            return current.normalized * max;
        else
            return current;
    }
}
