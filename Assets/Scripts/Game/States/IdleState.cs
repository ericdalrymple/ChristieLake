using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GameState, IUIController
{
    [SerializeField]
    private GameStateHandle m_WinState;

    [SerializeField]
    private GameStateHandle m_LoseState;

    [SerializeField]
    private UIHandle m_HudHandle;

    private BaseUIView m_View;

    public override void Enter()
    {
        GameController.UIManager.ShowDialog(m_HudHandle, GameController.Instance);

        StateMachine.gameObject.BroadcastMessage(GameMessages.MSG_RACE_START);
    }

    public override void Exit()
    {
        GameController.UIManager.ClearViews();
    }

    public void OnFinishRace()
    {
        StateMachine.SetCurrentState(m_WinState);
    }

    public void OnGameOver()
    {
        StateMachine.SetCurrentState(m_LoseState);
    }
}
