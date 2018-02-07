using Utils;
using StateMachines;
using Steerings;
using UnityEngine;

[RequireComponent(typeof(LaserEnemyBlackboard))]
[RequireComponent(typeof(FightFSM))]
[RequireComponent(typeof(WanderAroundPlusAvoid))]
public class AliveRoutineFSM : MonoBehaviour, IFSMState
{
    public enum EStates { INITIAL, WANDER, FIGHT }

    private LaserEnemyBlackboard m_blackboard;
    private FightFSM m_fightState;
    private WanderAroundPlusAvoid m_wanderSteering;

    private EStates m_currentState;
    private EStates m_nextState;
    
    public void Awake()
    {
        m_blackboard = GetComponent<LaserEnemyBlackboard>();
        m_fightState = GetComponent<FightFSM>();
        m_wanderSteering = GetComponent<WanderAroundPlusAvoid>();

        m_fightState.enabled = false;
        m_wanderSteering.enabled = false;

        m_currentState = EStates.INITIAL;
    }

    public void OnEnter()
    {
        m_fightState.enabled = false;
        m_wanderSteering.enabled = false;

        m_currentState = EStates.INITIAL;
    }

    public void OnExit()
    {
        m_fightState.enabled = false;
        m_wanderSteering.enabled = false;

        m_currentState = EStates.INITIAL;
    }

    public void Update()
    {
        switch(m_currentState)
        {
            case EStates.INITIAL:
                m_nextState = EStates.WANDER;
                ChangeState();
                break;
            case EStates.WANDER:

                if(!MathExtent.IsInRange(Vector3.Distance(this.transform.position, m_blackboard.m_player.position), m_blackboard.m_alertRadius))
                {
                    m_nextState = EStates.FIGHT;
                    ChangeState();
                }
                break;
            case EStates.FIGHT:

                if (MathExtent.IsInRange(Vector3.Distance(this.transform.position, m_blackboard.m_player.position), m_blackboard.m_alertRadius))
                {
                    m_nextState = EStates.WANDER;
                    ChangeState();
                }
                break;
        }
    }

    public void ChangeState()
    {
        //Exit
        switch (m_currentState)
        {
            case EStates.INITIAL:
                break;
            case EStates.WANDER:
                m_wanderSteering.enabled = false;
                break;
            case EStates.FIGHT:
                m_fightState.OnExit();
                m_fightState.enabled = false;
                break;
        }

        //Enter
        switch (m_nextState)
        {
            case EStates.INITIAL:
                break;
            case EStates.WANDER:
                m_wanderSteering.enabled = true;
                m_wanderSteering.m_seekInfo.m_target = m_blackboard.m_wanderAnchor;
                break;
            case EStates.FIGHT:
                m_fightState.enabled = true;
                m_fightState.OnEnter();
                break;
        }

        m_currentState = m_nextState;
    }
}
