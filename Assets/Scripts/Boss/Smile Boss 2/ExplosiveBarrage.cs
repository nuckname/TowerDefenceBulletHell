using UnityEngine;
using System.Collections;

public class ExplosiveBarrage : MonoBehaviour
{
    public bool targetPlayer = true;
    
    [Header("Barrage Settings")]
    public int bulletCount = 30;               // Total number of bullets to spawn
    public GameObject bulletPrefab;            // Prefab for the projectile bullet
    public float spawnRadius = 5f;             // Distance from the red circle where bullets spawn
    public float bulletSpeed = 10f;            // Speed at which bullets travel toward the red circle

    [Header("Explosion Visuals")]
    public float flashDuration = 0.5f;         // Duration of the flash before the barrage
    public Color flashColor = Color.red;       // Color for the flashing effect
    public SpriteRenderer spriteRenderer;      // SpriteRenderer to apply flash effects
    public GameObject explosionEffectPrefab;

    [Header("Delay")] 
    public float bulletSpawnDelay = 0.25f;
    public float bulletFiringDelay = 1;// Optional explosion particle effect

    void Start()
    {
        if (targetPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                transform.position = player.transform.position;
            }
        }
        // Ensure we have a SpriteRenderer reference.
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        StartCoroutine(FlashAndExplode(bulletSpawnDelay));
    }

    IEnumerator FlashAndExplode(float delay)
    {
        // Save the original color.
        Color originalColor = spriteRenderer.color;
        float elapsed = 0f;

        // Flash effect: interpolate between the original and flash color.
        while (elapsed < flashDuration)
        {
            spriteRenderer.color = Color.Lerp(originalColor, flashColor, Mathf.PingPong(elapsed * 5f, 1f));
            elapsed += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = originalColor;

        // Optionally, spawn a visual explosion effect.
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(delay); 
        // Spawn the barrage of bullets.
        for (int i = 0; i < bulletCount; i++)
        {
            StartCoroutine(SpawnBullet(bulletFiringDelay));
        }

        // Destroy the explosive object after the barrage.
        Destroy(gameObject);
    }

    IEnumerator SpawnBullet(float bulletFiringDelay)
    {
        // Choose a random angle (in degrees) around the circle.
        float angle = Random.Range(0f, 360f);
        // Determine the spawn position on the circle's circumference.
        Vector3 spawnOffset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f) * spawnRadius;
        Vector3 spawnPosition = transform.position + spawnOffset;

        // Instantiate the bullet at the computed position.
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // Calculate direction vector from the spawn point to the center (the red circle).
        Vector2 direction = ((Vector2)transform.position - (Vector2)spawnPosition).normalized;

        // Set the bullet's velocity if it has a Rigidbody2D component.
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }

        yield return new WaitForSeconds(bulletFiringDelay); 
    }

    // Optional: visualize the spawn circle in the Scene view.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
