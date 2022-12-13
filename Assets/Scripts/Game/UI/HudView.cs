using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class HudView : UIView<IHudController>
{
    [SerializeField]
    private TMP_Text m_ScoreLabel = null;

    [SerializeField]
    private TMP_Text m_WaypointLabel = null;

    [SerializeField]
    private TMP_Text m_TimerLabel = null;

    [SerializeField]
    private Image m_MotivationMeter = null;

    void Awake()
    {
        Assert.IsNotNull(m_WaypointLabel);
        Assert.IsNotNull(m_TimerLabel);
        Assert.IsNotNull(m_ScoreLabel);
        Assert.IsNotNull(m_MotivationMeter);
    }

    protected override void OnWillShow()
    {
        OnMotivationChanged();
        OnProgressChanged();
        OnScoreChanged();
        OnTimeChanged();
    }

    public void OnMotivationChanged()
    {
        m_MotivationMeter.fillAmount = GetController().MotivationPercent;
    }

    public void OnProgressChanged()
    {
        m_WaypointLabel.SetText(
            $"{GetController().CurrentWaypoint}/{GetController().WaypointCount}");
    }

    public void OnScoreChanged()
    {
        m_ScoreLabel.SetText(GetController().CurrentScore.ToString("N0"));
    }

    public void OnTimeChanged()
    {
        TimeSpan elapsedTime = GetController().TimeElapsed;
        m_TimerLabel.SetText(
            elapsedTime.Hours.ToString("D2") + ":" +
            elapsedTime.Minutes.ToString("D2") + ":" +
            elapsedTime.Seconds.ToString("D2") + "." +
            (elapsedTime.Milliseconds / 10).ToString("D2"));
    }
}
