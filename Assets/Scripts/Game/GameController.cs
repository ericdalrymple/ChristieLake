using PocketValues.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

public class GameController
    : SingletonBehaviour<GameController>
    , IHudController
    , IResultsController
{
    [Header("Subsystems")]

    [SerializeField]
    private UIManager m_UIManager;

    [Header("Config")]

    [SerializeField]
    private StringReference m_GameTitle = new StringReference();

    [SerializeField]
    private IntegerReference m_MaxMotivation = new IntegerReference(100);

    [SerializeField]
    private GameObject m_Player;

    [SerializeField]
    private Camera m_Camera;

    private GameSession m_Session;

    public static UIManager UIManager
    {
        get { return Instance?.m_UIManager; }
    }

    public static string GameTitle
    {
        get { return Instance?.m_GameTitle.Value; }
    }

    public int CurrentScore
    {
        // TODO: Implement
        get { return 100000000; }
    }

    public int CurrentMotivation
    {
        // TODO: Implement
        get { return 250; }
    }

    public int MaxMotivation
    {
        get { return m_MaxMotivation.Value; }
    }

    public float MotivationPercent
    {
        get { return (float)CurrentMotivation / (float)MaxMotivation; }
    }

    public TimeSpan TimeElapsed
    {
        // TODO: Implement
        get { return new TimeSpan(0, 0, 0); }
    }

    void Awake()
    {
        Assert.IsNotNull(m_UIManager, "Must specify a UIManager.");
        Assert.IsNotNull(m_Player, "Must pass ref to the player");

        // Cache game session
        m_Session = GetComponent<GameSession>();
        Assert.IsNotNull(m_Session, "Must add a GameSession component onto the GameController.");

        m_Camera = Camera.main;
        PlayerTracker playerTracker = m_Camera.GetComponent<PlayerTracker>();
        playerTracker.trackedObject = m_Player;
        StartGame();
    }


    public void StartGame()
    {
        m_Session.Reset();
        Debug.Log("Start race! Scene: " + SceneManager.GetActiveScene().ToString());

    }


    public void OnFinishRace()
    {
        WinGame();
    }


    public void WinGame()
    {
        Debug.Log("Race time: " + m_Session);
        ResetGame();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        StartGame();
    }
}
