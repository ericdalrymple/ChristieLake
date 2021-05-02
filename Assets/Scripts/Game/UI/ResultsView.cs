using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsView : UIView<IResultsController>
{
    [SerializeField]
    private TMP_Text m_ScoreLabel;

    [SerializeField]
    private TMP_Text m_TimeLabel;

    protected override void OnWillShow()
    {
        m_ScoreLabel.SetText(GetController().CurrentScore.ToString("N0"));

        TimeSpan elapsed = GetController().TimeElapsed;
        m_TimeLabel.SetText(
            elapsed.Hours.ToString("D2") + ":" +
            elapsed.Minutes.ToString("D2") + ":" +
            elapsed.Seconds.ToString("D2") + "." +
            (elapsed.Milliseconds / 10).ToString("D2"));
    }
}
