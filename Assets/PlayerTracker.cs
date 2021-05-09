using UnityEngine;

// reference :: https://learn.unity.com/tutorial/controlling-unity-camera-behaviour#5fc3f6a3edbc2a459f91c6ae
public class PlayerTracker : MonoBehaviour
{
    [SerializeField]
    private Transform m_PlayerTransform = null;

    private GameObject target;
    
    [SerializeField]
    public float cameraHeight = 0.45f;
    [SerializeField]
    public float cameraDistance = 0.8f;
    [SerializeField]
    public float targetHeightOffset = 0.2f;

    //private MeshRenderer _renderer;
    //public float hideDistance = 1.5f;


    private void Start()
    {
        target = new GameObject("ahead");
        //_renderer = trackedObject.gameObject.GetComponent<MeshRenderer>();
    }

    private void LateUpdate()
    {
        float distance = Vector3.Distance(transform.position, m_PlayerTransform.position);
        float cameraSpeed = Mathf.Pow(distance / cameraDistance, 1.75f);

        target.transform.position = m_PlayerTransform.position + targetHeightOffset * Vector3.up;

        Vector3 newCameraPosition = m_PlayerTransform.position + cameraHeight * Vector3.up - cameraDistance * m_PlayerTransform.forward;
        transform.position = Vector3.MoveTowards(transform.position, newCameraPosition, cameraSpeed * Time.deltaTime);

        transform.LookAt(target.transform);
    }
}
