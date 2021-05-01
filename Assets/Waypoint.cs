using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private Collider closeEnough;
    private Waypoints course;

    // Start is called before the first frame update
    void Start()
    {
        closeEnough = GetComponent<Collider>();
        Debug.Log("Trigger on");        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Waypoint reached!");
        BroadcastMessage("OnReachWaypoint", null, SendMessageOptions.DontRequireReceiver);
    }
}
