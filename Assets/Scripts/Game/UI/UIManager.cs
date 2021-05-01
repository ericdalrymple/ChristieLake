using PocketValues.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class UIManager : MonoBehaviour
{
    private Dictionary<string, UIView> m_ViewLookup = new Dictionary<string, UIView>();

    private Stack<UIView> m_NavigationStack = new Stack<UIView>();

    private UIView CurrentDialog
    {
        get
        {
            UIView dialog = null;
            if (m_NavigationStack.Count > 0)
            {
                return m_NavigationStack.Peek();
            }

            return dialog;
        }
    }

    public void ClearViews()
    {
        // Hide all views and pop them from the navigation stack
        while (m_NavigationStack.Count > 0)
        {
            if (!PopInvalidViews())
            {
                m_NavigationStack.Peek().Hide();
                m_NavigationStack.Pop();
            }
        }
    }

    public void NavigateBack()
    {
        if (!PopInvalidViews())
        {
            // Pop the current view
            CurrentDialog?.Hide();
            m_NavigationStack.Pop();
        }

        PopInvalidViews();
        CurrentDialog?.Show();
    }

    public void Register(UIView view)
    {
        m_ViewLookup.Add(view.Handle, view);
    }

    public void Unregister(UIView view)
    {
        m_ViewLookup.Remove(view.Handle);
    }

    public UIView ShowDialog(UIHandle viewHandle, UIController controller)
    {
        if (viewHandle == null)
        {
            // Null key
            return null;
        }

        UIView dialog;
        Assert.IsTrue(m_ViewLookup.TryGetValue(viewHandle.Value, out dialog), $"No registered dialog with handle '{viewHandle}'.");

        if ((dialog != null) && (dialog != CurrentDialog))
        {
            dialog.Show(controller);

            if (dialog.PushToNavigationStack && CurrentDialog != dialog)
            {
                m_NavigationStack.Push(dialog);
            }
        }

        return dialog;
    }

    private bool PopInvalidViews()
    {
        bool popped = false;

        while (m_NavigationStack.Peek() == null || !m_ViewLookup.ContainsKey(m_NavigationStack.Peek().Handle))
        {
            // Pop destroyed or unregistered views from the top of the stack
            m_NavigationStack.Pop();
            popped = true;
        }

        return popped;
    }
}
