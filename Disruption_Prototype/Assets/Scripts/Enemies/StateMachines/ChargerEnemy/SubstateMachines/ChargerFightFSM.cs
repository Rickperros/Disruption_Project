using Utils;
using StateMachines;
using UnityEngine;
using System;

public class ChargerFightFSM : MonoBehaviour, IFSMState
{
    public enum EStates {INITIAL, PREPARE, DASH}

    private ChargerBlackboard m_blackboard;
    private Timer m_attackClock;

    private EStates m_currentState;
    private EStates m_nextState;

    public void Start()
    {
        m_blackboard = GetComponent<ChargerBlackboard>();
        m_attackClock = new Timer(m_blackboard.m_attackCooldown);
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

                m_nextState = EStates.PREPARE;
                ChangeState();
                break;
            case EStates.PREPARE:

                if(m_attackClock.CheckTimer())
                {
                    m_nextState = EStates.DASH;
                    ChangeState();
                    break;
                }
                break;
            case EStates.DASH:
                
                if (Vector3.Distance(m_blackboard.m_player.position, this.transform.position) <= m_blackboard.m_closeEnoughRadius)
                {
                    m_nextState = EStates.PREPARE;
                    ChangeState();
                    break;
                }

                if(Vector3.Distance(m_blackboard.m_player.position, m_blackboard.m_GuardPoint.position) <= Vector3.Distance(this.transform.position, m_blackboard.m_GuardPoint.position))
                {
                    m_nextState = EStates.PREPARE;
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
            case EStates.PREPARE:
                m_blackboard.m_interposeSteering.enabled = false;
                break;
            case EStates.DASH:
                m_blackboard.m_seekSteering.enabled = false;
                break;
        }

        //enter
        switch (m_nextState)
        {
            case EStates.INITIAL:
                break;
            case EStates.PREPARE:
                m_blackboard.m_interposeSteering.m_interposeInfo.m_coveredObject = m_blackboard.m_GuardPoint;
                m_blackboard.m_interposeSteering.m_interposeInfo.m_target = m_blackboard.m_player;
                m_blackboard.m_interposeSteering.enabled = true;
                m_attackClock.ForceUpdate();
                break;
            case EStates.DASH:
                m_blackboard.m_lastPlayerPos.position = m_blackboard.m_player.position;
                m_blackboard.m_seekSteering.m_info.m_target = m_blackboard.m_lastPlayerPos;
                m_blackboard.m_seekSteering.enabled = true;
                break;
        }

        m_currentState = m_nextState;
    }
}
