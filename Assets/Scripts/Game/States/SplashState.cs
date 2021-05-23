using UnityEngine;
using UnityEngine.InputSystem;

public class SplashState : GameState, IUIController
{
    [SerializeField]
    private GameStateHandle m_StartGameState = null;

    [SerializeField]
    private UIHandle m_SplashViewHandle = null;

    [SerializeField]
    private InputActionAsset m_InputActions = null;

    public override bool AllowGameplayInput
    {
        get { return false; }
    }

    public string GameTitle
    {
        get
        {
            return GameController.GameTitle;
        }
    }

    public override void Enter()
    {
        GameController.UIManager.ShowDialog(m_SplashViewHandle, this);

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
        GameController.UIManager.ClearViews();

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
        StateMachine.SetCurrentState(m_StartGameState);
    }
}
