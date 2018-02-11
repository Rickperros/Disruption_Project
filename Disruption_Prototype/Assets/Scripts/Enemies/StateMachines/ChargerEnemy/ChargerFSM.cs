using StateMachines;
using Utils;
using UnityEngine;

[RequireComponent(typeof(ChargerBlackboard))]
public class ChargerFSM : MonoBehaviour, IFSMState
{
    public enum EStates {INITIAL, IDLE, FIGHT}

    private ChargerBlackboard m_blackboard;

    private EStates m_currentState;
    private EStates m_nextState;

    public void Start()
    {
        m_blackboard = GetComponent<ChargerBlackboard>();
        OnEnter();
    }
    public void OnEnter()
    {
        m_currentState = EStates.INITIAL;
        this.enabled = true;
    }

    public void OnExit()
    {
        m_nextState = EStates.INITIAL;
        ChangeState();
        this.enabled = false;
    }

    public void Update()
    {
        switch(m_currentState)
        {
            case EStates.INITIAL:
                m_nextState = EStates.IDLE;
                ChangeState();
                break;
            case EStates.IDLE:

                if(!MathExtent.IsInRange(Vector3.Distance(m_blackboard.m_player.position, this.transform.position), m_blackboard.m_alertRadius))
                {
                    m_nextState = EStates.FIGHT;
                    ChangeState();
                    break;
                }
                break;
            case EStates.FIGHT:

                if (MathExtent.IsInRange(Vector3.Distance(m_blackboard.m_player.position, this.transform.position), m_blackboard.m_alertRadius))
                {
                    m_nextState = EStates.IDLE;
                    ChangeState();
                    break;
                }
                break;
        }
    }

    public void ChangeState()
    {
        //exit
        switch (m_currentState)
        {
            case EStates.INITIAL:
                break;
            case EStates.IDLE:
                m_blackboard.m_wanderSteering.m_seekInfo.m_target = m_blackboard.m_wanderAnchor;
                m_blackboard.m_wanderSteering.enabled = false;
                break;
            case EStates.FIGHT:
                m_blackboard.m_fightFSM.OnExit();
                break;
        }

        //enter
        switch (m_nextState)
        {
            case EStates.INITIAL:
                break;
            case EStates.IDLE:
                m_blackboard.m_wanderSteering.enabled = true;
                break;
            case EStates.FIGHT:
                m_blackboard.m_fightFSM.OnEnter();
                break;
        }

        m_currentState = m_nextState;
    }
}
