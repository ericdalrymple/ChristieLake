using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHudController : IUIController
{
    int CurrentScore
    {
        get;
    }
}
