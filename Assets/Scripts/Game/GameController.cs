using PocketValues.Types;
using System;
using System.Collections;
using System.Collections.Generic;
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

    private Camera m_Camera;
    private CanoeController m_Player;
    private GameSession m_Session;
    private GameStateMachine m_StateMachine;
    private UIManager m_UIManager;
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

    public void ResetGame()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    protected override void OnSceneLoaded()
    {
        // Wipe the session
        m_Session.ResetValues();

        // Cache scene objects
        m_Camera = Camera.main;
        m_Player = FindObjectOfType<CanoeController>(false);
        Assert.IsNotNull(m_Player, "No player found in the scene.");

        m_WaypointManager = FindObjectOfType<Waypoints>();
        Assert.IsNotNull(m_WaypointManager, "No race course found in the scene.");
        m_WaypointManager.Initialize();

        // Setup UI
        UIViewCollection viewColltection = FindObjectOfType<UIViewCollection>();
        Assert.IsNotNull(viewColltection, "Must add a UIViewCollection to the scene.");
        m_UIManager.UnregisterViews();
        m_UIManager.Register(viewColltection.Views);

        // Setup tracker
        PlayerTracker playerTracker = m_Camera.GetComponent<PlayerTracker>();
        playerTracker.trackedObject = GetPlayer();

        // Reset the game state
        m_StateMachine.StartRetry();
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
    }
}
