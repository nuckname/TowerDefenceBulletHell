using UnityEngine;

public class ReverseBullet : MonoBehaviour
{
    [Header("Reverse Settings")]
    [SerializeField] private float reverseSpeed = 10f;
    [SerializeField] private float reverseDelay = 1f;
    [SerializeField] private bool destroyOnReverse = false;

    private Rigidbody2D rb;
    private Vector2 initialDirection;
    private bool hasReversed;

    private BasicBullet basicBullet;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        basicBullet = GetComponent<BasicBullet>();
        
        if (rb != null)
        {
            // Store initial direction
            initialDirection = rb.velocity.normalized;
            Debug.Log($"ReverseBullet: Initial direction set to {initialDirection}");
        }
        else
        {
            Debug.LogError("ReverseBullet: Rigidbody2D component is missing!");
        }
    }

    private void Start()
    {
        if (reverseDelay > 0)
        {
            Debug.Log($"ReverseBullet: Reverse scheduled in {reverseDelay} seconds");
            Invoke(nameof(ReverseDirection), reverseDelay);
        }
        else
        {
            Debug.LogWarning("ReverseBullet: Reverse delay is 0 or negative. Reversing immediately.");
            ReverseDirection();
        }
    }

    private void ReverseDirection()
    {
        if (hasReversed) return;

        rb.velocity = -initialDirection * reverseSpeed;
        hasReversed = true;

        // Disable BasicBullet's movement
        if (basicBullet != null)
        {
            basicBullet.enabled = false;
        }

        if (destroyOnReverse)
        {
            Destroy(gameObject, 1f);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("ReverseBullet: Bullet destroyed or disabled.");
        CancelInvoke();
    }
}