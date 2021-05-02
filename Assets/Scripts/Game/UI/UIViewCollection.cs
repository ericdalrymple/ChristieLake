using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewCollection : MonoBehaviour
{
    [SerializeField]
    private BaseUIView[] m_Views = new BaseUIView[0];

    void Awake()
    {
        foreach (BaseUIView view in m_Views)
        {
            if (view != null)
            {
                view.Hide();
                GameController.UIManager.Register(view);
            }
        }
    }

    void OnDestroy()
    {
        foreach (BaseUIView view in m_Views)
        {
            GameController.UIManager.Unregister(view);
        }
    }
}
