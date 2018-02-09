using UnityEngine;

public class PlayerDashSkill : ISkill
{
    private CharacterController m_characterController;
    private PlayerBlackboard m_playerBlackboard;
    private PlayerController m_playerController;

    private float l_timeDashing = 0;
    private float l_timeDashingLimit = 0.5f;

    public void DeInit()
    {
    }

    public void Init(PlayerBlackboard blackboard)
    {
        m_playerBlackboard = blackboard;
        m_characterController = blackboard.m_characterController;
        m_playerController = blackboard.m_playerController;
        l_timeDashing = 0;

    }

    public void OnEnd()
    {
        m_playerBlackboard.m_usingSkill = false;
        l_timeDashing = 0f;

        if (m_playerBlackboard.m_currentMovementType != null || !m_playerBlackboard.m_currentMovementType.Equals(null))
            m_playerBlackboard.m_currentMovementType.DeInit();

        m_playerBlackboard.m_currentMovementType = m_playerBlackboard.m_normalMove;
        m_playerBlackboard.m_currentMovementType.Init(m_playerBlackboard);
    }

    public void OnUse()
    {

        if (l_timeDashing < l_timeDashingLimit && m_playerBlackboard.m_PlayerDashing)
            l_timeDashing += Time.deltaTime;
        else
            OnEnd();
    }

    public bool TryToStart()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || m_playerBlackboard.m_PlayerDashing)
            return false;

        if (m_playerBlackboard.m_currentMovementType != null || !m_playerBlackboard.m_currentMovementType.Equals(null))
            m_playerBlackboard.m_currentMovementType.DeInit();

        m_playerBlackboard.m_currentMovementType = m_playerBlackboard.m_dashMove;
        m_playerBlackboard.m_currentMovementType.Init(m_playerBlackboard);

        return true;
    }
}
