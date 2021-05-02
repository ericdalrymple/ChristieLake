using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;
using UnityEngine;

public class CanoeSelector : MonoBehaviour
{
    [SerializeField]
    InputActionAsset inputActionAsset;

    private InputAction m_SwitchCanoeAction;

    private Queue<GameObject> m_CanoeModels;
    // Start is called before the first frame update
    void Awake()
    {

        Assert.IsNotNull(inputActionAsset, "CanoeSelector needs input action asset");

        var gameplayActionMap = inputActionAsset.FindActionMap("Player");
        Assert.IsNotNull(gameplayActionMap, "CanoeSelector couldn't find gameplay action map?");
        m_SwitchCanoeAction = gameplayActionMap.FindAction("Switch Canoe");
        Assert.IsNotNull(m_SwitchCanoeAction, "CanoeSelector couldn't find switch canoe action");


        m_SwitchCanoeAction.performed += ToggleNext;

        m_CanoeModels = new Queue<GameObject>();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            m_CanoeModels.Enqueue(child.gameObject);
        }
        m_CanoeModels.Peek().SetActive(true);
    }

    private void OnEnable()
    {
        m_SwitchCanoeAction.Enable();
    }

    private void OnDisable()
    {
        m_SwitchCanoeAction.Disable();
    }

    private void OnDestroy()
    {
        m_SwitchCanoeAction.performed -= ToggleNext;
    }
    public void ToggleNext(InputAction.CallbackContext context)
    {
        if (m_CanoeModels.Count > 0)
        {
            m_CanoeModels.Peek().SetActive(false);
            m_CanoeModels.Enqueue(m_CanoeModels.Dequeue());
            m_CanoeModels.Peek().SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
