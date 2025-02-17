using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BouncingBullet : MonoBehaviour
{
   public float initialSpeed = 10f;  // Starting speed
    public float speedLossPerBounce = 0.2f;  // Default 20% speed loss per bounce
    public float minSpeedThreshold = 1f;  // Speed at which the bullet stops bouncing
    public int maxBounces = 10;  // Safety limit to prevent infinite bounces

    private Rigidbody2D rb;
    private int bounceCount = 0;
    private bool hasInfiniteRicochet = false;
    private bool hasEnergyRebound = false;
    private float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = initialSpeed;
        rb.linearVelocity = transform.right * speed;  // Start moving forward
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            HandleEnemyHit();
        }
        else
        {
            HandleBounce(collision);
        }
    }

    void HandleBounce(Collision2D collision)
    {
        if (hasInfiniteRicochet)
        {
            // Infinite ricochet mode: No speed loss
            return;
        }

        bounceCount++;
        if (bounceCount > maxBounces) 
        {
            Destroy(gameObject);
            return;
        }

        // Calculate bounce reflection
        Vector2 inDirection = rb.linearVelocity.normalized;
        Vector2 normal = collision.contacts[0].normal;
        Vector2 reflectedDirection = Vector2.Reflect(inDirection, normal);

        // Reduce speed per bounce (upgradable)
        speed *= (1f - speedLossPerBounce);
        if (speed < minSpeedThreshold)
        {
            Destroy(gameObject);
            return;
        }

        rb.linearVelocity = reflectedDirection * speed;
    }

    void HandleEnemyHit()
    {
        if (hasEnergyRebound)
        {
            speed *= 1.1f;  // Gain 10% speed on enemy hit
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
        }
    }

    // Upgrade Methods
    public void UpgradeMomentumBoost(float newSpeedLoss)
    {
        speedLossPerBounce = newSpeedLoss;
    }

    public void UpgradeEnergyRebound()
    {
        hasEnergyRebound = true;
    }

    public void UpgradeInfiniteRicochet()
    {
        hasInfiniteRicochet = true;
    }

}