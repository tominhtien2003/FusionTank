using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] Rocket rocketPrefab;
    [SerializeField] Transform bulletHolder;

    [Networked] TickTimer attackCooldown { get; set; }

    private Vector3 directionPlayer;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void Spawned()
    {

    }
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            if (data.direction.sqrMagnitude > 0f)
            {
                directionPlayer = data.direction;
            }
            if (data.direction != Vector3.zero)
            {
                HandleStateMove();
            }
            else
            {
                HandleStateIdle();
            }
            if (data.buttons.IsSet(NetworkInputData.MOUSELEFT))
            {
                if (attackCooldown.ExpiredOrNotRunning(Runner))
                {
                    attackCooldown = TickTimer.CreateFromSeconds(Runner, .5f);
                    HandleStateAttack();
                }
            }
        }
    }
    
    #region Idle
    public void HandleStateIdle()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0f);
    }
    #endregion
    #region Move
    public void HandleStateMove()
    {
        rb.velocity = directionPlayer * moveSpeed;

        Quaternion targetRotation = Quaternion.LookRotation(directionPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Runner.DeltaTime * 15f);
    }
    #endregion
    #region Attack
    public void HandleStateAttack()
    {
        if (HasStateAuthority)
        {
            Runner.Spawn(rocketPrefab, bulletHolder.position, Quaternion.LookRotation(directionPlayer));
        }
    }
    #endregion
    public void Death()
    {
        Runner.Despawn(Object);
    }
}
