using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;

    private Rigidbody objectRigidBody;
    private Transform objectGrabPointTransform;

    public string value = "Test"; // Value for Scanner

    private float scrollSensitivity = 150f;
    private float lerpSpeed = 10f;
    private float minDistance = 1f;
    private float maxDistance = 3f;
    private float currentDistance = 2f; 

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;

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

    public void resetTransform()
    {
        // Default transform
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;

        // If objects has movement
        objectRigidBody.velocity = Vector3.zero;
        objectRigidBody.angularVelocity = Vector3.zero;
    }
}
