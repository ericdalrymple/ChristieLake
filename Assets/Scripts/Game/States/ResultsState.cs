using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResultsState : GameState, IResultsController
{
    [SerializeField]
    private UIHandle m_ResultViewHandle = null;

    [SerializeField]
    private UIHandle m_PlayerNameViewHandle = null;

    [SerializeField]
    private GameStateHandle m_LeaderboardState = null;

    [SerializeField]
    private InputActionAsset m_InputActions = null;

    public int CurrentScore
    {
        get { return GameController.Instance.CurrentScore; }
    }

    public TimeSpan TimeElapsed
    {
        get { return GameController.Instance.TimeElapsed; }
    }

    public override bool AllowGameplayInput
    {
        get { return false; }
    }

    public override void Enter()
    {
        GameController.UIManager.ShowDialog(m_ResultViewHandle, this);
    }

    public override void Exit()
    {
        GameController.UIManager.ClearViews();
    }

    public void SubmitScoreWithName(string name)
    {
        GameController.Instance.CurrentName = name;
        StateMachine.SetCurrentState(m_LeaderboardState);
    }

    public void GoToRetry()
    {
        GameController.Instance.ResetGame();
    }

    public void GoToSubmit()
    {
        GameController.UIManager.ShowDialog(m_PlayerNameViewHandle, this);
    }
}
