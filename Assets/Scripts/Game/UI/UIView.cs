using PocketValues.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class UIView<ControllerType> : MonoBehaviour
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
        OnHidden();
    }

    public void Show(ControllerType controller)
    {
        gameObject.SetActive(true);
        OnShown(controller);
    }

    protected abstract void RefreshView();

    protected virtual void OnShown(Object controller) {}

    protected virtual void OnHidden() {}
}
