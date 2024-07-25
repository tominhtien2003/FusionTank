using UnityEngine;

public abstract class BaseState
{
    protected PlayerController playerController;
    public BaseState(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    public abstract void Enter();
    public abstract void Excute();
    public abstract void Exit();
    public abstract string GetTypeState();
}
