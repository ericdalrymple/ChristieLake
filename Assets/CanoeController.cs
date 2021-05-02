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
    FloatReference lateralSpeed = new FloatReference(1.0f);

    [SerializeField]
    FloatReference torque = new FloatReference(1.0f);


    [SerializeField]
    InputActionAsset inputActionAsset;

    [SerializeField]
    public Transform target;


    InputAction rowLeft;
    InputAction rowRight;

    InputAction rowForward;
    InputAction rowBackward;
    InputAction switchSides;


    private bool right = true;

    //private Animation m_RowAnimation;

    [SerializeField]
    private GameObject m_PaddleLeft;
    [SerializeField]
    private GameObject m_PaddleRight;

    // Start is called before the first frame update
    void Awake()
    {
        Assert.IsNotNull(inputActionAsset, "Need inputActionAsset");
        Assert.IsNotNull(m_PaddleLeft, "Canoe needs reference to Left paddle game object");
        Assert.IsNotNull(m_PaddleRight, "Canoe needs reference to Right paddle game object");
        m_PaddleLeft.SetActive(false);
        m_PaddleRight.SetActive(false);

        rb = GetComponentInChildren<Rigidbody>();

        //m_RowAnimation = GetComponentInChildren<Animation>(); // won't work: two paddles each have an animation. need to get from each paddle

        var gameplayActionMap = inputActionAsset.FindActionMap("Player");

        rowLeft = gameplayActionMap.FindAction("Row Left");
        rowRight = gameplayActionMap.FindAction("Row Right");

        rowForward = gameplayActionMap.FindAction("Row Forward");
        rowBackward = gameplayActionMap.FindAction("Row Backward");

        switchSides = gameplayActionMap.FindAction("Switch Sides");

        rowForward.performed += OnRowForward;
        rowBackward.performed += OnRowBackward;
        switchSides.performed += OnSwitchSides;

        rowLeft.performed += OnRowLeft;
        rowRight.performed += OnRowRight;
    }

    private void OnEnable()
    {
        rowLeft.Enable();
        rowRight.Enable();
        rowForward.Enable();
        rowBackward.Enable();
        switchSides.Enable();

    }

    private void OnDisable()
    {
        rowLeft.Disable();
        rowRight.Disable();
        rowForward.Disable();
        rowBackward.Disable();
        switchSides.Disable();
    }

    private void OnDestroy()
    {
        rowLeft.performed -= OnRowLeft;
        rowRight.performed -= OnRowRight;

        rowForward.performed -= OnRowForward;
        rowBackward.performed -= OnRowBackward;
        switchSides.performed -= OnSwitchSides;
    }

    // Update is called once per frame
    void Update()
    {
        print("Is upside down?" + IsUpsideDown());
    }


    private bool IsUpsideDown()
    {

        return Vector3.Dot(transform.up, Vector3.up) < -.9;

    }
    public void OnRowLeft(InputAction.CallbackContext context)
    {
        print("Row Left");
        rb.AddForce(speed.Value * transform.forward);
        rb.AddForce(0.2f * speed.Value * transform.right);
        rb.AddTorque(transform.up * torque.Value * 1f);
    }

    public void OnRowRight(InputAction.CallbackContext context)
    {
        print("Row Right");
        rb.AddForce(speed.Value * transform.forward);
        rb.AddForce(0.2f * speed.Value * transform.right * -1);
        rb.AddTorque(transform.up * torque.Value * -1f);
    }

    public void OnRowForward(InputAction.CallbackContext context)
    {
        StartCoroutine(Row(right));
        //m_RowAnimation.Play();
        Debug.Log("Row Forward " + ( right ? "Right" : "Left"));
        rb.AddForce(speed.Value * transform.forward);
        rb.AddForce(0.2f * lateralSpeed.Value * (right ? -1 * transform.right : transform.right ));
        rb.AddTorque(transform.up * torque.Value * (right ? -1 : 1));
    }

    public void OnRowBackward(InputAction.CallbackContext context)
    {
        StartCoroutine(Row(right));
        //m_RowAnimation.Play();
        Debug.Log("Row Forward " + (right ? "Right" : "Left"));
        rb.AddForce(speed.Value * transform.forward * -1);
        rb.AddForce(0.2f * lateralSpeed.Value * (right ? -1 * transform.right : transform.right ));
        rb.AddTorque(transform.up * torque.Value * (right ? 1 : -1 ));

    }

    public void OnSwitchSides(InputAction.CallbackContext context)
    {
        
        right = !right;
        StartCoroutine(Row(right));
        Debug.Log("Switch to " + (right ? "Right" : "Left"));
    }

    public IEnumerator Row(bool right)
    {
        m_PaddleLeft.SetActive(!right);
        m_PaddleRight.SetActive(right);
        yield return new WaitForSeconds(1);
        m_PaddleLeft.SetActive(false);
        m_PaddleRight.SetActive(false);
    }

        
}
