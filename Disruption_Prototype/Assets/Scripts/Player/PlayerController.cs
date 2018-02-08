using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour //OBSOLETE!!! PROTO Version
{
    [SerializeField] private Battery m_battery;
    [SerializeField] private float m_batteryConstantConsumption = 5f;
    [SerializeField] private float m_shootConsumption = 2f;
    [SerializeField] private float m_TrapConsumption = 5f;

    [SerializeField] private float m_speed = 3f;
    [SerializeField] private Timer m_shootClock;
    [SerializeField] private Timer m_TrapClock;
   // [SerializeField] private string m_eventId;
 
    private CharacterController m_characterController;
    //private GameEvent m_event;

    //Afegit
    private bool l_dashing = false; //evita el movement
    private float l_timeDashing = 0;
    [SerializeField] private float l_timeDashingLimit = 2;
    private Vector3 l_dashDirection = Vector3.zero;

    public IMovement m_MovementType;
    public ISkill m_Skill;
    public IWeapon m_weapon;
    private PlayerBlackboard blackboard;

    #region Monobehaivour Methods

    private void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        blackboard = GetComponent<PlayerBlackboard>();
        m_MovementType = blackboard.m_MoveNormal;
        m_MovementType.Init(blackboard);

        m_Skill = blackboard.m_SkillDash;
        m_Skill.Init(blackboard);

        m_weapon = blackboard.m_weaponGrenade;
        //m_weapon.Init(blackboard);


    }


    private void Update()
    {
        if(GameManager.GetInstance().IsGamePlaying())
        {


            if (m_battery.IsUseful())
            {
                m_MovementType.TryToMove();
                m_Skill.UseSkill();
                Shoot();
                ReleaseTrap();
                ConsumeBattery(m_batteryConstantConsumption * Time.deltaTime);
                PauseGame();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                m_MovementType = blackboard.m_MoveHit;
                m_MovementType.Init(blackboard);
            }
            DebugFullFill();
            // DebugRecharge();
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
            blackboard.m_MoveHit.l_enemyForward = collision.gameObject.transform.forward;
            m_MovementType = blackboard.m_MoveHit;
        }
    }
    #endregion

    #region Public Methods

    public float GetCurrentBatteryCapacity()
    {
        return m_battery.GetCurrentCapacity();
    }

    public float GetMaxBatteryCapacity()
    {
        return m_battery.GetMaxCapacity();
    }

    #endregion

    #region Private Methods

    private void Shoot()
    {
        if (!(Input.GetMouseButton(0) && m_shootClock.CheckTimer()) && !Input.GetMouseButtonDown(0))
            return;

        Bullet l_bullet = GameManager.GetInstance().GetPlayerBullet();

        RaycastHit l_hitInfo;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out l_hitInfo, 8))
        {
            Vector3 direction = l_hitInfo.point - transform.position;
            direction.y = 0;
            l_bullet.SetDirection(direction.normalized);
        }

        l_bullet.transform.position = transform.position;

        l_bullet.gameObject.SetActive(true);

        ConsumeBattery(m_shootConsumption);
        m_shootClock.ForceUpdate();
    }

    private void ReleaseTrap()
    {
        if (!(Input.GetMouseButtonDown(1) && m_shootClock.CheckTimer()))
            return;

        Debug.Log("Trap");
        //ThrowGrenade();
        ConsumeBattery(m_shootConsumption);
    }

    private void TryToRecharge(RechargePoint rechargePoint)
    {
        if (!Input.GetKeyDown(KeyCode.R))
            return;

        RechargeBattery(rechargePoint.GetBattery());
    }

    private void MovePlayer()
    {
        m_characterController.Move((Vector3.right * Input.GetAxisRaw("Horizontal") + Vector3.forward * Input.GetAxisRaw("Vertical")).normalized * m_speed * Time.deltaTime);
    }

    private void ConsumeBattery(float consumptionAmount)
    {
        m_battery.Consume(consumptionAmount);
    }

    private void RechargeBattery(float rechargeAmount)
    {
        m_battery.PartialRecharge(rechargeAmount);
    }

    private void FullFillBattery()
    {
        m_battery.FullRecharge();
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

   

    private void ThrowGrenade()
    {
        if (!(Input.GetMouseButton(1) && m_shootClock.CheckTimer()) && !Input.GetMouseButtonDown(1))
            return;
        m_weapon.Init(blackboard);

        Grenade l_grenadeThrown = GameManager.GetInstance().GetPlayerGrenade();

        l_grenadeThrown.l_myGrenade.transform.position = transform.position;
        l_grenadeThrown.GrenadeThrown(transform.forward);
        l_grenadeThrown.l_myGrenade.SetActive(true);
        
        ConsumeBattery(m_shootConsumption);
        m_shootClock.ForceUpdate();
    }
    #endregion

    #region Debug methods

    private void DebugFullFill()
    {
        if (Input.GetKeyDown(KeyCode.F))
            FullFillBattery();
    }
    
    private void DebugRecharge()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RechargeBattery(25f);
    }
    #endregion
}
