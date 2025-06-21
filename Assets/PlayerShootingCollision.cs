using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShootingCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("Turret"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("FogEffect"))
        {
            //Sound?
            Destroy(gameObject);
        }
    }
}
