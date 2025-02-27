using System;
using UnityEngine;

public class GhostBulletCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(other);
            
            Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("PlayerCollision"))
        {
            Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("Turret"))
        {
            Destroy(gameObject);
        }
    }
}
