using Fusion;
using UnityEngine;

public class Rocket : NetworkBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [Networked] TickTimer life { get; set; }

    public override void Spawned()
    {
        life = TickTimer.CreateFromSeconds(Runner, 3f);
    }
    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner))
        {
            Runner.Despawn(Object);
        }
        else
        {
            transform.position += moveSpeed * transform.forward * Runner.DeltaTime;
        }
    }
}
