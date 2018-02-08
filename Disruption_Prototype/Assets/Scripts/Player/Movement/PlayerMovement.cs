using UnityEngine;

public class PlayerMovement : IMovement
{

    private CharacterController l_characterController;
    private PlayerBlackboard m_playerBlackboard;

    public void Init(PlayerBlackboard blackboard)
    {
        m_playerBlackboard = blackboard;
        l_characterController = blackboard.m_characterController;
    }

    public void TryToMove()
    {
        m_playerBlackboard.m_movementDirection.x = Input.GetAxisRaw("Horizontal");
        m_playerBlackboard.m_movementDirection.y = m_playerBlackboard.m_gravity;
        m_playerBlackboard.m_movementDirection.z = Input.GetAxisRaw("Vertical");

        l_characterController.Move(m_playerBlackboard.m_movementDirection.normalized * m_playerBlackboard.m_speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
    }

    public void OnTriggerStay(Collider other)
    {
    }

    public void OnTriggerExit(Collider other)
    {
    }

    public void OnControllerCOlliderHit(ControllerColliderHit hit)
    {
    }

    public void DeInit()
    {
    }

  
}
