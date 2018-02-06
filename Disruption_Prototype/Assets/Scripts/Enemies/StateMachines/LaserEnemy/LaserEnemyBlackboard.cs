using UnityEngine;

public class LaserEnemyBlackboard : MonoBehaviour
{
    public Transform m_player;
    public Transform m_companion;
    public LineRenderer m_laser;
    public bool m_alive;
    public float m_soloAttackCooldown;

    private void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
