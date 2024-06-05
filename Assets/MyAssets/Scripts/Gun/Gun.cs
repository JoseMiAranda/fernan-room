using UnityEngine;

public class Gun : MonoBehaviour
{
    private Rigidbody objectRigidBody;
    private GameObject gunGrabPoint;
    private Collider objectCollider;

    public Transform spawnPoint;
    public GameObject bullet;
    public ParticleSystem particles;
    private ParticleSystem particlesInstance;
    public float shotForce = 1500f;
    public float shotRate = 0.5f;
    private float shotRateTime = 0;
    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();
        if (particles != null)
        {
            particlesInstance = Instantiate(particles, transform.position, transform.rotation);
            particlesInstance.transform.parent = transform;
        }
    }

    private void Update()
    {
        if (gunGrabPoint != null) // When we grab the gun
        {
            if(Input.GetKeyDown(KeyCode.Mouse0) && Time.time > shotRateTime) // Press left mouse and shoot interval
            {
                GameObject newBullet;
                newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);
                shotRateTime = Time.time + shotRate;
                newBullet.AddComponent<Bullet>().bullet = newBullet; // Destroys bullet on collision
            }
        }
    }

    public void Grab(GameObject grabPoint)
    {
        // Asign gun to Player
        transform.parent = grabPoint.transform;
        Vector3 parentCenter = grabPoint.transform.position;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.Rotate(-3.234f, -5.2f, 3);
        objectRigidBody.useGravity = false;
        objectCollider.enabled = false;
        gunGrabPoint = grabPoint;

        // Delete particles
        if(particlesInstance != null)
        {
            Destroy(particlesInstance); 
        }
    }
}
