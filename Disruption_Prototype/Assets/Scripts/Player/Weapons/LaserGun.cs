using UnityEngine;

public class LaserGun : IWeapon
{
    private Timer m_attackClock;
    private PlayerBlackboard m_blackboard;

    public void DeInit()
    {
        m_blackboard.m_currentPrimaryWeapon = null;
    }

    public void Init(PlayerBlackboard blackboard)
    {
        m_blackboard = blackboard;
        m_blackboard.m_currentPrimaryWeapon = this;
        m_attackClock = new Timer(m_blackboard.m_laserGunCoolDown);
    }

    public void UseWeapon()
    {
        if (!(Input.GetMouseButton(0) && m_attackClock.CheckTimer()) && !Input.GetMouseButtonDown(0))
            return;

        Bullet l_bullet = GameManager.GetInstance().GetPlayerBullet();

        RaycastHit l_hitInfo;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out l_hitInfo, 8))
        {
            Vector3 direction = l_hitInfo.point - m_blackboard.transform.position;
            direction.y = 0;
            l_bullet.SetDirection(direction.normalized);
        }

        l_bullet.transform.position = m_blackboard.transform.position;

        l_bullet.gameObject.SetActive(true);

        m_blackboard.m_battery.Consume(m_blackboard.m_shootBatteryConsumption);
        m_attackClock.ForceUpdate();
    }
}
