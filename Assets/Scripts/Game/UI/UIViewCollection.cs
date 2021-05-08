using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class UIViewCollection : MonoBehaviour
{
    [SerializeField]
    private BaseUIView[] m_Views = new BaseUIView[0];

    public ReadOnlyCollection<BaseUIView> Views
    {
        get { return new ReadOnlyCollection<BaseUIView>(m_Views); }
    }

    void Start()
    {
        // Hide all views to ease iteration
        foreach (BaseUIView view in m_Views)
        {
            view.Hide();
        }
    }
}
