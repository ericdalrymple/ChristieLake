using PocketValues.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class UIView : MonoBehaviour
{
    [SerializeField]
    private UIHandle m_Handle;

    [SerializeField]
    private bool m_ShowOnStart = false;

    [SerializeField]
    private bool m_PushToNavigationStack = true;

    public string Handle
    {
        get
        {
            if (m_Handle != null)
            {
                return m_Handle.Value;
            }

            return string.Empty;
        }
    }

    public bool ShowOnStart
    {
        get { return m_ShowOnStart; }
    }

    public bool PushToNavigationStack
    {
        get { return m_PushToNavigationStack; }
    }

    void Start()
    {
        RefreshView();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    protected abstract void RefreshView();
}
