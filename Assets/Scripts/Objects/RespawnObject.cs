using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    public Transform respawnPoint;


    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = respawnPoint.position;
    }
}
