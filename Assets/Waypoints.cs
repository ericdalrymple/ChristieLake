using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    private Queue<GameObject> waypoints;

    private GameObject nextWaypoint;

    // Start is called before the first frame update
    void Start()
    {

        waypoints = new Queue<GameObject>();

        foreach (Transform child in transform)
        {
            print("Add waypoint!");
            waypoints.Enqueue(child.gameObject);
        }
        nextWaypoint = waypoints.Dequeue();
        nextWaypoint.SetActive(true);

    }


    public void OnReachWaypoint()
    {
        print("Reached waypoint!");
        nextWaypoint?.SetActive(false);
        nextWaypoint = waypoints.Dequeue();
        nextWaypoint.SetActive(true);
    }
}
