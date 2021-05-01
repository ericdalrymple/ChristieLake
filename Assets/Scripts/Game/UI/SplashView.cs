using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class SplashView : UIView
{
    [SerializeField]
    private TMP_Text m_TitleLabel;

    private void Awake()
    {
        Assert.IsNotNull(m_TitleLabel);

        foreach (string joystick in Input.GetJoystickNames())
        {
            Debug.Log(joystick);
        }
    }

    protected override void RefreshView()
    {
        m_TitleLabel.SetText(GameController.GameTitle);
    }
}
