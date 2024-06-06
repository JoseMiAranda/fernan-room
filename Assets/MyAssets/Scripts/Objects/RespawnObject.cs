using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    public Transform objectRespawnPoint;
    public Transform playerRespawnPoint;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 7) { // 7 == Player
            collision.transform.position = playerRespawnPoint.position;
        } else
        {
            collision.transform.position = objectRespawnPoint.position;
        }
    }
}
