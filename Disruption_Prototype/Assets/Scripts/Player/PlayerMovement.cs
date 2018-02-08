using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : IMovement {

    private CharacterController l_characterController;
    private PlayerBlackboard m_playerBlackboard;

    void IMovement.Init(PlayerBlackboard blackboard)
    {
        m_playerBlackboard = blackboard;
        l_characterController = blackboard.m_characterController;
    }

    void IMovement.TryToMove()
    {
        m_playerBlackboard.m_movementDirection.x = Input.GetAxisRaw("Horizontal");
        m_playerBlackboard.m_movementDirection.y = m_playerBlackboard.m_GravityScale;
        m_playerBlackboard.m_movementDirection.z = Input.GetAxisRaw("Vertical");

        //m_playerBlackboard.gameObject.transform.rotation = Quaternion.Euler(m_playerBlackboard.m_movementDirection.x * 90,/* m_playerBlackboard.m_movementDirection.y * 90*/ 0, m_playerBlackboard.m_movementDirection.z * 90);

        l_characterController.Move(m_playerBlackboard.m_movementDirection.normalized * m_playerBlackboard.m_movementSpeed * Time.deltaTime);
    }

    void IMovement.OnTriggerEnter(Collider other)
    {
    }

    void IMovement.OnTriggerStay(Collider other)
    {
    }

    void IMovement.OnTriggerExit(Collider other)
    {
    }

    void IMovement.OnControllerCOlliderHit(ControllerColliderHit hit)
    {
    }

    void IMovement.DeInit()
    {
    }

  
}
