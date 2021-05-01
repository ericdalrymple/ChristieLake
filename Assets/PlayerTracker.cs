using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// reference :: https://learn.unity.com/tutorial/controlling-unity-camera-behaviour#5fc3f6a3edbc2a459f91c6ae
public class PlayerTracker : MonoBehaviour
{
    public GameObject trackedObject;
    public float maxDistance = 10;
    public float moveSpeed = 100;
    public float updateSpeed = 100;

    [Range(0, 10)]
    public float currentDistance = 5;
    private GameObject ahead;
    private MeshRenderer _renderer;
    public float hideDistance = 1.5f;

    private void Start()
    {
        ahead = new GameObject("ahead");
        //_renderer = trackedObject.gameObject.GetComponent<MeshRenderer>();
    }

    private void LateUpdate()
    {
        float cameraDistance = Vector3.Distance(transform.position, trackedObject.transform.position);
        float cameraSpeed = cameraDistance / maxDistance;

        ahead.transform.position = trackedObject.transform.position + trackedObject.transform.forward * (maxDistance * 0.25f);
        Vector3 newCameraPosition = trackedObject.transform.position + Vector3.up * currentDistance  - trackedObject.transform.forward * (currentDistance + maxDistance * 0.5f);
        transform.position = Vector3.MoveTowards(transform.position, newCameraPosition, cameraSpeed* updateSpeed * Time.deltaTime);

        transform.LookAt(ahead.transform);
    }
}
