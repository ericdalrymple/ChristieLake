using UnityEngine;

public class LeaderboardState : GameState, IUIController
{
    public delegate void LeaderboardChangedDelegate();

    public event LeaderboardChangedDelegate OnLeaderboardChanged;

    [SerializeField]
    private UIHandle m_LeaderboardViewHandle = null;

    [SerializeField]
    private UIHandle m_LoadingViewHandle = null;

    private LeaderboardView m_LeaderboardView = null;

    public Leaderboard CurrentLeaderboard
    {
        get { return GameController.Instance.GlobalLeaderboard; }
    }

    public LeaderboardScore CurrentScore
    {
        get;
        private set;
    }

    public override bool AllowGameplayInput
    {
        get { return false; }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Enter()
    {
        m_LeaderboardView = (LeaderboardView)GameController.UIManager.ShowDialog(m_LeaderboardViewHandle, this);
        //m_LoadingView = GameController.UIManager.ShowDialog(m_LoadingViewHandle, this);

        CurrentLeaderboard.OnLeaderboardUpdated += OnLeaderboardUpdated;        
        CurrentLeaderboard.Submit(
                this,
                GameController.Instance.CurrentName,
                GameController.Instance.CurrentScore);
    }

    public override void Exit()
    {
        CurrentLeaderboard.OnLeaderboardUpdated -= OnLeaderboardUpdated;
    }

    public void RestartGame()
    {
        GameController.Instance.ResetGame();
    }

    private void OnLeaderboardUpdated(Leaderboard leaderboard)
    {
        //m_LoadingView.Hide();
        //OnLeaderboardChanged.Invoke();
        m_LeaderboardView.OnLeaderboardUpdated();
    }
}
