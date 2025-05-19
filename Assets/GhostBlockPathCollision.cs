using System;
using UnityEngine;

public class GhostBlockPathCollision : MonoBehaviour
{
    [SerializeField] private GameObject crossPrefab;
    
    public bool canPlaceGhost;

    private int collisionCount = 0;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip errorClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.CompareTag("PathCollision") || other.CompareTag("TurretPlacementCollider"))
        if (other.CompareTag("PathCollision") || other.CompareTag("Turret"))
        {
 

            collisionCount++;
            UpdatePlacementState();
        }
    }

    private void Update()
    {
        if (!canPlaceGhost)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                audioSource.PlayOneShot(errorClip);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PathCollision") || other.CompareTag("Turret"))
        {
            collisionCount = Mathf.Max(0, collisionCount - 1);
            UpdatePlacementState();
        }
    }

    private void UpdatePlacementState()
    {
        if (collisionCount > 0)
        {
            crossPrefab.SetActive(true);
            canPlaceGhost = false;
        }
        else
        {
            crossPrefab.SetActive(false);
            canPlaceGhost = true;
        }
    }
}