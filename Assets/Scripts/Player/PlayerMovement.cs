using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour, ISpeedModifiable
{
    [SerializeField] private Animator animator;
    public float moveSpeed = 5f;

    public float modifierFireRate = 1f;
    
    private Rigidbody2D rb;
    private Vector2 movement;

    private bool wasWalking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        bool isWalking = movement.sqrMagnitude > 0f;

        if (isWalking)
        {
            animator.SetBool("isMoving", true);
        }
        else
        
        {
            animator.SetBool("isMoving", false);
        }


    }

    void FixedUpdate()
    {
        // Actually move the rigidbody
        rb.linearVelocity = movement * moveSpeed;
    }
    
    public void ModifySpeed(float multiplier)
    {
        modifierFireRate *= multiplier;
    }
}