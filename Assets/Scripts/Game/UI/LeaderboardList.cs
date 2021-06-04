using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardList : MonoBehaviour
{
    [SerializeField]
    private LeaderboardEntry m_EntryPrefab = null;

    [SerializeField]
    private ScrollRect m_ScrollRect = null;

    private List<LeaderboardEntry> m_Entries = new List<LeaderboardEntry>();

    public void SetContent(Leaderboard leaderboard, int maxEntries, Guid currentScore)
    {
        int oldEntryCount = m_Entries.Count;
        int entryCount = Mathf.Min(maxEntries, leaderboard.ScoreCount);

        if (entryCount != oldEntryCount)
        {
            // Destroy excess entries
            if (entryCount < m_Entries.Count)
            {
                for (int i = entryCount; i < m_Entries.Count; ++i)
                {
                    DestroyImmediate(m_Entries[i].gameObject);
                }

                m_Entries.RemoveRange(entryCount, m_Entries.Count - entryCount);
            }

            // Create missing entries
            while (entryCount > m_Entries.Count)
            {
                LeaderboardEntry newEntry = Instantiate(m_EntryPrefab, m_ScrollRect.content);
                m_Entries.Add(newEntry);
            }
        }

        // Populate and layout the entries
        float padding = m_ScrollRect.content.GetComponent<VerticalLayoutGroup>().spacing;
        float contentHeight = 0;

        int entryIndex = 0;
        float currentScorePosition = 0.0f;
        foreach (LeaderboardScore score in leaderboard.Scores)
        {
            bool isCurrentScore = currentScore.Equals(score.Guid);
            float entryHeight = m_Entries[entryIndex].Height;
            m_Entries[entryIndex].SetScore(entryIndex + 1, score, isCurrentScore);

            if (isCurrentScore)
            {
                // Track the position of the entry for the current score
                currentScorePosition = contentHeight + (entryIndex / (entryCount - 1)) * entryHeight;
            }

            contentHeight += entryHeight;

            entryIndex++;
            if (entryIndex == entryCount)
            {
                // All leaderboard entries filled
                break;
            }

            contentHeight += padding;
        }

        // Adjust height of content pane to be exactly the height we need
        m_ScrollRect.content.sizeDelta = new Vector2(m_ScrollRect.content.sizeDelta.x, contentHeight);

        // Focus on the player's score
        if (contentHeight > 0.0f)
        {
            m_ScrollRect.verticalNormalizedPosition = 1.0f - (currentScorePosition / contentHeight);
        }
    }
}
