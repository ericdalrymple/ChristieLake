using UnityEngine;

public abstract class BaseUIView : MonoBehaviour
{
    [SerializeField]
    private UIHandle m_Handle = null;

    [SerializeField]
    private bool m_PushToNavigationStack = true;

    public abstract IUIController Controller { get; set; }
    public string Handle
    {
        get { return m_Handle.Value; }
    }

    public bool IsVisible
    {
        get { return isActiveAndEnabled; }
    }

    public bool PushToNavigationStack
    {
        get { return m_PushToNavigationStack; }
    }

    public virtual void Tick() { }
    public void Hide()
    {
        gameObject.SetActive(false);
        OnWillHide();
        OnHidden();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        OnWillShow();
        OnShown();
    }

    protected virtual void OnWillShow() { }
    protected virtual void OnShown() { }
    protected virtual void OnWillHide() { }
    protected virtual void OnHidden() { }
}
