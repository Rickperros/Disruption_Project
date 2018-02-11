using UnityEngine;
using Steerings;
using StateMachines;


[RequireComponent(typeof(LaserEnemyBlackboard))]
[RequireComponent(typeof(KeepPositionPlusAvoidObstacles))]
public class SoloFightFSM : MonoBehaviour, IFSMState
{
    public enum EState {INITIAL, KEEP_POSITION}

    private LaserEnemyBlackboard m_blackboard;
    private KeepPositionPlusAvoidObstacles m_steering;

    private EState m_currentState;
    private EState m_nextState;
    private Timer m_attackTimer;

    public void Awake()
    {
        m_steering = GetComponent<KeepPositionPlusAvoidObstacles>();
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
        }

        //Enter state
        switch(m_nextState)
        {
            case EState.INITIAL:
                break;
            case EState.KEEP_POSITION:

                m_steering.enabled = true;
                m_steering.m_useArrive = true;
                m_steering.m_keepPositionInfo.m_requiredDistance = m_blackboard.m_soloDistanceToPlayer;
                m_steering.m_keepPositionInfo.m_requiredAngle = m_blackboard.m_soloAngleToPlayer;
                break;
        }

        m_currentState = m_nextState;
    }

}

