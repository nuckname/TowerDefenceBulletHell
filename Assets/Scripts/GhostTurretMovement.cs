using System;
using Unity.Netcode;
using UnityEngine;

public class GhostTurretMovement : NetworkBehaviour
{
    public float moveSpeed = 5f; // Speed of the character

    private Vector3 movement = Vector3.zero; // Movement vector initialized once

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

        // Check for input and update movement vector
        if (Input.GetKey(KeyCode.I)) // Move Up
        {
            movement += Vector3.up;
        }

        if (Input.GetKey(KeyCode.K)) // Move Down
        {
            movement += Vector3.down;
        }

        if (Input.GetKey(KeyCode.J)) // Move Left
        {
            movement += Vector3.left;
        }

        if (Input.GetKey(KeyCode.L)) // Move Right
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