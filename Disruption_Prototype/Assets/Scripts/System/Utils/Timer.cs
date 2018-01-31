using UnityEngine;

[System.Serializable]
public class Timer
{
    [SerializeField] private float m_initTime;
    [SerializeField] private float m_updateTime;

    private float m_currentTime;
    private bool m_initialized;

    public Timer(float updateTime)
    {
        m_initTime = 0f;
        m_updateTime = updateTime;
        m_initialized = false;
        m_currentTime = Time.time;
    }

    public Timer(float initTime, float updateTime)
    {
        m_initTime = initTime;
        m_updateTime = updateTime;
        m_initialized = false;
        m_currentTime = Time.time;
    }

    public void ForceUpdate()
    {
        m_currentTime = Time.time;
    }

    public bool CheckTimer()
    {
        if (!m_initialized)
            return InitTimer();

        if (Time.time <= m_updateTime + m_currentTime)
            return false;

        m_currentTime = Time.time;
        return true;
    }

    private bool InitTimer()
    {
        if (Time.time <= m_initTime + m_currentTime)
            return false;

        m_currentTime = Time.time;
        m_initialized = true;
        return true;
    }
}
