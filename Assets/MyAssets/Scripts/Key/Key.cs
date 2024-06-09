using UnityEngine;

public class Key : MonoBehaviour
{

    private Rigidbody objectRigidBody;
    private Collider objectCollider;
    private GameObject keyGrabPoint;

    public ParticleSystem particles;
    private ParticleSystem particlesInstance;

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();
        if (particles != null)
        {
            particlesInstance = Instantiate(particles, transform.position, transform.rotation);
            particlesInstance.transform.parent = gameObject.transform;
            particlesInstance.transform.localScale = Vector3.one;
        }
    }

    public void Grab(GameObject grabPoint)
    {
        // Asign key to Player
        transform.parent = grabPoint.transform;
        Vector3 parentCenter = grabPoint.transform.position;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.Rotate(-3.234f, -5.2f, 3);
        objectRigidBody.useGravity = false;
        this.keyGrabPoint = grabPoint;

        // Delete particles
        if (particlesInstance != null)
        {
            Destroy(particlesInstance);
        }
    }
}
