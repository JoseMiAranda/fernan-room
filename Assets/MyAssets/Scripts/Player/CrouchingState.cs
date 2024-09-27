using UnityEngine;

public class CrouchingState : PlayerState
{
    public CrouchingState(PlayerMovement playerMovement) : base(playerMovement)
    {
        Vector3 localScale = playerMovement.characterController.transform.localScale;
        playerMovement.characterController.transform.localScale = new Vector3(localScale.x, localScale.y * playerMovement.bendDownPercentage, localScale.z);
    }

    public override void HandleInput()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Vector3 localScale = playerMovement.characterController.transform.localScale;
            playerMovement.characterController.transform.localScale = new Vector3(localScale.x, localScale.y / playerMovement.bendDownPercentage, localScale.z);
            playerMovement.TransitionToState(new GroundedState(playerMovement));
        }
    }

    public override void UpdateState() { }
}
