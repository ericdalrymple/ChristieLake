using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PocketValues.Types;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class CanoeController : MonoBehaviour
{

    Rigidbody rb;

    [SerializeField]
    FloatReference speed = new FloatReference(1.0f);

    [SerializeField]
    FloatReference torque = new FloatReference(1.0f);
    
    [SerializeField]
    InputActionAsset inputActionAsset;

    [SerializeField]
    public Transform target;


    InputAction rowLeft;
    InputAction rowRight;

    
    // Start is called before the first frame update
    void Awake()
    {
        Assert.IsNotNull(inputActionAsset, "Need inputActionAsset");
        rb = GetComponentInChildren<Rigidbody>();

        var gameplayActionMap = inputActionAsset.FindActionMap("Player");

        rowLeft = gameplayActionMap.FindAction("Row Left");
        rowRight = gameplayActionMap.FindAction("Row Right");

        rowLeft.performed += OnRowLeft;
        rowRight.performed += OnRowRight;
    }

    private void OnEnable()
    {
        rowLeft.Enable();
        rowRight.Enable();
    }

    private void OnDisable()
    {
        rowLeft.Disable();
        rowRight.Disable();
    }

    private void OnDestroy()
    {
        rowLeft.performed -= OnRowLeft;
        rowRight.performed -= OnRowRight;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey("a"))
        {
            rb.AddForce(speed.Value * Vector3.forward);
            rb.AddTorque(transform.up * torque.Value * 1f);
        }

        if (Input.GetKey("d"))
        {
            rb.AddForce(speed.Value * Vector3.forward);
            rb.AddTorque(transform.up * torque.Value * -1f);
        }
        */
    }

    public void OnRowLeft(InputAction.CallbackContext context)
    {
        print("Row Left");
        rb.AddForce(speed.Value * Vector3.forward);
        rb.AddTorque(transform.up * torque.Value * 1f);
    }

    public void OnRowRight(InputAction.CallbackContext context)
    {
        print("Row Right");
        rb.AddForce(speed.Value * Vector3.forward);
        rb.AddTorque(transform.up * torque.Value * -1f);
    }
}
