using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Rigidbody objectRigidBody;
    private GameObject gunGrabPoint;
    private Collider objectCollider;

    public Transform spawnPoint;
    public GameObject bullet;
    public float shotForce = 1500f;
    public float shotRate = 0.5f;
    private float shotRateTime = 0;
    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (gunGrabPoint != null) // When we grab the gun
        {
            if(Input.GetKeyDown(KeyCode.Mouse0) && Time.time > shotRateTime)
            {
                GameObject newBullet;
                newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);
                shotRateTime = Time.time + shotRate;
                Destroy(newBullet,3);
            }
        }
    }

    public void Grab(GameObject grabPoint)
    {
        transform.parent = grabPoint.transform;
        Vector3 parentCenter = grabPoint.transform.position;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.Rotate(-3.234f, -5.2f, 3);
        objectRigidBody.useGravity = false;
        objectCollider.enabled = false;
        gunGrabPoint = grabPoint;
    }
}
