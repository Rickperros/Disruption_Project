using UnityEngine;

[System.Serializable]
public class GameEvent
{
    [Tooltip("Event identier")]
    [SerializeField] private string m_eventId;
    [Tooltip("The event has to be repeating over game?")]
    [SerializeField] private bool m_cyclicalEvent;
    [Tooltip("Number of cycles that have to be done, 0 means infinite looping")]
    [SerializeField] private int m_cycles;
    [SerializeField] private EGameEventStates m_currentState;

    private int m_currentCycleCounter;

    public delegate void OnInitGameEvent();
    public delegate void OnEndGameEvent();
    public delegate void OnLoopGameEvent();

    #region Constructors
    /// <summary>
    /// Constructor
    /// </summary>
    public GameEvent()
    {
        m_eventId = "Default";
        m_currentState = EGameEventStates.WAITING;
        m_cyclicalEvent = false;
        m_currentCycleCounter = 0;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">Name of the event</param>
    /// <param name="cyclical">Is event cyclical over time?</param>
    /// <param name="cycles">0 means infinite looping</param>
    /// <param name="startingState">first game event state</param>
    public GameEvent(string name, bool cyclical, int cycles = 0, EGameEventStates startingState = EGameEventStates.WAITING)
    {
        m_eventId = name;
        m_currentState = startingState;
        m_cyclicalEvent = cyclical;
        m_currentCycleCounter = cycles;
    }

    #endregion

    #region Public Check Methods

    /// <summary>
    /// Returns true if the event id is the same as the instanced event id
    /// </summary>
    /// <param name="idToCheck">Id to check</param>
    public bool CheckEventId(string idToCheck)
    {
        return m_eventId == idToCheck;
    }

    /// <summary>
    /// Returns true if game event state is currently waiting
    /// </summary>
    public bool IsWaiting()
    {
        return m_currentState == EGameEventStates.WAITING;
    }

    /// <summary>
    /// Returns true if game event state is currently finished
    /// </summary>
    public bool IsFinished()
    {
        return m_currentState == EGameEventStates.FINISHED;
    }

    /// <summary>
    /// Returns true if game event state is currently running
    /// </summary>
    public bool IsRunning()
    {
        return m_currentState == EGameEventStates.RUNNING;
    }

    /// <summary>
    /// Returns true if event is cyclical
    /// </summary>
    public bool IsCyclicalEvent()
    {
        return m_cyclicalEvent;
    }

    /// <summary>
    /// Returns if its last loop of this event
    /// </summary>
    public bool IsLastLoop()
    {
        return !m_cyclicalEvent || m_cycles > 0 && (m_cycles - 1) == m_currentCycleCounter;
    }
    #endregion

    #region Public Input Methods

    /// <summary>
    /// Sets current state of the event to running
    /// </summary>
    public void InitGameEvent()
    {
        m_currentState = EGameEventStates.RUNNING;
    }

    /// <summary>
    /// Sets the current state of the event to running and executes an On Init function
    /// </summary>
    /// <param name="OnInitGameEventMethod">function to execute it has to be void return and have no arguments</param>
    public void InitGameEvent(OnInitGameEvent OnInitGameEventMethod)
    {
        m_currentState = EGameEventStates.RUNNING;
        OnInitGameEventMethod();
    }

    /// <summary>
    /// Sets the event current state to finnished, if its cyclical its set to waiting
    /// </summary>
    public void EndGameEvent()
    {
        m_currentState = EGameEventStates.FINISHED;

        if(!m_cyclicalEvent || !(m_cycles == 0 || m_currentCycleCounter < m_cycles))
            return;

        m_currentState = EGameEventStates.WAITING;
        m_currentCycleCounter += 1;     
    }

    /// <summary>
    /// Sets the current state of the event to finished or waiting (if cyclical) and executes an On End function
    /// </summary>
    /// <param name="OnEndGameEventMethod">function to execute it has to be void return and have no arguments</param>
    public void EndGameEvent(OnEndGameEvent OnEndGameEventMethod)
    {
        m_currentState = EGameEventStates.FINISHED;

        if (!m_cyclicalEvent || !(m_cycles == 0 || m_currentCycleCounter < m_cycles))
        {
            OnEndGameEventMethod();
            return;
        }

        m_currentState = EGameEventStates.WAITING;
        m_currentCycleCounter += 1;
    }

    /// <summary>
    /// Sets the current state of the event to finished or waiting (if cyclical) and executes an On Loop function
    /// </summary>
    /// <param name="OnLoopGameEventMethod">function to execute it has to be void return and have no arguments</param>
    public void EndGameEvent(OnLoopGameEvent OnLoopGameEventMethod)
    {
        m_currentState = EGameEventStates.FINISHED;

        if (!m_cyclicalEvent || !(m_cycles == 0 || m_currentCycleCounter < m_cycles))
            return;

        OnLoopGameEventMethod();
        m_currentState = EGameEventStates.WAITING;
        m_currentCycleCounter += 1;
    }

    /// <summary>
    /// Sets the current state of the event to finished or waiting (if cyclical) and executes an On End and On Loop function
    /// </summary>
    /// <param name="OnEndGameEventMethod">function to execute it has to be void return and have no arguments</param>
    /// <param name="OnLoopGameEventMethod">function to execute it has to be void return and have no arguments</param>
    public void EndGameEvent(OnLoopGameEvent OnLoopGameEventMethod, OnEndGameEvent OnEndGameEventMethod)
    {
        m_currentState = EGameEventStates.FINISHED;

        if (!m_cyclicalEvent || !(m_cycles == 0 || m_currentCycleCounter < m_cycles))
        {
            OnEndGameEventMethod();
            return;
        }

        OnLoopGameEventMethod();
        m_currentState = EGameEventStates.WAITING;
        m_currentCycleCounter += 1;
    }

    #endregion
}
