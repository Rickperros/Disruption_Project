using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashSkill : ISkill
{
    private CharacterController m_characterController;
    private PlayerBlackboard m_playerBlackboard;
    private PlayerController m_playerController;
    private IMovement movement;

    public void Init(PlayerBlackboard blackboard)
    {
        m_playerBlackboard = blackboard;
        m_characterController = blackboard.m_characterController;
        m_playerController = blackboard.m_playerController;
        movement = blackboard.m_MoveDashing;
    }


    public void UseSkill()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !m_playerBlackboard.m_PlayerDashing)
        {
            if (m_playerController.m_MovementType != null)
                m_playerController.m_MovementType.DeInit();

            m_playerController.m_MovementType = m_playerBlackboard.m_MoveDashing;
            m_playerController.m_MovementType.Init(m_playerBlackboard);
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
