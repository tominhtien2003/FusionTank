public class PlayerStateIdle : BaseState
{
    public PlayerStateIdle(PlayerController playerController) : base(playerController)
    {
    }

    public override void Enter()
    {

    }

    public override void Excute()
    {
        playerController.HandleStateIdle();
    }

    public override void Exit()
    {

    }

    public override string GetTypeState()
    {
        return "PlayerIdle";
    }
}
