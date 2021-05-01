using PocketValues.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class UIManager : MonoBehaviour
{
    private Dictionary<string, BaseUIView> m_ViewLookup = new Dictionary<string, BaseUIView>();

    private Stack<BaseUIView> m_NavigationStack = new Stack<BaseUIView>();

    private BaseUIView CurrentDialog
    {
        get
        {
            if (m_NavigationStack.Count > 0)
            {
                return m_NavigationStack.Peek();
            }

            return null;
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

    public void Register(BaseUIView view)
    {
        m_ViewLookup.Add(view.Handle, view);
    }

    public void Unregister(BaseUIView view)
    {
        m_ViewLookup.Remove(view.Handle);
    }

    public BaseUIView ShowDialog(UIHandle viewHandle, IUIController controller)
    {
        if (viewHandle == null)
        {
            // Null key
            return null;
        }

        BaseUIView view;
        Assert.IsTrue(m_ViewLookup.TryGetValue(viewHandle.Value, out view), $"No registered dialog with handle '{viewHandle}'.");

        if ((view != null) && (view != CurrentDialog))
        {
            view.Controller = controller;
            view.Show();

            if (view.PushToNavigationStack && CurrentDialog != view)
            {
                m_NavigationStack.Push(view);
            }
        }

        return view;
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
