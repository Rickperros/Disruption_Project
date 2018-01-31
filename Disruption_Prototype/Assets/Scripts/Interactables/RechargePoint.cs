using UnityEngine;

public class RechargePoint : MonoBehaviour //OBSOLETE!!! PROTO Version
{
    [SerializeField] private float m_batteryRechargeAmount = 50f;
    
    private bool m_useful = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player" || !m_useful)
            return;
        //Display context info
    }

    public float GetBattery()
    {
        m_useful = false;
        gameObject.tag = "Untagged";
        return m_batteryRechargeAmount;
    }
}
