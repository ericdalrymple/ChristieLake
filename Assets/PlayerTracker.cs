using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// reference :: https://learn.unity.com/tutorial/controlling-unity-camera-behaviour#5fc3f6a3edbc2a459f91c6ae
public class PlayerTracker : MonoBehaviour
{
    public GameObject trackedObject;
    //public float maxDistance = 10;
    //public float moveSpeed = 100;
    //public float updateSpeed = 1000;

//    [Range(0, 10)]
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
        float distance = Vector3.Distance(transform.position, trackedObject.transform.position);
        float cameraSpeed = distance / cameraDistance;

        target.transform.position = trackedObject.transform.position + targetHeightOffset * Vector3.up;

        Vector3 newCameraPosition = trackedObject.transform.position + cameraHeight * Vector3.up - cameraDistance  * trackedObject.transform.forward;
        transform.position = Vector3.MoveTowards(transform.position, newCameraPosition, cameraSpeed * Time.deltaTime);

        transform.LookAt(target.transform);
    }
}
