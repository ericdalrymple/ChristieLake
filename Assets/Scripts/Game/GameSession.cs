using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private TimeSpan m_TimeElapsed;

    public int RemainingMotivation
    {
        get;
        private set;
    }

    public void Reset()
    {
        m_TimeElapsed = new TimeSpan(0, 0, 0);
    }

    public TimeSpan Elapsed()
    {
        return m_TimeElapsed;
    }

    void Update()
    {
        // Update the race duration
        int seconds = (int)Time.deltaTime;
        int milliseconds = (int)(Time.deltaTime * 1000) - (seconds * 1000);
        m_TimeElapsed.Add(new TimeSpan(0, 0, seconds, milliseconds));

        // Drain motivation

    }

    
}
