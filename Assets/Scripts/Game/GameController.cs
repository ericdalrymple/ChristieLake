using PocketValues.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameController : SingletonBehaviour<GameController>
{
    [Header("Subsystems")]

    [SerializeField]
    private UIManager m_UIManager;


    [Header("Config")]

    [SerializeField]
    private StringReference m_GameTitle = new StringReference();

    public static UIManager UIManager
    {
        get { return Instance?.m_UIManager; }
    }

    public static string GameTitle
    {
        get { return Instance?.m_GameTitle.Value; }
    }

    void Awake()
    {
        Assert.IsNotNull(m_UIManager, "Must specify a UIManager.");
    }
}
