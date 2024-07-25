using Fusion;
using UnityEngine;
using System.Collections;

public class Rocket : NetworkBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [Networked] TickTimer life { get; set; }

    [SerializeField] GameObject effectExplosion;
    private float effectDuration = 1.5f; 
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

    private void OnTriggerEnter(Collider other)
    {
        if (Runner.IsServer)
        {
            NetworkObject obj = Runner.Spawn(effectExplosion, transform.position, Quaternion.identity);
            StartCoroutine(DestroyEffectAfterDelay(obj, effectDuration));
        }
        if (other != null)
        {
            PlayerController playerController = other.GetComponentInParent<PlayerController>();
            if (playerController != null)
            {
                Runner.Despawn(Object);

                playerController.Death();
            }
        }
    }

    private IEnumerator DestroyEffectAfterDelay(NetworkObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        Runner.Despawn(effect);
    }
}
