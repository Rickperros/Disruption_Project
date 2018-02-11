using Steerings;
using UnityEngine;

[RequireComponent(typeof(Interpose))]
[RequireComponent(typeof(Seek))]
[RequireComponent(typeof(WanderAroundPlusAvoid))]
[RequireComponent(typeof(ChargerFightFSM))]
public class ChargerBlackboard : MonoBehaviour
{
    [HideInInspector] public KinematicState m_kinematicState;
    [HideInInspector] public Interpose m_interposeSteering;
    [HideInInspector] public WanderAroundPlusAvoid m_wanderSteering;
    [HideInInspector] public Seek m_seekSteering;
    [HideInInspector] public ChargerFightFSM m_fightFSM;

    public Transform m_player;
    public Transform m_wanderAnchor;
    public Transform m_GuardPoint;
    public Transform m_lastPlayerPos;

    public float m_alertRadius = 8f;
    public float m_attackRadius = 5f;
    public float m_closeEnoughRadius;
    public float m_attackCooldown = 5f;


    private void Awake()
    {
        m_kinematicState = GetComponent<KinematicState>();
        m_interposeSteering = GetComponent<Interpose>();
        m_wanderSteering = GetComponent<WanderAroundPlusAvoid>();
        m_seekSteering = GetComponent<Seek>();
        m_fightFSM = GetComponent<ChargerFightFSM>();

        m_interposeSteering.enabled = false;
        m_wanderSteering.enabled = false;
        m_seekSteering.enabled = false;
        m_fightFSM.enabled = false;


        m_player = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
