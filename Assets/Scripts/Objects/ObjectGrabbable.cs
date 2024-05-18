using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidBody;
    private Transform objectGrabPointTransform;
    public string value = "Test";
    private float scrollSensitivity = 150f;
    private float lerpSpeed = 10f;
    private float minDistance = 1f;
    private float maxDistance = 3f;
    private float currentDistance = 2f; 

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidBody.useGravity = false;
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        objectRigidBody.useGravity = true;
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            float scrollWheelValue = Input.GetAxis("Mouse ScrollWheel"); // Obtain 0.1 or -0.1 depending on scroll mouse wheel
            
            currentDistance += scrollWheelValue * scrollSensitivity * Time.deltaTime;

            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance); 

            Camera mainCamera = Camera.main;
            Vector3 targetPosition = mainCamera.transform.position + mainCamera.transform.forward * currentDistance;
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);

            objectRigidBody.MovePosition(newPosition);
        }
    }
}
