using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour//OBSOLETE!!! PROTO Version
{
    [SerializeField] private PlayerController m_playerInfo;
    [SerializeField] private Slider m_batteryCapacityInfo;

    private void Awake()
    {
        m_batteryCapacityInfo.maxValue = m_playerInfo.GetMaxBatteryCapacity();
    }
    private void Update()
    {
        m_batteryCapacityInfo.value = m_playerInfo.GetCurrentBatteryCapacity();
    }
}
