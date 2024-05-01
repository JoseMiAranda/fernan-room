using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 10f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal"); // Get A,D and left and rigth arrows
        float z = Input.GetAxis("Vertical"); // Get W,S and up arrow
        
        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * speed * Time.deltaTime);
    }
}
