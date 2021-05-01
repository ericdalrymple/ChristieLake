using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PocketValues.Types;

public class CanoeController : MonoBehaviour
{

    Rigidbody rb;

    [SerializeField]
    float speed = 1.0f;
    //FloatReference speed = ;

    [SerializeField]
    float torque = 1.0f;
    //FloatReference torque

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            rb.AddForce(speed * Vector3.forward);
            rb.AddTorque(transform.up * torque * 1);
        }

        if (Input.GetKey("d"))
        {
            rb.AddForce(speed * Vector3.forward);
            rb.AddTorque(transform.up * torque * -1);
        }
    }
}
