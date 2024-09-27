using UnityEngine;

public class FallingState : PlayerState
{
    public FallingState(PlayerMovement playerMovement) : base(playerMovement) { }

    public override void HandleInput()
    {
    }

    public override void UpdateState()
    {
        if (playerMovement.isGrounded)
        {
            playerMovement.TransitionToState(new GroundedState(playerMovement));
        }
    }
}
