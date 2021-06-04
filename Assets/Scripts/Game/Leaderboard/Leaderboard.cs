using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public class Leaderboard
{
    private enum LeaderboardStatus
    {
        Empty,
        Retrieving,
        Submitting,
        Ready,
        Error
    }

    public delegate void LeaderboardUpdatedDelegate(Leaderboard leaderboard);

    private static readonly int MIN_NAME_LENGTH = 0;
    private static readonly int MAX_NAME_LENGTH = 3;

    public event LeaderboardUpdatedDelegate OnLeaderboardUpdated;

    private LeaderboardStatus m_Status = LeaderboardStatus.Empty;
    private SortedSet<LeaderboardScore> m_Scores = new SortedSet<LeaderboardScore>();

    public bool IsReady
    {
        get
        {
            return m_Status == LeaderboardStatus.Ready;
        }
    }

    public LeaderboardScore LatestScore
    {
        get;
        private set;
    }

    public int ScoreCount
    {
        get
        {
            return m_Scores.Count;
        }
    }

    public IEnumerable<LeaderboardScore> Scores
    {
        get
        {
            return m_Scores;
        }
    }

    public void Retrieve(MonoBehaviour source)
    {
        InnerRetrieve();
    }

    public void Submit(MonoBehaviour source, string name, int score)
    {
        InnerSubmit(source, name, score);
    }

    public void InnerRetrieve()
    {
        m_Status = LeaderboardStatus.Retrieving;

        // TODO: Fetch scores.


        // Repopulate local leaderboard scores
        /* m_Scores.Clear();
        for (int i = 0; i < scores.Length; i += 2)
        {
            LeaderboardScore score = new LeaderboardScore(scores[i], scores[i + 1]);
            m_Scores.Add(score);
        }*/

        // Update status
        m_Status = LeaderboardStatus.Ready;
        if (OnLeaderboardUpdated != null)
        {
            OnLeaderboardUpdated.Invoke(this);
        }
    }

    private void InnerSubmit(MonoBehaviour source, string name, int score)
    {
        Assert.IsTrue(
            (name.Length >= MIN_NAME_LENGTH) && (name.Length <= MAX_NAME_LENGTH),
            $"Score name '{name}' must be between {MIN_NAME_LENGTH} and {MAX_NAME_LENGTH} characters in length.");

        m_Status = LeaderboardStatus.Submitting;

        LatestScore = new LeaderboardScore(name, score);

        // TODO: Submit score to table.
        m_Scores.Add(LatestScore);

        InnerRetrieve();
    }
}
