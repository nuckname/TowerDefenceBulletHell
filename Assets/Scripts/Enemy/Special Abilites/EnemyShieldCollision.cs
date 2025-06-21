using System;
using UnityEngine;

public class EnemyShieldCollision : MonoBehaviour
{
    public int shieldHealth;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("PlayerBullet"))
        {
            shieldHealth--;

            // Bounce logic
            Rigidbody2D rb = other.attachedRigidbody;
            if (rb != null)
            {
                Vector2 incomingVelocity = rb.linearVelocity;
                Vector2 normal = (rb.position - (Vector2)transform.position).normalized;
                Vector2 reflected = Vector2.Reflect(incomingVelocity, normal);

                // Add a ±45° angle deviation
                float angleOffset = UnityEngine.Random.Range(-45f, 45f);
                reflected = Quaternion.Euler(0, 0, angleOffset) * reflected;
                
                rb.linearVelocity = reflected.normalized * incomingVelocity.magnitude;
            }
                        
            // Disable shield if health is depleted
            if (shieldHealth <= 0)
            {
                gameObject.SetActive(false);
            }
            
            //Destory Bullet
            //Destroy(other.gameObject);
   
        }
    }
}
