using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameEvent> m_levelEvents;

    private static LevelManager m_instance;

    #region Monobehaivour

    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Gets Game event by its id
    /// </summary>
    public GameEvent GetGameEvent(string eventId)
    {
        return m_levelEvents.Find((x) => x.CheckEventId(eventId));
    }

    #endregion

    #region Singleton
    public static LevelManager GetInstance()
    {
        return m_instance;
    }
    #endregion
}
