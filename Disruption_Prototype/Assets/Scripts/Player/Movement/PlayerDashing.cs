using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashing : IMovement
{

    private CharacterController m_characterController;
    private PlayerBlackboard m_playerBlackboard;
    private PlayerController m_playerController;


    private float l_timeDashing = 0;
    [SerializeField] private float l_timeDashingLimit = 0.5f;
    private Vector3 l_dashDirection = Vector3.zero;
    private Vector3 l_endPostion;


    public void Init(PlayerBlackboard blackboard)
    {
        m_playerBlackboard = blackboard;
        m_characterController = blackboard.m_characterController;
        m_playerController = blackboard.m_playerController;


        //Dash variables
        m_playerBlackboard.m_PlayerDashing = true;
        l_timeDashing = 0;
        l_dashDirection = m_playerBlackboard.m_movementDirection.normalized;

        l_endPostion = blackboard.m_playerController.transform.position + l_dashDirection;
    }

    public void TryToMove()
    {
        Dash();
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



    public void Dash()
    {



        if (l_timeDashing < l_timeDashingLimit && m_playerBlackboard.m_PlayerDashing)
        {
            l_timeDashing += Time.deltaTime;
            //l_characterController.Move(m_playerBlackboard.l_dashVelocity * l_dashDirection * (l_timeDashingLimit - l_timeDashing < 0.1f ? l_timeDashingLimit - l_timeDashing : 0.1f));
            m_characterController.Move(m_playerBlackboard.l_dashVelocity * l_dashDirection /** (l_endPostion - l_playerController.transform.position).magnitude)*/);
        }
        else
        {
            m_playerBlackboard.m_PlayerDashing = false;
            m_playerBlackboard.m_currentMovementType = m_playerBlackboard.m_normalMove;
            m_playerBlackboard.m_currentMovementType.Init(m_playerBlackboard);
        }


    }

}
