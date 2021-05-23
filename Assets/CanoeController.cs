using System.Collections;
using UnityEngine;
using PocketValues.Types;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class CanoeController : MonoBehaviour
{
    [SerializeField]
    FloatReference m_Speed = new FloatReference(1.0f);

    [SerializeField]
    FloatReference m_LateralSpeed = new FloatReference(1.0f);

    [SerializeField]
    FloatReference m_Torque = new FloatReference(1.0f);

    [SerializeField]
    InputActionAsset m_InputActionAsset = null;

    [SerializeField]
    private GameObject m_PaddleLeft = null;

    [SerializeField]
    private GameObject m_PaddleRight = null;

    private InputAction m_RowLeft;
    private InputAction m_RowRight;
    private InputAction m_RowForward;
    private InputAction m_RowBackward;
    private InputAction m_SwitchSides;

    private bool m_IsPaddleOnRightSide = true;
    private Rigidbody m_Body;

    // Start is called before the first frame update
    void Awake()
    {
        Assert.IsNotNull(m_InputActionAsset, "Need inputActionAsset");
        Assert.IsNotNull(m_PaddleLeft, "Canoe needs reference to Left paddle game object");
        Assert.IsNotNull(m_PaddleRight, "Canoe needs reference to Right paddle game object");
        m_PaddleLeft.SetActive(false);
        m_PaddleRight.SetActive(false);

        m_Body = GetComponentInChildren<Rigidbody>();

        //m_RowAnimation = GetComponentInChildren<Animation>(); // won't work: two paddles each have an animation. need to get from each paddle

        InputActionMap gameplayActionMap = m_InputActionAsset.FindActionMap("Player");

        m_RowLeft = gameplayActionMap.FindAction("Row Left");
        m_RowRight = gameplayActionMap.FindAction("Row Right");

        m_RowForward = gameplayActionMap.FindAction("Row Forward");
        m_RowBackward = gameplayActionMap.FindAction("Row Backward");

        m_SwitchSides = gameplayActionMap.FindAction("Switch Sides");

        m_RowForward.performed += OnRowForward;
        m_RowBackward.performed += OnRowBackward;
        m_SwitchSides.performed += OnSwitchSides;

        m_RowLeft.performed += OnRowLeft;
        m_RowRight.performed += OnRowRight;
    }

    private void OnEnable()
    {
        m_RowLeft.Enable();
        m_RowRight.Enable();
        m_RowForward.Enable();
        m_RowBackward.Enable();
        m_SwitchSides.Enable();

    }

    private void OnDisable()
    {
        m_RowLeft.Disable();
        m_RowRight.Disable();
        m_RowForward.Disable();
        m_RowBackward.Disable();
        m_SwitchSides.Disable();
    }

    private void OnDestroy()
    {
        m_RowLeft.performed -= OnRowLeft;
        m_RowRight.performed -= OnRowRight;

        m_RowForward.performed -= OnRowForward;
        m_RowBackward.performed -= OnRowBackward;
        m_SwitchSides.performed -= OnSwitchSides;
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
        if (!GameController.GameplayInputEnabled)
        {
            return;
        }

        //print("Row Left");
        m_Body.AddForce(m_Speed.Value * transform.forward);
        m_Body.AddForce(0.2f * m_Speed.Value * transform.right);
        m_Body.AddTorque(transform.up * m_Torque.Value * 1f);
    }

    public void OnRowRight(InputAction.CallbackContext context)
    {
        if (!GameController.GameplayInputEnabled)
        {
            return;
        }

        //print("Row Right");
        m_Body.AddForce(m_Speed.Value * transform.forward);
        m_Body.AddForce(0.2f * m_Speed.Value * transform.right * -1);
        m_Body.AddTorque(transform.up * m_Torque.Value * -1f);
    }

    public void OnRowForward(InputAction.CallbackContext context)
    {
        if (!GameController.GameplayInputEnabled)
        {
            return;
        }

        StartCoroutine(Row(m_IsPaddleOnRightSide));
        //m_RowAnimation.Play();
        //Debug.Log("Row Forward " + ( right ? "Right" : "Left"));
        m_Body.AddForce(m_Speed.Value * transform.forward);
        m_Body.AddForce(0.2f * m_LateralSpeed.Value * (m_IsPaddleOnRightSide ? -1 * transform.right : transform.right ));
        m_Body.AddTorque(transform.up * m_Torque.Value * (m_IsPaddleOnRightSide ? -1 : 1));
    }

    public void OnRowBackward(InputAction.CallbackContext context)
    {
        if (!GameController.GameplayInputEnabled)
        {
            return;
        }

        StartCoroutine(Row(m_IsPaddleOnRightSide));
        //m_RowAnimation.Play();
        //Debug.Log("Row Forward " + (right ? "Right" : "Left"));
        m_Body.AddForce(m_Speed.Value * transform.forward * -1);
        m_Body.AddForce(0.2f * m_LateralSpeed.Value * (m_IsPaddleOnRightSide ? -1 * transform.right : transform.right ));
        m_Body.AddTorque(transform.up * m_Torque.Value * (m_IsPaddleOnRightSide ? 1 : -1 ));

    }

    public void OnSwitchSides(InputAction.CallbackContext context)
    {
        if (!GameController.GameplayInputEnabled)
        {
            return;
        }

        m_IsPaddleOnRightSide = !m_IsPaddleOnRightSide;
        StartCoroutine(Row(m_IsPaddleOnRightSide));
        Debug.Log("Switch to " + (m_IsPaddleOnRightSide ? "Right" : "Left"));
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
