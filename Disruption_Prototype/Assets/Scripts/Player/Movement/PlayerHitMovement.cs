using UnityEngine;

public class PlayerHitMovement : IMovement
{

    private CharacterController m_characterController;
    private PlayerBlackboard m_playerBlackboard;
    private PlayerController m_playerController;

    public Vector3 l_enemyForward;
    private float l_timePush = 0, l_timePushLimit = 0.2f;

    private Timer l_hitTimer;

    public void Init(PlayerBlackboard blackboard)
    {
        m_playerBlackboard = blackboard;
        m_characterController = blackboard.m_characterController;
        m_playerController = blackboard.m_playerController;

        l_enemyForward = Vector3.right * 4; //DE PROVAA



        l_timePush = 0;
    }

    public void  TryToMove()
    {
        l_timePush += Time.deltaTime;
        if (l_timePush <= l_timePushLimit)
        {
            m_characterController.Move(l_enemyForward * m_playerBlackboard.m_hitPushValue * Time.deltaTime);
        } else
        {
            m_playerBlackboard.m_currentMovementType = m_playerBlackboard.m_normalMove;
            m_playerBlackboard.m_currentMovementType.Init(m_playerBlackboard);
        }
    }


    public void DeInit()
    {
    }

    public void OnControllerCOlliderHit(ControllerColliderHit hit)
    {
    }

    public void OnTriggerEnter(Collider other)
    {
    }

    public void OnTriggerExit(Collider other)
    {
    }

    public void OnTriggerStay(Collider other)
    {
    }


}
