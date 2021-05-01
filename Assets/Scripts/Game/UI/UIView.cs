using PocketValues.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class UIView<ControllerType> : BaseUIView where ControllerType : IUIController
{
    private ControllerType m_Controller;

    public override IUIController Controller
    {
        get
        {
            return m_Controller;
        }

        set
        {
            bool validType = value.GetType().IsAssignableFrom(typeof(ControllerType));
            Assert.IsTrue(validType);
            if (validType)
            {
                m_Controller = (ControllerType)value;
            }
        }
    }

    void Start()
    {
        RefreshView();
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
        OnWillHide();
        OnHidden();
    }

    public override void Show()
    {
        gameObject.SetActive(true);
        OnWillShow();
        OnShown();
    }

    protected abstract void RefreshView();

    protected virtual void OnWillShow() {}

    protected virtual void OnShown() {}

    protected virtual void OnWillHide() {}

    protected virtual void OnHidden() {}

    protected ControllerType GetController()
    {
        return m_Controller;
    }
}
