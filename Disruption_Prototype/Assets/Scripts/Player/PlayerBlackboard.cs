using UnityEngine;

public class PlayerBlackboard : MonoBehaviour
{
    public Battery m_battery;
    public float m_batteryConstantConsumption = 5f;
    public float m_speed = 3f;
    public float m_gravity = -2;
    public Vector3 m_movementDirection;
    public bool m_usingSkill;

    //laser gun vars
    public float m_laserGunCoolDown;
    public float m_shootBatteryConsumption = 2f;

    //GrenadeLauncher vars 
    public float m_grenadeLauncherCoolDown;
    public float m_TrapBatteryConsumption = 5f;

    //Variables
    public bool m_PlayerDashing = false; 
    public float l_dashVelocity = 0.1f;
    public float m_hitPushValue = 1;

    [HideInInspector] public CharacterController m_characterController;
    [HideInInspector] public PlayerController m_playerController;
    [HideInInspector] public PlayerMovement m_normalMove;
    [HideInInspector] public PlayerDashing m_dashMove;
    [HideInInspector] public PlayerHitMovement m_hitMove;

    //Skills
    [HideInInspector] public PlayerDashSkill m_dashSkill;

    //Weapons
    [HideInInspector] public LaserGun m_laserGun;

    //current states
    [HideInInspector] public IWeapon m_currentPrimaryWeapon;
    [HideInInspector] public IWeapon m_currentSecondaryWeapon;
    [HideInInspector] public IMovement m_currentMovementType;
    [HideInInspector] public ISkill m_currentSkill;


    void Awake ()
    {
        m_characterController = GetComponent<CharacterController>();
        m_playerController = GetComponent<PlayerController>();

        m_normalMove = new PlayerMovement();
        m_dashMove = new PlayerDashing();
        m_dashSkill = new PlayerDashSkill();
        m_hitMove = new PlayerHitMovement();
        m_laserGun = new LaserGun();

        m_normalMove.Init(this);
        m_hitMove.Init(this);
        m_laserGun.Init(this);

        m_dashSkill.Init(this);

        m_currentPrimaryWeapon = m_laserGun;
        m_currentMovementType = m_normalMove;
        m_currentSkill = m_dashSkill;
    }
}
