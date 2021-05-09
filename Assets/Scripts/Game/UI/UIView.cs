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
            bool validType = typeof(ControllerType).IsAssignableFrom(value.GetType());
            Assert.IsTrue(validType);
            if (validType)
            {
                m_Controller = (ControllerType)value;
            }
        }
    }

    protected ControllerType GetController()
    {
        return m_Controller;
    }
}
