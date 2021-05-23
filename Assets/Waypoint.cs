using System;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Serializable]
    protected struct WaypointScaling
    {
        public float MinScale;
        public float MaxScale;

        [Range(0.1f, 1000.0f)]
        public float MaxDistance;
    }

    [SerializeField]
    private Transform m_MeshTransform;

    [SerializeField]
    private WaypointScaling m_Scaling;

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

    void Update()
    {
        // Scale according to player distance
        if (m_MeshTransform != null)
        {
            GameObject player = GameController.Instance.GetPlayer();
            float playerDistance = Vector3.Distance(player.transform.position, transform.position);
            playerDistance = Mathf.Min(m_Scaling.MaxDistance, playerDistance);

            float normalizedPlayerDistance = playerDistance / m_Scaling.MaxDistance;
            float scale = m_Scaling.MinScale + (normalizedPlayerDistance * (m_Scaling.MaxScale - m_Scaling.MinScale));
            m_MeshTransform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
