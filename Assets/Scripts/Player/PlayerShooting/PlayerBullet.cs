using System;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 10f;
    
    private Vector2 direction;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        // Apply velocity if we have a direction set
        if (direction != Vector2.zero && rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
    }

    private void Update()
    {
        // Continuously update velocity to maintain speed
        if (rb != null && direction != Vector2.zero)
        {
            rb.linearVelocity = direction * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("IceOnDeathEffect"))
        {
            IceExplosionZone iceZone = other.gameObject.GetComponent<IceExplosionZone>();
            if (iceZone != null)
            {
                iceZone.IceOnDeathEffect(this.gameObject, 0.5f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("IceOnDeathEffect"))
        {
            IceExplosionZone iceZone = other.gameObject.GetComponent<IceExplosionZone>();
            if (iceZone != null)
            {
                iceZone.IceOnDeathEffect(this.gameObject, 2f);
            }
        }
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        
        // Apply velocity immediately if rigidbody is available
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
    }
    
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        
        // Update velocity if already moving
        if (rb != null && direction != Vector2.zero)
        {
            rb.linearVelocity = direction * speed;
        }
    }
}