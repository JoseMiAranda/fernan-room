using UnityEngine;

public class GroundedState : PlayerState
{
    public GroundedState(PlayerMovement playerMovement) : base(playerMovement) { }

    public override void HandleInput()
    {       
        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerMovement.TransitionToState(new JumpingState(playerMovement));
        }

        // Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playerMovement.TransitionToState(new CrouchingState(playerMovement));
        }
    }

    public override void UpdateState()
    {
        // Apply gravity to jumped player
        if (!playerMovement.isGrounded)
        {
            playerMovement.TransitionToState(new FallingState(playerMovement));
        }
    }
}
