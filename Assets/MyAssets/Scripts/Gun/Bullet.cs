using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    void OnCollisionEnter(Collision collision)
    {
        Destroy(bullet);
    }
}
