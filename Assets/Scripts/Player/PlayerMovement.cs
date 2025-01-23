using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float slideDamping = 0.9f; // Factor to reduce velocity when sliding
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        // Movement using W, A, S, D keys
        movement.x = (Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0);
        movement.y = (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0);

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
    }

    void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            rb.linearVelocity = movement * moveSpeed;
        }
        else
        {
            // Apply sliding effect by reducing the velocity gradually
            rb.linearVelocity = rb.linearVelocity * slideDamping;

            // Stop completely if the velocity is very small
            if (rb.linearVelocity.magnitude < 0.1f)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }
}