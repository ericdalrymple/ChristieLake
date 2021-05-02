using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private float m_SessionStartTime;
    private float m_LastTime;

    private bool m_TrackTime = false;

    public int RemainingMotivation
    {
        get;
        private set;
    }

    public TimeSpan ElapsedTime
    {
        get
        {
            float elapsed = m_LastTime - m_SessionStartTime;
            return TimeSpan.FromSeconds(elapsed);
        }
    }

    public void Reset()
    {
        m_SessionStartTime = 0.0f;
    }

    void Update()
    {
        if (m_TrackTime)
        {
            // Update the race duration
            m_LastTime = Time.time;

            // Drain motivation
        }
    }

    public void OnStartRace()
    {
        m_TrackTime = true;
        m_SessionStartTime = m_LastTime = Time.time;
    }

    public void OnFinishRace()
    {
        m_TrackTime = false;
    }
}
