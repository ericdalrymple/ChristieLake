using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResultsController : IUIController
{
    int CurrentScore
    {
        get;
    }

    TimeSpan TimeElapsed
    {
        get;
    }
}
