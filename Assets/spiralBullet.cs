using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiralBullet : MonoBehaviour
{
    public float spiralSpeed = 5f;  // Speed of the spiral rotation
    public float moveSpeed = 5f;   // Forward movement speed
    public float spiralRadius = 1f; // Radius of the spiral
    public float lifetime = 5f;    // How long the bullet lasts before being destroyed

    private float angle; // Current angle for the spiral movement
    private Vector3 startPosition; // Initial position of the bullet

    void Start()
    {
        startPosition = transform.position; // Store the starting position
        Destroy(gameObject, lifetime); // Automatically destroy bullet after its lifetime
    }

    void Update()
    {
        angle += spiralSpeed * Time.deltaTime;

        float offsetX = Mathf.Cos(angle) * spiralRadius;
        float offsetY = Mathf.Sin(angle) * spiralRadius;

        // Calculate forward movement
        Vector3 forwardMove = transform.up * moveSpeed * Time.deltaTime;

        // Update position with spiral offset and forward movement
        transform.position += forwardMove + new Vector3(offsetX, offsetY, 0);
    }
}
