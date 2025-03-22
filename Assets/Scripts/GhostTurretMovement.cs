using System;
using Unity.Netcode;
using UnityEngine;

public class GhostTurretMovement : NetworkBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float followSpeed = 10f; // How fast it follows the cursor
    public float rotationStep = 5f; // Degrees per scroll

    private Vector3 movement = Vector3.zero; // Movement vector

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

        // Get mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Ensure it's on the same plane as the turret

        // Move smoothly toward the mouse position
        transform.position = Vector3.Lerp(transform.position, mousePos, followSpeed * Time.deltaTime);

        // Rotate based on middle mouse scroll
        float scrollDelta = Input.mouseScrollDelta.y;

        if (Mathf.Abs(scrollDelta) > 0f)
        {
            transform.Rotate(0f, 0f, -scrollDelta * rotationStep);
        }
    }
}