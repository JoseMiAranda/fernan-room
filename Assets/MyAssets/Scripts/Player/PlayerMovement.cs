using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;

    public float speed = 10f;
    public float gravity = -9.81f;
    public float bendDownPercentage = 0.5f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public Transform playerRespawnPoint;
    public float sphereRadius = 0.3f;
    public LayerMask groundMask;
    public LayerMask abyssMask;

    public Vector3 velocity;
    public bool isGrounded;
    public bool isAbyss;

    private PlayerState currentState;

    void Start()
    {
        currentState = new GroundedState(this);
    }

    void Update()
    {
        if (GameManager.Instance.CanMove())
        {
            isAbyss = Physics.CheckSphere(groundCheck.position, sphereRadius, abyssMask);
            if (isAbyss)
            {
                // Respawn
                characterController.transform.position = playerRespawnPoint.transform.position;
            }
            else
            {
                isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);

                float x = Input.GetAxis("Horizontal"); // Get A,D and left and rigth from arrow keys
                float z = Input.GetAxis("Vertical"); // Get W,S and up arrow from arrow keys

                Vector3 move = transform.right * x + transform.forward * z; // Make Player move horizontally

                characterController.Move(move * speed * Time.deltaTime); // Move Axis X and Z

                // Update state
                currentState.HandleInput();
                currentState.UpdateState();

                //Apply gravity
                velocity.y += gravity * Time.deltaTime;
                characterController.Move(velocity * Time.deltaTime);
            }
        }
    }

    public void TransitionToState(PlayerState newState)
    {
        currentState = newState;
    }
}
