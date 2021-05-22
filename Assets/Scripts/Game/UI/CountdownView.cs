using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownView : UIView<CountdownState>
{
    [SerializeField]
    private Animator m_AnimationPrefab;

    [HideInInspector]
    public bool m_IsCountdownComplete = false;

    private Animator m_AnimationInstance = null;

    public override void Tick()
    {
        if (m_AnimationInstance.GetCurrentAnimatorStateInfo(0).IsName("Done"))
        {
            GetController().OnCountdownComplete();
            DestroyImmediate(m_AnimationInstance);
        }
    }

    protected override void OnShown()
    {
        m_AnimationInstance = Instantiate(m_AnimationPrefab, transform, false);
    }
}
