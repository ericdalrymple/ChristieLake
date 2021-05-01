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
    private Image m_MotivationMeter;

    private void Awake()
    {
        Assert.IsNotNull(m_ScoreLabel);
    }

    protected override void OnWillShow()
    {
        // Update widget states and contents
        m_ScoreLabel.SetText(GetController().CurrentScore.ToString("N0"));

        m_MotivationMeter.fillAmount = 0.5f;
    }
}
