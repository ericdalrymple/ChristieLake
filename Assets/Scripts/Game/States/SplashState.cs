using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashState : GameState
{
    [SerializeField]
    private UIHandle m_SplashScreenHandle;

    public override void Enter()
    {
        GameController.UIManager.ShowDialog(m_SplashScreenHandle);
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
