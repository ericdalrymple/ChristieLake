using UnityEngine;

public class LeaderboardView : UIView<LeaderboardState>
{
    [SerializeField]
    private LeaderboardList m_List = null;

    [SerializeField]
    private int m_DisplayLimit = 100;

    public void OnLeaderboardUpdated()
    {
        Leaderboard leaderboard = GetController().CurrentLeaderboard;

        m_List.SetContent(
            leaderboard,
            m_DisplayLimit,
            leaderboard.LatestScore.Guid);
    }

    public void OnRestartButtonClicked()
    {
        GetController().RestartGame();
    }

    protected override void OnWillShow()
    {
        //GetController().OnLeaderboardChanged += OnLeaderboardUpdated;
    }

    protected override void OnWillHide()
    {
        //GetController().OnLeaderboardChanged -= OnLeaderboardUpdated;
    }
}
