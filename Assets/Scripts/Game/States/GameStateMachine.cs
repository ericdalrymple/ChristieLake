using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameStateMachine : SingletonBehaviour<GameStateMachine>
{
    [SerializeField]
    private GameStateHandle m_InitialState = null;

    [SerializeField]
    private GameStateHandle m_RetryState = null;

    protected Dictionary<string, GameState> m_StateLookup = new Dictionary<string, GameState>();

    protected GameState m_CurrentState;

    public bool GameplayInputEnabled
    {
        get
        {
            return m_CurrentState ? m_CurrentState.AllowGameplayInput : true;
        }
    }

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

    public void Initialize()
    {
        // Register states
        GameState[] states = gameObject.GetComponentsInChildren<GameState>(false);
        foreach (GameState state in states)
        {
            // Register
            m_StateLookup.Add(state.Handle, state);
        }
    }

    public void StartRetry()
    {
        SetCurrentState(m_RetryState);
    }

    public void Tick()
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.Tick();
        }
    }

    void Awake()
    {
        Assert.IsNotNull(m_InitialState, "Must specify an initial state.");
        Assert.IsNotNull(m_RetryState, "Must specify the state the game goes to on retry.");
    }

    void Start()
    {
        SetCurrentState(m_InitialState);
    }

    void FixedUpdate()
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.FixedUpdate();
        }
    }
}
