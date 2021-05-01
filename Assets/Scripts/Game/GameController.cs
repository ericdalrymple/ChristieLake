using PocketValues.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameController : SingletonBehaviour<GameController>, IHudController
{
    [Header("Subsystems")]

    [SerializeField]
    private UIManager m_UIManager;


    [Header("Config")]

    [SerializeField]
    private StringReference m_GameTitle = new StringReference();

    [SerializeField]
    private GameObject m_Player;

    [SerializeField]
    private Camera m_Camera;

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
        get { return 100000000; }
    }

    void Awake()
    {
        Assert.IsNotNull(m_UIManager, "Must specify a UIManager.");
        Assert.IsNotNull(m_Player, "Must pass ref to the player");
        m_Camera = Camera.main;


    }
}
