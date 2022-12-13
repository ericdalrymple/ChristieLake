using PocketValues.Types;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(GameSession))]
public class GameController
    : SingletonBehaviour<GameController>
    , IHudController
    , IResultsController
{
    [Header("Config")]

    [SerializeField]
    private StringReference m_GameTitle = new StringReference();

    private bool m_InitialLaunch = true;

    private Camera m_Camera;
    private CanoeController m_Player;
    private GameSession m_Session;
    private GameStateMachine m_StateMachine;
    private UIManager m_UIManager;
    private UIViewCollection m_ViewCollection;
    private Waypoints m_WaypointManager;

    public static GameObject GameObject
    {
        get { return Instance?.gameObject; }
    }

    public static UIManager UIManager
    {
        get {
            Assert.IsNotNull(Instance?.m_UIManager);
            return Instance?.m_UIManager; }
    }

    public static string GameTitle
    {
        get { return Instance?.m_GameTitle.Value; }
    }

    public static bool GameplayInputEnabled
    {
        get { return Instance ? (Instance.m_StateMachine ? Instance.m_StateMachine.GameplayInputEnabled : true) : true; }
    }

    public int CurrentScore
    {
        get { return m_Session.CurrentScore; }
    }

    public int CurrentMotivation
    {
        get { return m_Session.CurrentMotivation; }
    }

    public float MotivationPercent
    {
        get
        {
            if (m_Session.MaxMotivation <= 0)
            {
                return 0;
            }

            return (float)CurrentMotivation / (float)m_Session.MaxMotivation;
        }
    }

    public int CurrentWaypoint
    {
        get { return m_WaypointManager.Completed; }
    }

    public GameObject NextWaypoint
    {
        get { return m_WaypointManager.NextWaypoint; }
    }

    public int WaypointCount
    {
        get { return m_WaypointManager.Count; }
    }

    public TimeSpan TimeElapsed
    {
        get { return m_Session.ElapsedTime; }
    }

    public GameObject GetPlayer()
    {
        return m_Player.gameObject;
    }

    public void NotifyMotivationChanged()
    {
        gameObject.BroadcastMessage(GameMessages.MSG_MOTIVATION_CHANGED, null, SendMessageOptions.DontRequireReceiver);
        m_ViewCollection.BroadcastMessage(GameMessages.MSG_MOTIVATION_CHANGED, null, SendMessageOptions.DontRequireReceiver);
    }

    public void NotifyProgressChanged()
    {
        gameObject.BroadcastMessage(GameMessages.MSG_PROGRESS_CHANGED, null, SendMessageOptions.DontRequireReceiver);
        m_ViewCollection.BroadcastMessage(GameMessages.MSG_PROGRESS_CHANGED, null, SendMessageOptions.DontRequireReceiver);
    }

    public void NotifyScoreChanged()
    {
        gameObject.BroadcastMessage(GameMessages.MSG_SCORE_CHANGED, null, SendMessageOptions.DontRequireReceiver);
        m_ViewCollection.BroadcastMessage(GameMessages.MSG_SCORE_CHANGED, null, SendMessageOptions.DontRequireReceiver);
    }

    public void NotifyTimeChanged()
    {
        gameObject.BroadcastMessage(GameMessages.MSG_TIME_CHANGED, null, SendMessageOptions.DontRequireReceiver);
        m_ViewCollection.BroadcastMessage(GameMessages.MSG_TIME_CHANGED, null, SendMessageOptions.DontRequireReceiver);
    }

    public void ResetGame()
    {
        m_InitialLaunch = false;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    protected override void OnSceneLoaded()
    {
        // Cache scene objects
        m_Camera = Camera.main;
        m_Player = FindObjectOfType<CanoeController>(false);
        Assert.IsNotNull(m_Player, "No player found in the scene.");

        m_WaypointManager = FindObjectOfType<Waypoints>();
        Assert.IsNotNull(m_WaypointManager, "No race course found in the scene.");
        m_WaypointManager.Initialize();

        // Setup UI
        m_ViewCollection = FindObjectOfType<UIViewCollection>();
        Assert.IsNotNull(m_ViewCollection, "Must add a UIViewCollection to the scene.");

        m_UIManager.UnregisterViews();
        m_UIManager.Register(m_ViewCollection.Views);
        m_UIManager.ClearViews();

        // Wipe the session
        m_Session.ResetValues();

        // Reset the game state
        if (!m_InitialLaunch)
        {
            m_StateMachine.StartRetry();
        }
    }

    void OnWaypointChanged()
    {
        NotifyProgressChanged();
    }

    void Awake()
    {
        // Cache/initialize local components
        m_Session = GetComponent<GameSession>();
        Assert.IsNotNull(m_Session, "Must add a GameSession component onto the GameController.");

        m_StateMachine = GetComponent<GameStateMachine>();
        Assert.IsNotNull(m_StateMachine, "Must add a GameStateMachine component onto the GameController.");
        m_StateMachine.Initialize();

        m_UIManager = GetComponent<UIManager>();
        Assert.IsNotNull(m_UIManager, "Must add a UIManager component onto the GameController.");

        // Load scenes
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);

        // Cache scenes
        //m_RaceScene = SceneManager.GetSceneByName("Basic");
        //m_TerrainScene = SceneManager.GetSceneByName("Terrain");
        //m_PropsScene = SceneManager.GetSceneByName("Props");
    }

    void Update()
    {
        m_StateMachine.Tick();
        m_UIManager.Tick();
    }
}
