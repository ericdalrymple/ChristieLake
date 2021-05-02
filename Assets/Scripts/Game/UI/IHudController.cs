using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHudController : IUIController
{
    int CurrentScore
    {
        get;
    }

    int CurrentMotivation
    {
        get;
    }

    int MaxMotivation
    {
        get;
    }

    float MotivationPercent
    {
        get;
    }

    TimeSpan TimeElapsed
    {
        get;
    }
}
