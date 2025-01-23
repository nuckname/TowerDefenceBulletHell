using System;
using Unity.Netcode;
using UnityEngine;

public class GhostTurretMovement : NetworkBehaviour
{
    public float moveSpeed = 5f; // Speed of the character

    private Vector3 movement = Vector3.zero; // Movement vector initialized once

    private void Awake()
    {
        BindingOfIsaacShooting.disableShooting = true;
    }

    void Update()
    {
        /*
        if (!IsOwner)
        {
            return;
        }
        */
        // Reset movement vector each frame
        movement = Vector3.zero;
        
        //Back option
        if (Input.GetKey(KeyCode.Q))
        {
            Destroy(gameObject);
        }

        // Check for input and update movement vector
        if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.UpArrow)) // Move Up
        {
            movement += Vector3.up;
        }

        if (Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.DownArrow)) // Move Down
        {
            movement += Vector3.down;
        }

        if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.LeftArrow)) // Move Left
        {
            movement += Vector3.left;
        }

        if (Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.RightArrow)) // Move Right
        {
            movement += Vector3.right;
        }

        // Normalize movement vector to prevent faster diagonal movement
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        // Calculate the new position based on movement
        Vector3 newPosition = transform.position + movement * moveSpeed * Time.deltaTime;
        transform.position = newPosition;
    }
}