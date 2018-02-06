using UnityEngine;

[System.Serializable]
public class SteeringOutput
{
    public bool m_linearActive = true;
    public bool m_angularActive = false;

    public float m_angularAcceleration = 0f;
    public Vector3 m_linearAcceleration = Vector3.zero;
}
