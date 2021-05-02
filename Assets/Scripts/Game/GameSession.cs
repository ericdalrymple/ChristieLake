using PocketValues.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField]
    private IntegerReference m_MaxMotivation = new IntegerReference();

    [SerializeField]
    private FloatReference m_MotivationDecayRate = new FloatReference(10.0f);

    private float m_LastTime;
    private float m_Motivation;
    private float m_SessionStartTime;

    private bool m_TrackTime = false;

    public int MaxMotivation
    {
        get { return m_MaxMotivation.Value; }
    }

    public int CurrentMotivation
    {
        get { return (int)m_Motivation; }
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
        m_Motivation = m_MaxMotivation.Value;
        m_SessionStartTime = 0.0f;
    }

    void Update()
    {
        if (m_TrackTime)
        {
            // Update the race duration
            m_LastTime = Time.time;

            // Decay motivation
            float decayAmount = m_MotivationDecayRate.Value * Time.deltaTime;
            m_Motivation -= decayAmount;
            if (m_Motivation < 0.0f)
            {
                m_Motivation = 0.0f;
            }
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
