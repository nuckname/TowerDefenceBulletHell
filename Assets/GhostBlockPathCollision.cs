using System;
using UnityEngine;

public class GhostBlockPathCollision : MonoBehaviour
{
    public bool canPlaceGhost;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private int collisionCount = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PathCollision") || other.CompareTag("TurretPlacementCollider"))
        {
            collisionCount++;
            UpdatePlacementState();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PathCollision") || other.CompareTag("TurretPlacementCollider"))
        {
            collisionCount = Mathf.Max(0, collisionCount - 1);
            UpdatePlacementState();
        }
    }

    private void UpdatePlacementState()
    {
        if (collisionCount > 0)
        {
            spriteRenderer.color = Color.red;
            canPlaceGhost = false;
        }
        else
        {
            spriteRenderer.color = Color.white;
            canPlaceGhost = true;
        }
    }
}