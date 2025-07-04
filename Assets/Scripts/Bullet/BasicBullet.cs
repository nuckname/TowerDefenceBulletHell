// BasicBullet.cs
using System.Collections;
using UnityEngine;

public class BasicBullet : MonoBehaviour, ISpeedModifiable
{
    private Vector2 direction;
    public float speed;

    private float lifetime;
    private float timeRemaining;

    private int currentBounces = 0;

    [Header("Bounce Settings")]
    public bool canBounce = false;
    public int maxBounces = 3;
    public LayerMask colliderLayer;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool fading = false;

    public void ModifySpeed(float multiplier)
    {
        speed *= multiplier;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    public void SetSpeed(float bulletSpeed)
    {
        speed = bulletSpeed;
    }

    public void SetLifetime(float time)
    {
        lifetime = time;
        timeRemaining = time;
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        // Movement
        transform.Translate(direction * speed * Time.deltaTime);

        // Lifetime countdown
        timeRemaining -= Time.deltaTime;

        // Start fade when 1s left
        if (timeRemaining <= 1f && !fading)
            fading = true;

        if (fading && spriteRenderer != null)
        {
            float alpha = Mathf.Clamp01(timeRemaining / 1f);
            var c = originalColor;
            c.a = alpha;
            spriteRenderer.color = c;
        }

        // Destroy if time’s up
        if (timeRemaining <= 0f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canBounce)
        {
            if (currentBounces >= maxBounces)
            {
                Destroy(gameObject);
                return;
            }
            Bounce(other);
        }

        if (other.CompareTag("IceOnDeathEffect"))
            ModifySpeed(0.5f);
    }

    private void Bounce(Collider2D other)
    {
        var hit = Physics2D.Raycast(transform.position, direction, 1f, colliderLayer);
        if (hit)
            direction = Vector2.Reflect(direction, hit.normal).normalized;
        currentBounces++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("IceOnDeathEffect"))
            ModifySpeed(2f);
    }

    /// <summary>
    /// Call this before re‑pooling a bullet to restore its visuals.
    /// </summary>
    public void ResetVisuals()
    {
        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;
        fading = false;
    }
}
