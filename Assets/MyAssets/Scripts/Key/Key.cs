using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    private Rigidbody objectRigidBody;
    private Transform keyGrabPoint;

    public ParticleSystem particles;
    private ParticleSystem particlesInstance;

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        if (particles != null)
        {
            particlesInstance = Instantiate(particles, transform.position, transform.rotation);
            particlesInstance.transform.parent = gameObject.transform;
            particlesInstance.transform.localScale = Vector3.one;
        }
    }

    private void Update()
    {
        if (keyGrabPoint != null)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.Rotate(-3.234f, -5.2f, 3);
        }
    }

    public void Interact(Transform grabPoint)
    {
        // Asign key to Player
        transform.parent = grabPoint.transform;
        Vector3 parentCenter = grabPoint.transform.position;
        objectRigidBody.useGravity = false;
        this.keyGrabPoint = grabPoint;

        // Delete particles
        if (particlesInstance != null)
        {
            Destroy(particlesInstance);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            GameManager.Instance.NextRound();
        }
    }
}
