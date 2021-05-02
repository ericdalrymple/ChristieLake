using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class HudView : UIView<IHudController>
{
    [SerializeField]
    private TMP_Text m_ScoreLabel;

    [SerializeField]
    private TMP_Text m_WaypointLabel;

    [SerializeField]
    private TMP_Text m_TimerLabel;

    [SerializeField]
    private Image m_MotivationMeter;

    private void Awake()
    {
        Assert.IsNotNull(m_WaypointLabel);
        Assert.IsNotNull(m_TimerLabel);
        Assert.IsNotNull(m_ScoreLabel);
    }

    protected override void OnWillShow()
    {
        Update();
    }

    void Update()
    {
        UpdateMotivation();
        UpdateScore();
        UpdateTimer();
        UpdateWaypoints();
    }

    private void UpdateMotivation()
    {
        m_MotivationMeter.fillAmount = GetController().MotivationPercent;
    }

    private void UpdateScore()
    {
        m_ScoreLabel.SetText(GetController().CurrentScore.ToString("N0"));
    }

    private void UpdateTimer()
    {
        TimeSpan elapsed = GetController().TimeElapsed;
        m_TimerLabel.SetText(
            elapsed.Hours.ToString("D2") + ":" +
            elapsed.Minutes.ToString("D2") + ":" +
            elapsed.Seconds.ToString("D2") + "." +
            (elapsed.Milliseconds / 10).ToString("D2"));
    }

    private void UpdateWaypoints()
    {
        m_WaypointLabel.SetText(
            $"{GetController().CurrentWaypoint}/{GetController().WaypointCount}");
    }
}
