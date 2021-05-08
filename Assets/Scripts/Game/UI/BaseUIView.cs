using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUIView : MonoBehaviour
{
    [SerializeField]
    private UIHandle m_Handle;

    [SerializeField]
    private bool m_PushToNavigationStack = true;

    public abstract IUIController Controller { get; set; }
    public string Handle
    {
        get { return m_Handle.Value; }
    }

    public bool PushToNavigationStack
    {
        get { return m_PushToNavigationStack; }
    }

    public abstract void Hide();
    public abstract void Show();
    public virtual void Tick() { }
}
