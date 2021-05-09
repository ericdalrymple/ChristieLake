using UnityEngine;

public class WaypointPointer : MonoBehaviour
{
    private GameObject m_Target;

    public void OnWaypointChanged()
    {
        m_Target = GameController.Instance.NextWaypoint;
    }

    private void Start()
    {
        m_Target = GameController.Instance.NextWaypoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Target == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Target position: " + m_Target.transform.position.ToString());
            Vector3 v = 0.2f*(m_Target.transform.position - transform.position).normalized;
            transform.position = transform.parent.position + Vector3.MoveTowards(transform.position, v, 500);
            Debug.Log("Current position: " + transform.position.ToString());
            gameObject.transform.LookAt(m_Target.transform);
        }
    }
}
