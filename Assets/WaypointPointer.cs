using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPointer : MonoBehaviour
{

    public GameObject target;
    public GameObject m_Player;

    private void Start()
    {
        m_Player = GameController.Instance.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (target is null) {
            gameObject.SetActive(false);
        }
        else {
            Debug.Log("Target position: " + target.transform.position.ToString());
            Vector3 v = 0.2f*(target.transform.position - transform.position).normalized;
            transform.position = m_Player.transform.position + Vector3.MoveTowards(transform.position, v, 500);
            Debug.Log("Current position: " + transform.position.ToString());
            gameObject.transform.LookAt(target.transform);
        }
    }
}
