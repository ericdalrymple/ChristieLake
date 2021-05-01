using PocketValues.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashState : GameState, IUIController
{
    [SerializeField]
    private UIHandle m_SplashViewHandle;

    [SerializeField]
    private StringReference m_GameTitle;

    public string GameTitle
    {
        get
        {
            return m_GameTitle.Value;
        }
    }

    public override void Enter()
    {
        GameController.UIManager.ShowDialog(m_SplashViewHandle, this);
    }

    public override void Exit()
    {
        GameController.UIManager.ClearViews();
    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
    }
}
