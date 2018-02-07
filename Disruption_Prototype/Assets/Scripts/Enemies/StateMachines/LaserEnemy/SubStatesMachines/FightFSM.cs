using StateMachines;
using UnityEngine;

[RequireComponent(typeof(LaserEnemyBlackboard))]
[RequireComponent(typeof(CompanionFightFSM))]
[RequireComponent(typeof(SoloFightFSM))]
public class FightFSM : MonoBehaviour, IFSMState
{
    public enum EStates { INITIAL, SOLO_FIGHT, COMPANION_FIGHT }

    private LaserEnemyBlackboard m_blackboard;
    private CompanionFightFSM m_companionFight;
    private SoloFightFSM m_soloFight;

    private EStates m_currentState;
    private EStates m_nextState;

    public void Awake()
    {
        m_currentState = EStates.INITIAL;

        m_blackboard = GetComponent<LaserEnemyBlackboard>();
        m_companionFight = GetComponent<CompanionFightFSM>();
        m_soloFight = GetComponent<SoloFightFSM>();

        m_companionFight.enabled = false;
        m_soloFight.enabled = false;
    }

    public void OnEnter()
    {
        m_currentState = EStates.INITIAL;
        m_companionFight.enabled = false;
        m_soloFight.enabled = false;
    }

    public void OnExit()
    {
        m_companionFight.OnExit();
        m_soloFight.OnExit();
        m_companionFight.enabled = false;
        m_soloFight.enabled = false;
    }

    public void Update()
    {
        switch(m_currentState)
        {
            case EStates.INITIAL:

                m_nextState = EStates.COMPANION_FIGHT;
                ChangeState();
                break;
            case EStates.COMPANION_FIGHT:

                if(m_blackboard.m_companion == null || m_blackboard.m_companion.Equals(null))
                {
                    m_nextState = EStates.SOLO_FIGHT;
                    ChangeState();
                    break;
                }
                break;
            case EStates.SOLO_FIGHT:

                if (m_blackboard.m_companion != null && !m_blackboard.m_companion.Equals(null))
                {
                    m_nextState = EStates.COMPANION_FIGHT;
                    ChangeState();
                    break;
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
            case EStates.COMPANION_FIGHT:
                m_companionFight.OnExit();
                m_companionFight.enabled = false;
                break;
            case EStates.SOLO_FIGHT:
                m_soloFight.OnExit();
                m_soloFight.enabled = false;
                break;
        }

        //Enter
        switch (m_nextState)
        {
            case EStates.INITIAL:
                break;
            case EStates.COMPANION_FIGHT:
                m_companionFight.enabled = true;
                m_companionFight.OnEnter();
                break;
            case EStates.SOLO_FIGHT:
                m_soloFight.enabled = true;
                m_soloFight.OnEnter();
                break;
        }

        m_currentState = m_nextState;
    }
}
