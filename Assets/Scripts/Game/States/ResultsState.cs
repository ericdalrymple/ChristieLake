using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsState : GameState, IUIController
{
    [SerializeField]
    private UIHandle m_ResultViewHandle;

    public override void Enter()
    {
        GameController.UIManager.ShowDialog(m_ResultViewHandle, GameController.Instance);
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
    }
}
