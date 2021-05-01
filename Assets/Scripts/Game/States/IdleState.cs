using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GameState, IUIController
{
    [SerializeField]
    private UIHandle m_HudHandle;

    public override void Enter()
    {
        GameController.UIManager.ShowDialog(m_HudHandle, GameController.Instance);
    }

    public override void Exit() { }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
    }
}
