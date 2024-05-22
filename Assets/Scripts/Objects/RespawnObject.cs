using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    private Transform respawnPoint;

    private void Awake()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = respawnPoint.position;
    }
}
