using Utils;
using UnityEngine;

namespace Steerings
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(KinematicState))]
    public class SteeringBehaviour : MonoBehaviour
    {
        public SAlign m_alignInfo;
        public bool m_ignoreHeight = true;
        protected KinematicState m_ownKS;

        protected static Transform SURROGATE_TARGET = null;
        protected static SteeringOutput NULL_STEERING;

        protected CharacterController m_characterController;

        protected virtual void Awake()
        {
            m_ownKS = GetComponent<KinematicState>();
            m_characterController = GetComponent<CharacterController>();

            if (SURROGATE_TARGET == null)
            {
                SURROGATE_TARGET = new GameObject("surrogate target").transform;
                SURROGATE_TARGET.gameObject.AddComponent<KinematicState>();
            }

            if (NULL_STEERING == null)
            {
                NULL_STEERING = new SteeringOutput();
                NULL_STEERING.m_linearActive = false;
            }
        }

        void Update()
        {
            //Get Accelerations
            SteeringOutput l_steering = GetSteering();

            //Stop if there is no acceleration
            if (l_steering == null)
            {
                m_ownKS.m_linearVelocity = Vector3.zero;
                m_ownKS.m_angularSpeed = 0f;
                return;
            }

            float dt = Time.deltaTime;

            //Apply linear accelerations
            if (l_steering.m_linearActive)
            {
                if (m_ignoreHeight)
                    l_steering.m_linearAcceleration.y = 0f;

                m_ownKS.m_linearVelocity = m_ownKS.m_linearVelocity + l_steering.m_linearAcceleration * dt;
                m_ownKS.m_linearVelocity = MathExtent.Clip(m_ownKS.m_linearVelocity, m_ownKS.m_maxLinearSpeed);

                m_characterController.Move(m_ownKS.m_linearVelocity * dt + 0.5f * l_steering.m_linearAcceleration * dt * dt);
                m_ownKS.m_position += m_ownKS.m_linearVelocity * dt + 0.5f * l_steering.m_linearAcceleration * dt * dt;
            }
            else
                m_ownKS.m_linearVelocity = Vector3.zero;

            //Apply angular Accelerations
            if (l_steering.m_angularActive)
            {
                m_ownKS.m_angularSpeed = m_ownKS.m_angularSpeed + l_steering.m_angularAcceleration * dt;
                m_ownKS.m_angularSpeed = MathExtent.Clip(m_ownKS.m_angularSpeed, m_ownKS.m_maxAngularSpeed, true);

                m_ownKS.m_orientation += m_ownKS.m_angularSpeed * dt + 0.5f * l_steering.m_angularAcceleration * dt * dt;
                transform.rotation = Quaternion.Euler(0f, m_ownKS.m_orientation, 0f);
            }
            else
                m_ownKS.m_angularSpeed = 0f;

        }

        protected virtual SteeringOutput GetSteering()
        {
            return NULL_STEERING;
        }

        protected virtual void ApplyLWYGI(SteeringOutput steering)
        {
            if (m_ownKS.m_linearVelocity.magnitude > 0.001f)
            {
                transform.rotation = Quaternion.Euler(0f, MathExtent.VectorToAngle(m_ownKS.m_linearVelocity), 0f);
                m_ownKS.m_orientation = transform.rotation.eulerAngles.y;
            }
            steering.m_angularActive = false;
        }

        protected virtual void ApplyLWYG(SteeringOutput steering)
        {
            if (m_ownKS.m_linearVelocity.magnitude > 0.001f)
            {
                SURROGATE_TARGET.transform.rotation = Quaternion.Euler(0, MathExtent.VectorToAngle(m_ownKS.m_linearVelocity), 0);
                m_alignInfo.m_target = SURROGATE_TARGET;
                SteeringOutput st = Align.GetSteering(m_ownKS, m_alignInfo);
                steering.m_angularAcceleration = st.m_angularAcceleration;
                steering.m_angularActive = st.m_angularActive;
            }
            else
                steering.m_angularActive = false;
        }

        public virtual void ApplyFTI(SteeringOutput steering, GameObject target)
        {

            transform.rotation = Quaternion.Euler(0, MathExtent.VectorToAngle(target.transform.position - m_ownKS.m_position) -90f, 0);
            m_ownKS.m_orientation = transform.rotation.eulerAngles.y;
            
            steering.m_angularActive = false;
        }

        public virtual void ApplyFT(SteeringOutput steering, GameObject target)
        {
            m_alignInfo.m_target = target.transform;
            SteeringOutput st = Face.GetSteering(m_ownKS, m_alignInfo);
            steering.m_angularAcceleration = st.m_angularAcceleration;
            steering.m_angularActive = st.m_angularActive;
        }

        public virtual void ApplyFacingPolicy(EFacingPolicy rotationalPolicy, SteeringOutput steering, GameObject target = null)
        {
            switch (rotationalPolicy)
            {
                case EFacingPolicy.LWYGI:
                    ApplyLWYGI(steering);
                    break;
                case EFacingPolicy.LWYG:
                    ApplyLWYG(steering);
                    break;
                case EFacingPolicy.FTI:
                    if (target == null)
                        break;
                    ApplyFTI(steering, target);
                    break;
                case EFacingPolicy.FT:
                    if (target == null)
                        break;
                    ApplyFT(steering, target);
                    break;
                case EFacingPolicy.NONE:
                    break;
            }
        }
    }
}
