using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    private Queue<GameObject> waypoints;

    private int m_TotalWaypointCount;

    private GameObject nextWaypoint;

    private WaypointPointer waypointPointer;

    public int Count
    {
        get { return m_TotalWaypointCount; }
    }

    public int Completed
    {
        get { return m_TotalWaypointCount - waypoints.Count; }
    }

    public int Remaining
    {
        get { return waypoints.Count; }
    }

    // Start is called before the first frame update
    void Start()
    {
        waypointPointer = GameController.Instance.GetPlayer().GetComponentInChildren<WaypointPointer>();

        waypoints = new Queue<GameObject>();

        foreach (Transform child in transform)
        {
            Debug.Log("Add waypoint!");
            child.gameObject.SetActive(false);
            waypoints.Enqueue(child.gameObject);
        }

        m_TotalWaypointCount = waypoints.Count;
        
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
            GameController.GameObject.BroadcastMessage(GameMessages.MSG_WAYPOINT_REACHED, null, SendMessageOptions.DontRequireReceiver);
        }
        else
        // empty 
        {
            waypointPointer.target = null;
            Debug.Log("Finished Race!");
            GameController.GameObject.BroadcastMessage(GameMessages.MSG_RACE_FINISHED, null, SendMessageOptions.DontRequireReceiver);
        }
    }
}
