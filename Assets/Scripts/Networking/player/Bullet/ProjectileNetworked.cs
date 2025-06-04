using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(NetworkObject))]
public class ProjectileNetworked : NetworkBehaviour
{
    // A small NetworkVariable to hold the projectile’s velocity vector.
    private NetworkVariable<Vector2> velocityVar = new NetworkVariable<Vector2>(
        writePerm: NetworkVariableWritePermission.Server,
        readPerm:  NetworkVariableReadPermission.Everyone
    );

    private Vector2 localVelocity;

    private void Start()
    {
        // Subscribe to changes so that clients pick up the velocity as soon as the server sets it.
        velocityVar.OnValueChanged += OnVelocityChanged;

        // If this object already has a nonzero velocityVar (e.g. host just spawned it),
        // cache it now:
        localVelocity = velocityVar.Value;
    }

    private void OnDestroy()
    {
        velocityVar.OnValueChanged -= OnVelocityChanged;
    }

    private void OnVelocityChanged(Vector2 previous, Vector2 current)
    {
        localVelocity = current;
    }

    private void Update()
    {
        // Everyone (host and remote clients) simply moves the projectile each frame
        if (localVelocity != Vector2.zero)
        {
            // Basic translation; you can add gravity or rotation if you want
            transform.Translate(localVelocity * Time.deltaTime);
        }
    }

    /// <summary>
    /// Called on the server (host) right after spawning the projectile.
    /// This writes to the NetworkVariable, so all clients see the same velocity.
    /// </summary>
    public void InitializeOnServer(Vector2 velocity)
    {
        if (!IsServer) return;
        velocityVar.Value = velocity;
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Example: destroy on impact 
        // (make sure your colliders + Rigidbody layers/tags are set up)
        if (!IsServer) return; 
        // Only the server decides collision/despawn. Clients are just visual.

        //check out PlayerShooringCollision.cs

        if (other.CompareTag("Enemy"))
        {
            // Here you could do damage RPCs to the enemy, etc.
            // Then despawn this projectile:
            var netObj = GetComponent<NetworkObject>();
            if (netObj != null && netObj.IsSpawned)
                netObj.Despawn(true);
            else 
                Destroy(gameObject);
        }
    }
}
