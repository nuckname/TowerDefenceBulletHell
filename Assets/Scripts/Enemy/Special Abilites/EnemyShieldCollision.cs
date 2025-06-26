using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyShieldCollision : MonoBehaviour
{
    [Header("Shield Settings")]
    public int shieldHealth = 3;

    [Header("Bounce Settings")]
    [Tooltip("Speed of the bullet after bouncing off the shield")]
    public float bounceSpeed = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet") || other.gameObject.CompareTag("Bullet"))
        {
            shieldHealth--;

            if (shieldHealth <= 0)
            {
                Destroy(gameObject);
            }
            
            Destroy(other.gameObject);
        }
    }
}