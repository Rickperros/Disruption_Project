using UnityEngine;

[System.Serializable]
public class Battery
{
    [SerializeField] private float m_maxCapacity = 100f;
    private float m_currentCapacity = 0f;

    public Battery()
    {
        m_maxCapacity = 100f;
        m_currentCapacity = m_maxCapacity;
    }

    public bool IsUseful()
    {
        return m_currentCapacity >= 0f;
    }

    public float GetCurrentCapacity()
    {
        return m_currentCapacity;
    }

    public float GetMaxCapacity()
    {
        return m_maxCapacity;
    }

    public void PartialRecharge(float fillAmount)
    {
        m_currentCapacity += fillAmount;

        if (m_currentCapacity >= m_maxCapacity)
            m_currentCapacity = m_maxCapacity;
    }

    public void FullRecharge()
    {
        m_currentCapacity = m_maxCapacity;
    }

    public void Consume(float consumeAmount)
    {
        m_currentCapacity -= consumeAmount;
    }
}
