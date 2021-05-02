using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResultsState : GameState, IUIController
{
    [SerializeField]
    private GameStateHandle m_StartGameState;

    [SerializeField]
    private UIHandle m_ResultViewHandle;

    [SerializeField]
    private InputActionAsset m_InputActions;

    private InputAction m_ConfirmInput;

    public override void Enter()
    {
        GameController.UIManager.ShowDialog(m_ResultViewHandle, GameController.Instance);

        // Bind confirm to all keys
        InputActionMap gameplayActionMap = m_InputActions.FindActionMap("UI");
        foreach (InputAction action in gameplayActionMap.actions)
        {
            if (action.type == InputActionType.Button)
            {
                action.performed += OnConfirmInput;
            }
        }

        gameplayActionMap = m_InputActions.FindActionMap("Player");
        foreach (InputAction action in gameplayActionMap.actions)
        {
            if (action.type == InputActionType.Button)
            {
                action.performed += OnConfirmInput;
            }
        }
    }

    public override void Exit()
    {
        // Unbind confirm from all keys
        InputActionMap gameplayActionMap = m_InputActions.FindActionMap("UI");
        foreach (InputAction action in gameplayActionMap.actions)
        {
            if (action.type == InputActionType.Button)
            {
                action.performed -= OnConfirmInput;
            }
        }

        gameplayActionMap = m_InputActions.FindActionMap("Player");
        foreach (InputAction action in gameplayActionMap.actions)
        {
            if (action.type == InputActionType.Button)
            {
                action.performed -= OnConfirmInput;
            }
        }
    }

    public void OnConfirmInput(InputAction.CallbackContext context)
    {
        GameController.Instance.ResetGame();
        StateMachine.SetCurrentState(m_StartGameState);
    }
}
