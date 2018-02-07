using UnityEngine;
using Steerings;
using Utils;
using StateMachines;


[RequireComponent(typeof(LaserEnemyBlackboard))]
[RequireComponent(typeof(KeepPosition))]
public class SoloFightFSM : MonoBehaviour, IFSMState
{
    public enum EState {INITIAL, KEEP_POSITION, SHOOT}

    private LaserEnemyBlackboard m_blackboard;
    private KeepPosition m_steering;

    private EState m_currentState;
    private EState m_nextState;
    private Timer m_attackTimer;

    public void Awake()
    {
        m_steering = GetComponent<KeepPosition>();
        m_blackboard = GetComponent<LaserEnemyBlackboard>();

        m_currentState = EState.INITIAL;
        m_nextState = EState.KEEP_POSITION;

        m_attackTimer = new Timer(m_blackboard.m_soloAttackCooldown);
        m_steering.enabled = false;
    }

    public void OnEnter()
    {
        m_currentState = EState.INITIAL;
        m_nextState = EState.KEEP_POSITION;
        m_attackTimer.ForceUpdate();
        this.enabled = true;
    }

    public void OnExit()
    {
        m_steering.enabled = false;
        this.enabled = false;
    }

    public void Update()
    {
        switch(m_currentState)
        {
            case EState.INITIAL:
                m_nextState = EState.KEEP_POSITION;
                ChangeState();
                break;
            case EState.KEEP_POSITION:
                if(m_attackTimer.CheckTimer())
                {
                    m_nextState = EState.SHOOT;
                    ChangeState();
                    break;
                }
                break;
            case EState.SHOOT:

                if(MathExtent.IsInRange(Vector3.Distance(transform.position, m_blackboard.m_player.position), m_blackboard.m_soloAttackDistanceToPlayer))
                {
                    Shoot();
                    m_nextState = EState.KEEP_POSITION;
                    ChangeState();
                    break;
                }

                break;
        }
    }

    public void ChangeState()
    {
        //Exit state
        switch(m_currentState)
        {
            case EState.INITIAL:
                break;
            case EState.KEEP_POSITION:
                m_steering.m_useArrive = false;
                m_steering.enabled = false;
                    break;
            case EState.SHOOT:
                m_steering.enabled = false;
                break;
        }

        //Enter state
        switch(m_nextState)
        {
            case EState.INITIAL:
                break;
            case EState.KEEP_POSITION:

                m_steering.enabled = true;
                m_steering.m_useArrive = true;
                m_steering.m_keepPositioninfo.m_requiredDistance = m_blackboard.m_soloDistanceToPlayer;
                m_steering.m_keepPositioninfo.m_requiredAngle = m_blackboard.m_soloAngleToPlayer;
                break;
            case EState.SHOOT:

                m_steering.enabled = true;
                m_steering.m_keepPositioninfo.m_requiredDistance = m_blackboard.m_soloAttackDistanceToPlayer;
                m_steering.m_keepPositioninfo.m_requiredAngle = m_blackboard.m_soloAngleToPlayer;
                break;
        }

        m_currentState = m_nextState;
    }

    private void Shoot()
    {
        Ray l_ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.red, 3f);
        RaycastHit l_hitInfo;

        if(Physics.Raycast(l_ray, out l_hitInfo))
        {
            if(l_hitInfo.collider.tag == "Player")
            {
                Debug.Log("DIE");
            }
        }
    }
}

