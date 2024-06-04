using System;
using UnityEngine;

public class ProximityDetector : MonoBehaviour
{
    public Transform player; // Assign the player transform in the Inspector
    private Guid guid; /// Assign a unique ID to differentiate this object from others of the same type

    private void Start()
    {
        guid = Guid.NewGuid();
    }
    void Update()
    {
        if (player != null)
        {
            // Pass the player location to the shader
            GetComponent<Renderer>().sharedMaterial.SetVector("_PlayerPosition", player.position);
        }
    }

    public Guid GetGuid()
    {
        return guid;
    }

}
