using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewCollection : MonoBehaviour
{
    [SerializeField]
    private UIView[] m_Views = new UIView[0];

    void Awake()
    {
        foreach (UIView view in m_Views)
        {
            GameController.UIManager.Register(view);
        }
    }

    void OnDestroy()
    {
        foreach (UIView view in m_Views)
        {
            GameController.UIManager.Unregister(view);
        }
    }
}
