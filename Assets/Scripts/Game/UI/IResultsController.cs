using System;

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

    public void GoToRetry();
    public void GoToSubmit();
}
