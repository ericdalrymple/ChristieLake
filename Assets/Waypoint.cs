using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private Animator m_BobAnimator;

    // Start is called before the first frame update
    void Start()
    {
        m_BobAnimator = GetComponent<Animator>();
        m_BobAnimator.Play("WaypointBob");

        //Debug.Log("Trigger on");
        // optionally, rotate buoy model
        //transform.rotation = Random.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Waypoint reached!");
        transform.parent.BroadcastMessage(GameMessages.MSG_WAYPOINT_REACHED, null, SendMessageOptions.DontRequireReceiver);
    }
}
