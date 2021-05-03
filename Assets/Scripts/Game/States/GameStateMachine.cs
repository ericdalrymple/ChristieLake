using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : SingletonBehaviour<GameStateMachine>
{
    [SerializeField]
    private GameStateHandle m_InitialState;

    protected Dictionary<string, GameState> m_StateLookup = new Dictionary<string, GameState>();

    protected GameState m_CurrentState;

    public void Add(GameStateHandle handle, GameState state)
    {
        if (handle != null)
        {
            Debug.Log("Add state: " + state.ToString());
            m_StateLookup.Add(handle.Value, state);
        }
    }

    public GameState GetState(GameStateHandle handle)
    {
        if (handle != null)
        {
            GameState state;
            if (m_StateLookup.TryGetValue(handle.Value, out state))
            {
                return state;
            }
        }

        return null;
    }

    public void SetCurrentState(GameStateHandle handle)
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.Exit();
        }

        m_CurrentState = GetState(handle);

        if (m_CurrentState != null)
        {
            m_CurrentState.Enter();
        }
    }

    void Awake()
    {
        // Register states
        GameState[] states = gameObject.GetComponentsInChildren<GameState>(false);
        foreach (GameState state in states)
        {
            // Register
            m_StateLookup.Add(state.Handle, state);
        }
    }

    void Start()
    {
        if (m_InitialState != null)
        {
            SetCurrentState(m_InitialState);
        }
    }

    void Update()
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.Update();
        }
    }

    void FixedUpdate()
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.FixedUpdate();
        }
    }
}
