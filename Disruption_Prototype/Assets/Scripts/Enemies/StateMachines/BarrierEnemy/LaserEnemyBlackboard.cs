using UnityEngine;

[RequireComponent(typeof(KinematicState))]
public class LaserEnemyBlackboard : MonoBehaviour
{
    [HideInInspector] public KinematicState m_kineamticState;
    [HideInInspector] public LineRenderer m_laser;
    public Transform m_player;
    public Transform m_wanderAnchor;
    public Transform m_companion;

    public float m_alertRadius = 8f;

    public float m_soloAttackCooldown;
    public float m_soloAngleToPlayer = 0f;
    public float m_soloDistanceToPlayer = 3f;
    public float m_soloAttackDistanceToPlayer = 5f;

    public float m_companionFightHoldAngle = 60f;
    public float m_companionFightAttackAngle = 150f;
    public float m_companionFightDistance = 8f;
    public float m_companionAttackCooldown = 10f;
    public float m_companionAttackWaitTime = 3f;
    public bool m_emisionFont = false;

    private void Awake()
    {
        m_kineamticState = GetComponent<KinematicState>();
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
