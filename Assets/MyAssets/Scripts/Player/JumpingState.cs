using UnityEngine;

public class JumpingState : PlayerState
{
    public JumpingState(PlayerMovement playerMovement) : base(playerMovement)
    {
        AudioManager.Instance.PlaySfx(Sfxs.jump);
        playerMovement.velocity.y = Mathf.Sqrt(playerMovement.jumpHeight * -2 * playerMovement.gravity);
    }

    public override void HandleInput()
    {
    }

    public override void UpdateState()
    {
        // Start falling
        if (playerMovement.velocity.y < 0)
        {
            playerMovement.TransitionToState(new FallingState(playerMovement));
        }
    }
}
