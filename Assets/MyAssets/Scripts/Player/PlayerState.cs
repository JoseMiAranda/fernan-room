public abstract class PlayerState
{
    protected PlayerMovement playerMovement;

    public PlayerState(PlayerMovement playerMovement)
    {
        this.playerMovement = playerMovement;
    }

    public abstract void HandleInput();
    public abstract void UpdateState();
}
