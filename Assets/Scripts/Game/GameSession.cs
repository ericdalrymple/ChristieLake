using PocketValues.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameController))]
public class GameSession : MonoBehaviour
{
    [SerializeField]
    private IntegerReference m_MaxMotivation = new IntegerReference();

    [SerializeField]
    private FloatReference m_MotivationDecayRate = new FloatReference(10.0f);

    [SerializeField]
    private IntegerReference m_BuoyScore = new IntegerReference(250);

    private float m_LastTime;
    private float m_Motivation;
    private float m_Score;
    private float m_SessionStartTime;
    private bool m_TrackTime = false;

    private GameController m_Controller;

    public int MaxMotivation
    {
        get { return m_MaxMotivation.Value; }
    }

    public int CurrentMotivation
    {
        get { return (int)m_Motivation; }
    }

    public int CurrentScore
    {
        get { return (int)m_Score; }
    }

    private float LastTime
    {
        get { return m_LastTime; }
        set
        {
            m_LastTime = value;
            m_Controller.NotifyTimeChanged();
        }
    }

    private float Motivation
    {
        get { return m_Motivation; }
        set
        {
            int previousMotivation = (int)m_Motivation;
            m_Motivation = value;

            if ((int)m_Motivation != previousMotivation)
            {
                m_Controller.NotifyMotivationChanged();
            }
        }
    }

    private float MotivationPercent
    {
        get
        {
            return (m_MaxMotivation.Value == 0) ?
                0 :
                Motivation / (float)m_MaxMotivation.Value;
        }
    }

    private float Score
    {
        get { return m_Score; }
        set
        {
            int previousScore = (int)m_Score;
            m_Score = value;

            if ((int)m_Score != previousScore)
            {
                m_Controller.NotifyScoreChanged();
            }
        }
    }

    private float StartTime
    {
        get { return m_SessionStartTime; }
        set
        {
            if (m_SessionStartTime != value)
            {
                m_SessionStartTime = value;
                m_Controller.NotifyTimeChanged();
            }
        }
    }

    public TimeSpan ElapsedTime
    {
        get
        {
            float elapsed = LastTime - StartTime;
            return TimeSpan.FromSeconds(elapsed);
        }
    }

    public void ResetValues()
    {
        Motivation = m_MaxMotivation.Value;
        Score = 0.0f;
        StartTime = 0.0f;
        LastTime = 0.0f;
    }

    private void Awake()
    {
        m_Controller = GetComponent<GameController>();
    }

    void Update()
    {
        if (m_TrackTime)
        {
            // Update the race duration
            LastTime = Time.time;

            // Decay motivation
            float decayAmount = m_MotivationDecayRate.Value * Time.deltaTime;
            Motivation -= decayAmount;
            if (Motivation < 0.0f)
            {
                Motivation = 0.0f;
            }
        }
    }

    public void OnStartRace()
    {
        m_TrackTime = true;
        StartTime = LastTime = Time.time;
    }

    public void OnFinishRace()
    {
        m_TrackTime = false;

        // Score the buoy
        Score += (int)(m_BuoyScore.Value * (1.0f + MotivationPercent));
    }

    public void OnWaypointChanged()
    {
        // Score the buoy
        Score += (int)(m_BuoyScore.Value * (1.0f + MotivationPercent));

        // Fill up motivation
        Motivation = m_MaxMotivation.Value;
    }
}
