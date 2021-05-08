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
        Debug.Log("UIManager::ClearViews()");
        // Hide all the view
        foreach (BaseUIView view in m_ViewLookup.Values)
        {
            //view?.Hide();
        }

        // Clear the nav stack
        m_NavigationStack.Clear();
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

    public void Register(IEnumerable<BaseUIView> views)
    {
        foreach (BaseUIView view in views)
        {
            if (view != null)
            {
                Debug.Log("UIManager::Register(view): " + view.ToString());
                m_ViewLookup.Add(view.Handle, view);
            }
        }
    }

    public void UnregisterViews()
    {
        foreach (BaseUIView view in m_ViewLookup.Values)
        {
            if (view != null)
            {
                Debug.Log("UIManager::Unregister(view): " + view.ToString());
            }
        }

        m_ViewLookup.Clear();
    }

    public BaseUIView ShowDialog(UIHandle viewHandle, IUIController controller)
    {
        if (viewHandle == null)
        {
            // Null key
            return null;
        }

        BaseUIView view = null;
        var val = m_ViewLookup.TryGetValue(viewHandle.Value, out view);
        Assert.IsTrue(val, $"No registered dialog with handle '{viewHandle}'.");

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
            Debug.Log("UIManager::PopInvalidViews() -- null or view not found in lookup");
            m_NavigationStack.Pop();
            popped = true;
        }

        return popped;
    }

    void Update()
    {
        CurrentDialog?.Tick();
    }
}
