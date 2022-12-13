using System;

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

    float MotivationPercent
    {
        get;
    }

    int CurrentWaypoint
    {
        get;
    }

    int WaypointCount
    {
        get;
    }

    TimeSpan TimeElapsed
    {
        get;
    }
}
