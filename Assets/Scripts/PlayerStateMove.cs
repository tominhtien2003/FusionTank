public class PlayerStateMove : BaseState
{
    public PlayerStateMove(PlayerController playerController) : base(playerController)
    {
    }

    public override void Enter()
    {

    }

    public override void Excute()
    {
        playerController.HandleStateMove();
    }

    public override void Exit()
    {

    }

    public override string GetTypeState()
    {
        return "PlayerMove";
    }
}
