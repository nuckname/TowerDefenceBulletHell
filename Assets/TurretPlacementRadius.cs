using System;
using UnityEngine;

public class TurretPlacementRadius : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GhostTurret") && rb != null)
        {
            GetComponent<Rigidbody2D>();
            // Stop the Rigidbody2D
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f; // Stops rotation, if needed
        }
    }
}