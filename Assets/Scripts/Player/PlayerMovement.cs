using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private Animator animator;
    
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        //animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        // Get WASD input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        movement = movement.normalized; // Prevent faster diagonal movement

        Debug.Log("IsWalking: " + (movement != Vector2.zero));
        
        animator.SetBool("isWalk", movement != Vector2.zero);
        /*
        bool isWalking = movement != Vector2.zero;
        if (isWalking)
        {
            animator.SetBool("isWalking", isWalking);
            animator.SetBool("isIdle", false);

        }
        else
        {
            animator.SetBool("isIdle", true);
        }*/

    }

    void FixedUpdate()
    {
        // Move the player
        rb.linearVelocity = movement * moveSpeed;

        // Rotate player to face the mouse

    }

}