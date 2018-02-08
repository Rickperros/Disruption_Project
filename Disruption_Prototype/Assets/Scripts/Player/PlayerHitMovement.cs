using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitMovement : IMovement
{

    private CharacterController m_characterController;
    private PlayerBlackboard m_playerBlackboard;
    private PlayerController m_playerController;

    public Vector3 l_enemyForward;
    private float l_timePush = 0, l_timePushLimit = 0.2f;

    private Timer l_hitTimer;

    void IMovement.Init(PlayerBlackboard blackboard)
    {
        m_playerBlackboard = blackboard;
        m_characterController = blackboard.m_characterController;
        m_playerController = blackboard.m_playerController;

        l_enemyForward = Vector3.right * 4; //DE PROVAA



        l_timePush = 0;
    }

    void  IMovement.TryToMove()
    {
        l_timePush += Time.deltaTime;
        if (l_timePush <= l_timePushLimit)
        {
            Debug.Log("PlayerHit and moving away");
            m_characterController.Move(l_enemyForward * m_playerBlackboard.m_hitPushValue * Time.deltaTime);
        } else
        {
            m_playerController.m_MovementType = m_playerBlackboard.m_MoveNormal;
            m_playerController.m_MovementType.Init(m_playerBlackboard);
        }
    }


    void IMovement.DeInit()
    {
    }

    void IMovement.OnControllerCOlliderHit(ControllerColliderHit hit)
    {
    }

    void IMovement.OnTriggerEnter(Collider other)
    {
    }

    void IMovement.OnTriggerExit(Collider other)
    {
    }

    void IMovement.OnTriggerStay(Collider other)
    {
    }


}
