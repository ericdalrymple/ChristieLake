using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private TimeSpan m_TimeElapsed;

    private bool m_TrackTime = false;

    public int RemainingMotivation
    {
        get;
        private set;
    }

    public TimeSpan ElapsedTime
    {
        get { return m_TimeElapsed; }
    }

    public void Reset()
    {
        m_TimeElapsed = new TimeSpan(0, 0, 0);
    }

    void Update()
    {
        if (m_TrackTime)
        {
            // Update the race duration
            int seconds = (int)Time.deltaTime;
            int milliseconds = (int)(Time.deltaTime * 1000) - (seconds * 1000);
            m_TimeElapsed = m_TimeElapsed.Add(new TimeSpan(0, 0, seconds, milliseconds));

            // Drain motivation
        }
    }

    public void OnStartRace()
    {
        m_TrackTime = true;
    }

    public void OnFinishRace()
    {
        m_TrackTime = false;
    }
}
