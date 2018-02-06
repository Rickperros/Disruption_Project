using UnityEngine;
using Steerings;

namespace StateMachines
{
    [RequireComponent(typeof(LaserEnemyBlackboard))]
    [RequireComponent(typeof(KeepPosition))]
    public class SoloFightFSM : MonoBehaviour, IFSMState
    {
        public enum EState {INITIAL, KEEP_POSITION, SHOOT}

        public LaserEnemyBlackboard m_blackboard;
        public KeepPosition m_steering;

        private EState m_currentState;
        private EState m_nextState;
        private Timer m_attackTimer;

        public void Init()
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
                    Debug.Log("Shoot");
                    m_nextState = EState.KEEP_POSITION;
                    ChangeState();
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
                     break;
                case EState.SHOOT:
                    break;
            }

            //Enter state
            switch(m_nextState)
            {
                case EState.INITIAL:
                    break;
                case EState.KEEP_POSITION:
                    break;
                case EState.SHOOT:
                    break;
            }

            m_currentState = m_nextState;
        }
    }
}
