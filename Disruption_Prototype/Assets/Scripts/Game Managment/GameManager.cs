using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour //Obsolete!!
{
    [SerializeField] private int m_playerBulletsPoolSize;
    [SerializeField] private int m_playerGrenadesPoolSize;
    [SerializeField] private GameObject m_playerBulletPrefab;
    [SerializeField] private GameObject m_playerGrenadePrefab;


    private GameObjectPool m_playerBullets;
    private GameObjectPool m_playerGrenades;

    private static GameManager m_instance;
    private EGameStates m_currentGameState = EGameStates.NONE;

    #region Monobehaivour functions

    public void Awake()
    {
        if (m_instance == null)
            m_instance = this;

        SetPlayerBulletsPool();
        SetPlayerGrenadesPool();

    }

    private void Start()
    {
        ResumeGame();
    }

    #endregion

    #region Public Consultion Methods

    public bool IsGameLoading()
    {
        return m_currentGameState == EGameStates.LOADING;
    }

    public bool IsGameWaiting()
    {
        return m_currentGameState == EGameStates.WAITING;
    }

    public bool IsGamePlaying()
    {
        return m_currentGameState == EGameStates.PLAYING;
    }

    public bool IsGamePaused()
    {
        return m_currentGameState == EGameStates.PAUSED;
    }

    public bool IsGameOver()
    {
        return m_currentGameState == EGameStates.GAME_OVER;
    }

    #endregion

    #region Public Input Methods

    public void StopGame()
    {
        m_currentGameState = EGameStates.GAME_OVER;
    }

    public void StopGame(string nextScene)
    {
        LoadScene(nextScene);
    }

    public void StopGame(int sceneIndex)
    {
        LoadScene(sceneIndex);
    }

    public void PauseGame()
    {
        m_currentGameState = EGameStates.PAUSED;
    }

    public void ResumeGame()
    {
        m_currentGameState = EGameStates.PLAYING;
    }

    public void LoadScene(string nextScene, LoadSceneMode loadMode = LoadSceneMode.Single)
    {
        m_currentGameState = EGameStates.LOADING;
        SceneManager.LoadScene(nextScene, loadMode);
    }

    public void LoadScene(int nextSceneIndex, LoadSceneMode loadMode = LoadSceneMode.Single)
    {
        m_currentGameState = EGameStates.LOADING;
        SceneManager.LoadScene(nextSceneIndex, loadMode);
    }

    #endregion

    #region Public Getters

    public Bullet GetPlayerBullet()
    {
        GameObject l_bulletObject = m_playerBullets.RequireObject();
        l_bulletObject.SetActive(true);

        return l_bulletObject.GetComponent<Bullet>();
    }


    //Afegit
    public Grenade GetPlayerGrenade()
    {
        GameObject l_GrenadeObject = m_playerGrenades.RequireObject();
        l_GrenadeObject.SetActive(true);

        return l_GrenadeObject.GetComponent<Grenade>();
    }


    #endregion

    #region Editor Functions
#if UNITY_EDITOR

    public void SetPlayerBulletsPool()
    {
        if (m_playerBullets != null)
            m_playerBullets.ClearPool();

        m_playerBullets = new GameObjectPool(m_playerBulletPrefab, m_playerBulletsPoolSize, "Player Bullets Pool");
    }

    public void SetPlayerGrenadesPool()
    {
        if (m_playerGrenades != null)
            m_playerGrenades.ClearPool();
        m_playerGrenades = new GameObjectPool(m_playerGrenadePrefab, m_playerGrenadesPoolSize, "Player Grenades Pool");
    }


#endif
    #endregion

    #region Static functions

    public static GameManager GetInstance()
    {
        return m_instance;
    }

    #endregion
}
