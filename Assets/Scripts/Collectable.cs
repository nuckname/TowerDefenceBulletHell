using System;
using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Vector3 targetpostion;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    private bool hasTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector2 targetDirection = (targetpostion - transform.position).normalized;
            rb.linearVelocity = new Vector2(targetDirection.x, targetDirection.y) * speed;
        }

    }

    public void SetTarget(Vector3 position)
    {
        targetpostion = position;
        hasTarget = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerCollision"))
        {
            Destroy(gameObject);
        }
    }
}
