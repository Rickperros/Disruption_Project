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

    #region Monobehaivour Methods

    private void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        //m_event = LevelManager.GetInstance().GetGameEvent(m_eventId);
    }
    private void Update()
    {
        if(GameManager.GetInstance().IsGamePlaying())
        {
            if(m_battery.IsUseful())
            {
                MovePlayer();
                Shoot();
                ReleaseTrap();
                ConsumeBattery(m_batteryConstantConsumption * Time.deltaTime);
                PauseGame();
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

        l_bullet.transform.position = transform.position;
        l_bullet.SetDirection(Vector3.right);
        l_bullet.gameObject.SetActive(true);

        ConsumeBattery(m_shootConsumption);
        m_shootClock.ForceUpdate();
    }

    private void ReleaseTrap()
    {
        if (!(Input.GetMouseButtonDown(1) && m_shootClock.CheckTimer()))
            return;

        Debug.Log("Trap");
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
    #endregion

    #region Debug methods

    public void PrintString()
    {
        Debug.Log("Event Start");
    }
    public void PrintString2()
    {
        Debug.Log("Event End");
    }
   /* private void DebugEndEvent()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            m_event.EndGameEvent((GameEvent.OnLoopGameEvent) PrintString2);
    }
    private void StartEvent()
    {
        if (Input.GetKeyDown(KeyCode.P))
            m_event.InitGameEvent(PrintString);
    }*/
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
