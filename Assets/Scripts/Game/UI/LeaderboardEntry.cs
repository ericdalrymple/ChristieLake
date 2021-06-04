using TMPro;
using UnityEngine;

public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_RankText = null;

    [SerializeField]
    private TMP_Text m_NameText = null;

    [SerializeField]
    private TMP_Text m_ScoreText = null;

    public float Height
    {
        get
        {
            RectTransform xform = (RectTransform)transform;
            return xform.sizeDelta.y;
        }
    }

    public void SetScore(int rank, LeaderboardScore score, bool isCurrentScore = false)
    {
        m_RankText.SetText($"<i>#</i>{rank}");
        m_NameText.SetText(score.Name);
        m_ScoreText.SetText(score.Value.ToString("N0"));

        if (isCurrentScore)
        {
            Color highlight = new Color(1.0f, 0.93f, 0.4f);
            m_RankText.color = highlight;
            m_NameText.color = highlight;
            m_ScoreText.color = highlight;
        }
        else
        {
            m_RankText.color = Color.white;
            m_NameText.color = Color.white;
            m_ScoreText.color = Color.white;
        }
    }
}
