using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    
    // AXIS X and Z
    public float speed = 10f;
    
    // AXIS Y
    private float gravity = -9.81f;
    private float bendDownPercentage = 0.5f;

    public Transform groundCheck;
    public float sphereRadius = 0.3f;
    public LayerMask groundMask;
    bool isGrounded;

    public float jumpHeight = 3f;

    // AXIS X, Y and Z
    Vector3 velocity;

    void Update()
    {
        if(GameManager.Instance.CanMove())
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2; // Reset velocity Y when Player is jumping/falling down
            }

            float x = Input.GetAxis("Horizontal"); // Get A,D and left and rigth from arrow keys
            float z = Input.GetAxis("Vertical"); // Get W,S and up arrow from arrow keys

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); // Make Player jump
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Vector3 localScale = characterController.transform.localScale;
                characterController.transform.localScale = new Vector3(localScale.x, localScale.y * bendDownPercentage, localScale.z); // Make player bend down
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                Vector3 localScale = characterController.transform.localScale;
                characterController.transform.localScale = new Vector3(localScale.x, localScale.y / bendDownPercentage, localScale.z); // Make player stand up
            }

            Vector3 move = transform.right * x + transform.forward * z; // Make Player move horizontally

            characterController.Move(move * speed * Time.deltaTime); // Move Axis X and Z

            velocity.y += gravity * Time.deltaTime; // Make Player fall down

            characterController.Move(velocity * Time.deltaTime); // Gravity Axis Y
        }
    }
}
