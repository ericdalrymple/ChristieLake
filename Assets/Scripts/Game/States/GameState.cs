using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    [SerializeField]
    private GameStateHandle m_Handle;

    public string Handle
    {
        get { return m_Handle != null ? m_Handle.Value : string.Empty; }
    }

    protected GameStateMachine StateMachine
    {
        get;
        private set;
    }

    void Awake()
    {
        StateMachine = GetComponentInParent<GameStateMachine>();
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}
