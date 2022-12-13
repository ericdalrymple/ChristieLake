using UnityEngine;

public class CountdownState : GameState, IUIController
{
    [SerializeField]
    private GameStateHandle m_GoState = null;

    [SerializeField]
    private UIHandle m_CountdownView = null;

    public override bool AllowGameplayInput
    {
        get { return false; }
    }

    public override void Enter()
    {
        GameController.UIManager.ShowDialog(m_CountdownView, this);
    }

    public override void Exit()
    {
        GameController.UIManager.ClearViews();
    }

    public void OnCountdownComplete()
    {
        StateMachine.SetCurrentState(m_GoState);
    }
}
