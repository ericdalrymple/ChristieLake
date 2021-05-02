using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    private Queue<GameObject> waypoints;

    private GameObject nextWaypoint;

    private WaypointPointer waypointPointer;

    // Start is called before the first frame update
    void Start()
    {

        waypointPointer = GameController.Instance.GetPlayer().GetComponentInChildren<WaypointPointer>();

        waypoints = new Queue<GameObject>();

        foreach (Transform child in transform)
        {
            print("Add waypoint!");
            child.gameObject.SetActive(false);
            waypoints.Enqueue(child.gameObject);
        }
        
        nextWaypoint = waypoints.Dequeue();
        nextWaypoint.SetActive(true);

        waypointPointer.target = nextWaypoint;
          
    }


    public void OnReachWaypoint()
    {
        print("Reached waypoint!");
        nextWaypoint?.SetActive(false);

        // if not empty
        if (waypoints.Count > 0)
        {
            nextWaypoint = waypoints.Dequeue();
            waypointPointer.target = nextWaypoint;
            nextWaypoint.SetActive(true);
        }
        else
        // empty 
        {
            waypointPointer.target = null;
            Debug.Log("Finished Race!");
            GameController.Instance?.OnFinishRace();
            SendMessageUpwards("OnFinishRace", null, SendMessageOptions.DontRequireReceiver);
        }
    }
}
