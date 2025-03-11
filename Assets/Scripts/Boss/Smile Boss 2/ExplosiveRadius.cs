using UnityEngine;
using System.Collections;

public class ExplosiveRadius : MonoBehaviour
{
    [Header("Targeting Options")]
    // If true, the explosive will move to the player's position on spawn.
    public bool targetPlayer = true;

    [Header("Explosion Settings")]
    public float flashDuration = 0.5f;       // How long the flash lasts before exploding.
    public float explosionDelay = 0.5f;      // Delay after flashing before the explosion effect.
    public float explosionRadius = 5f;       // Area of effect radius.
    public float explosionDamage = 100f;     // Damage dealt if the player is within the radius.

    [Header("Visual Settings")]
    public Color flashColor = Color.red;     // Color used for the flash effect.
    public GameObject explosionEffectPrefab; // Optional particle effect prefab for explosion.
    
    // Reference to the SpriteRenderer (should be on the same GameObject).
    public SpriteRenderer spriteRenderer;

    private Color originalColor;

    void Start()
    {
        // Optionally move the explosive to the player's current position.
        if (targetPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                transform.position = player.transform.position;
            }
        }

        // Get the sprite renderer if not already assigned.
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        originalColor = spriteRenderer.color;

        // Start the flashing and explosion routine.
        StartCoroutine(FlashAndExplode());
    }

    IEnumerator FlashAndExplode()
    {
        // Flash effect: smoothly change color back and forth.
        float elapsed = 0f;
        while (elapsed < flashDuration)
        {
            spriteRenderer.color = Color.Lerp(originalColor, flashColor, Mathf.PingPong(elapsed * 5f, 1f));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Wait a little after flashing to build up tension.
        yield return new WaitForSeconds(explosionDelay);

        // Optionally spawn an explosion effect (e.g., particles).
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        // Detect colliders within the explosion radius.
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hitColliders)
        {
            // Check if the collider is the player.
            if (hit.CompareTag("Player"))
            {
                print("Player take damage");
                // Assumes the player has a component with a TakeDamage(float) method.
                //hit.GetComponent<PlayerHealth>()?.TakeDamage(explosionDamage);
            }
        }

        // Destroy the explosive prefab after the explosion.
        Destroy(gameObject);
    }

    // Optional: Visualize the explosion radius in the editor.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
