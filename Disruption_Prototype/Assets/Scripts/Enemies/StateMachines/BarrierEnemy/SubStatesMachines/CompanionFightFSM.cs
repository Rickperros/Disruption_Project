using Steerings;
using UnityEngine;
using StateMachines;

[RequireComponent(typeof(LaserEnemyBlackboard))]
[RequireComponent(typeof(KeepPositionPlusAvoidObstacles))]
public class CompanionFightFSM : MonoBehaviour, IFSMState
{
    public enum EState { INITIAL, HOLD_BARRIER, DASH }

    private LaserEnemyBlackboard m_blackboard;
    private LineRenderer m_laser;
    private KeepPositionPlusAvoidObstacles m_steering;

    private EState m_currentState;
    private EState m_nextState;
    private Timer m_attackTimer;
    private Timer m_postAttackTimer;

    void Awake()
    {
        m_currentState = EState.INITIAL;

        m_blackboard = GetComponent<LaserEnemyBlackboard>();
        m_laser = GetComponent<LineRenderer>();
        m_steering = GetComponent<KeepPositionPlusAvoidObstacles>();

        m_laser.enabled = false;
        m_steering.enabled = false;

        m_attackTimer = new Timer(m_blackboard.m_companionAttackCooldown);
        m_postAttackTimer = new Timer(m_blackboard.m_companionAttackWaitTime);
    }

    public void OnEnter()
    {
        m_currentState = EState.INITIAL;
        m_nextState = EState.HOLD_BARRIER;
        m_attackTimer.ForceUpdate();
        m_steering.enabled = false;
        m_laser.enabled = false;
        this.enabled = true;
    }

    public void OnExit()
    {
        m_laser.enabled = false;
        m_steering.enabled = false;
        this.enabled = false;
    }

    public void Update()
    {
        switch(m_currentState)
        {
            case EState.INITIAL:
                m_nextState = EState.HOLD_BARRIER;
                ChangeState();
                break;
            case EState.HOLD_BARRIER:

                if(m_attackTimer.CheckTimer())
                {
                    m_nextState = EState.DASH;
                    ChangeState();
                    break;
                }
                HoldBarrier();
                break;
            case EState.DASH:

                if(m_postAttackTimer.CheckTimer())
                {
                    m_nextState = EState.HOLD_BARRIER;
                    ChangeState();
                    break;
                }
                HoldBarrier();
                break;
        }
    }

    public void ChangeState()
    {
        //Exit
        switch(m_currentState)
        {
            case EState.INITIAL:
                break;
            case EState.HOLD_BARRIER:
                m_steering.enabled = false;
                m_laser.enabled = false;
                break;
            case EState.DASH:
                m_steering.enabled = false;
                m_laser.enabled = false;
                break;
        }

        //Enter
        switch(m_nextState)
        {
            case EState.INITIAL:
                break;
            case EState.HOLD_BARRIER:

                HoldBarrier();
                m_laser.enabled = true;
                m_steering.enabled = true;
                m_steering.m_useArrive = true;
                m_steering.m_keepPositionInfo.m_requiredAngle = m_blackboard.m_companionFightHoldAngle;
                m_steering.m_keepPositionInfo.m_requiredDistance = m_blackboard.m_companionFightDistance;
                m_attackTimer.ForceUpdate();
                break;
            case EState.DASH:

                HoldBarrier();
                m_laser.enabled = true;
                m_steering.enabled = true;
                m_steering.m_useArrive = true;
                m_steering.m_keepPositionInfo.m_requiredAngle = m_blackboard.m_companionFightAttackAngle;
                m_steering.m_keepPositionInfo.m_requiredDistance = m_blackboard.m_companionFightDistance;
                m_postAttackTimer.ForceUpdate();
                break;
        }

        m_currentState = m_nextState;
    }

    private void HoldBarrier()
    {
        if (!m_blackboard.m_emisionFont)
            return;

        Ray l_ray = new Ray(transform.position, (m_blackboard.m_companion.position - this.transform.position).normalized);
        RaycastHit l_hitInfo;

        if (Physics.Raycast(l_ray, out l_hitInfo))
        {
            if (l_hitInfo.collider.transform == m_blackboard.m_companion)
            {
                m_laser.SetPosition(0, this.transform.position);
                m_laser.SetPosition(1, l_hitInfo.point);
            }
        }
    }
}

