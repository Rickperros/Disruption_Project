using UnityEngine;

[System.Serializable]
public class KinematicState : MonoBehaviour
{
    #region Constraints

    public float m_maxLinearAcceleration = 0f;
    [Tooltip("In Degrees")]
    public float m_maxAngularAcceleration = 0f;

    public float m_maxLinearSpeed = 0f;
    public float m_maxAngularSpeed = 0f;

    #endregion

    #region Kinematic state

    public float m_orientation = 0f;
    public float m_angularSpeed = 0f;
    public Vector3 m_position = Vector3.zero;
    public Vector3 m_linearVelocity = Vector3.zero;

    public float m_stoppedPrecision = 0.001f;

    #endregion

    private void Start()
    {
        m_position = transform.position;
        m_orientation = transform.eulerAngles.y;
    }

    public bool IsStopped()
    {
        return Mathf.Abs(m_linearVelocity.magnitude) <= m_stoppedPrecision;
    }
}
