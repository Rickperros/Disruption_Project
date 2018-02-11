using UnityEngine;

[RequireComponent(typeof(PlayerBlackboard))]
public class PlayerController : MonoBehaviour 
{
    private PlayerBlackboard m_blackboard;

    #region Monobehaivour Methods

    private void Start()
    {
        m_blackboard = GetComponent<PlayerBlackboard>();
        m_blackboard.m_usingSkill = false;
    }

    private void Update()
    {
        if(GameManager.GetInstance().IsGamePlaying())
        {
            if (m_blackboard.m_battery.IsUseful())
            {
                m_blackboard.m_currentMovementType.TryToMove();

                if (!m_blackboard.m_usingSkill)
                    m_blackboard.m_usingSkill = m_blackboard.m_currentSkill.TryToStart();
                else
                    m_blackboard.m_currentSkill.OnUse();

                m_blackboard.m_currentPrimaryWeapon.UseWeapon();
                ConsumeBattery(m_blackboard.m_batteryConstantConsumption * Time.deltaTime);
                PauseGame();
            }
        }
        else
        {
            ResumeGame();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Recharge Point")
        {
            TryToRecharge(other.GetComponent<RechargePoint>());
        }

        if (other.tag == "Information Point")
        {

        }

        if(other.tag == "Goal Point")
        {
            GameManager.GetInstance().StopGame();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            m_blackboard.m_hitMove.l_enemyForward = collision.gameObject.transform.forward;
            m_blackboard.m_currentMovementType = m_blackboard.m_hitMove;
        }
    }
    #endregion

   #region Private Methods

    private void TryToRecharge(RechargePoint rechargePoint)
    {
        if (!Input.GetKeyDown(KeyCode.R))
            return;

        RechargeBattery(rechargePoint.GetBattery());
    }

    private void ConsumeBattery(float consumptionAmount)
    {
        m_blackboard.m_battery.Consume(consumptionAmount);
    }

    private void RechargeBattery(float rechargeAmount)
    {
        m_blackboard.m_battery.PartialRecharge(rechargeAmount);
    }

    private void FullFillBattery()
    {
        m_blackboard.m_battery.FullRecharge();
    }

    private void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.P))
            GameManager.GetInstance().PauseGame();
    }

    private void ResumeGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
            GameManager.GetInstance().ResumeGame();
    }
    #endregion
}
