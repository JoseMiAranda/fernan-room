using UnityEngine;

public class Mirror : MonoBehaviour
{
    private Transform grabPoint;
    private Rigidbody objectRigidBody;
    private Collider objectCollider;
    private bool isLooking = false;
    public float distance = 0.1f;

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if(grabPoint != null) // When the mirros is equiped
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                isLooking = !isLooking;
                if (isLooking)
                {
                    this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + distance));
                    this.transform.localRotation = Quaternion.Euler(-90f, 0, 0);
                }
                else
                {
                    ResetPosition();
                }
            }
        }
    }

    public void Grab(GameObject grabPoint)
    {
        transform.parent = grabPoint.transform;
        ResetPosition();
        objectRigidBody.useGravity = false;
        objectCollider.enabled = false;
        this.grabPoint = grabPoint.transform;
    }

    private void ResetPosition()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
