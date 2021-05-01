using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class SplashView : UIView<SplashState>
{
    [SerializeField]
    private TMP_Text m_TitleLabel;

    void Awake()
    {
        Assert.IsNotNull(m_TitleLabel);
    }

    void Update()
    {
        
    }

    protected override void OnShown()
    {
    }

    protected override void RefreshView()
    {
        m_TitleLabel.SetText(GetController().GameTitle);
    }
}
