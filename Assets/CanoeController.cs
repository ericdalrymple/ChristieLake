using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PocketValues.Types;

public class CanoeController : MonoBehaviour
{

    Rigidbody rb;

    [SerializeField]
    FloatReference speed = new FloatReference(1.0f);

    [SerializeField]
    FloatReference torque = new FloatReference(1.0f);

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
            rb.AddForce(speed.Value * Vector3.forward);
            rb.AddTorque(transform.up * torque.Value * 1f);
        }

        if (Input.GetKey("d"))
        {
            rb.AddForce(speed.Value * Vector3.forward);
            rb.AddTorque(transform.up * torque.Value * -1f);
        }
    }
}
