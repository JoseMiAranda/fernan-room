using UnityEngine;

public class ProximityDetector : MonoBehaviour
{
    public Transform player; // Assign the player transform in the Inspector

    void Update()
    {
        if (player != null)
        {
            // Pass the player location to the shader
            GetComponent<Renderer>().sharedMaterial.SetVector("_PlayerPosition", player.position);
        }
    }
}
