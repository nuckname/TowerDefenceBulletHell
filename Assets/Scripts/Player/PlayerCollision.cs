using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private HealthManager _healthManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _healthManager.PlayerMinusHealth();
            Destroy(other);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

    }
}
