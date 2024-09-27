using UnityEngine;

public class Mirror : MonoBehaviour, IInteractable
{
    public ParticleSystem particles;
    private ParticleSystem particlesInstance;
    private Transform grabPoint;
    private Rigidbody objectRigidBody;
    private Collider objectCollider;
    private bool isLooking = false;
    public float distance = 0.1f;

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();
        if (particles != null)
        {
            particlesInstance = Instantiate(particles, this.transform.position, this.transform.rotation);
            particlesInstance.transform.parent = gameObject.transform;
            particlesInstance.transform.localScale = Vector3.one;
        }
    }

    private void Update()
    {
        if (grabPoint != null) // When the mirror is equiped
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
    public void Interact(Transform grabPoint)
    {
        this.grabPoint = grabPoint.transform;
        // Asign mirror to Player
        transform.parent = grabPoint.transform;
        Vector3 parentCenter = grabPoint.transform.position;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        objectRigidBody.useGravity = false;
        objectCollider.enabled = false;

        // Delete particles
        if (particlesInstance != null)
        {
            Destroy(particlesInstance);
        }
    }

    private void ResetPosition()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
