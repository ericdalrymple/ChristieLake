using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class HudView : UIView<IHudController>
{
    [SerializeField]
    private TMP_Text m_ScoreLabel;

    private void Awake()
    {
        Assert.IsNotNull(m_ScoreLabel);
    }

    private void Update()
    {

    }

    protected override void OnWillShow()
    {
        // Update widget states and contents
        m_ScoreLabel.SetText(GetController().CurrentScore.ToString("N0"));
    }
}
