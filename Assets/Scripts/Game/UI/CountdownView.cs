using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownView : UIView<CountdownState>
{
    [SerializeField]
    private Animator m_Animator;

    [HideInInspector]
    public bool m_IsCountdownComplete = false;

    public override void Tick()
    {
        if (m_IsCountdownComplete)
        {
            GetController().OnCountdownComplete();
            m_IsCountdownComplete = false;
        }
    }

    protected override void OnShown()
    {
        m_Animator.Rebind();
        m_Animator.SetTrigger("StateEnter");
    }

    protected override void OnWillShow()
    {
        m_IsCountdownComplete = false;
    }
}
