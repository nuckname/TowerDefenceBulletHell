using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosiveBarrage : MonoBehaviour
{
    public bool targetPlayer = true;
    
    [Header("Barrage Settings")]
    public int bulletCount = 30;               // Total number of bullets to spawn
    public GameObject bulletPrefab;            // Prefab for the projectile bullet
    public float spawnRadius = 5f;             // Distance from the red circle where bullets spawn
    public float bulletSpeed = 10f;            // Speed at which bullets travel toward the red circle

    [Tooltip("Minimum angular separation (in degrees) between any two bullets")]
    public float minAngleGap = 2.5f;             

    [Header("Explosion Visuals")]
    public float flashDuration = 0.5f;         
    public Color flashColor = Color.red;       
    public SpriteRenderer spriteRenderer;      
    public GameObject explosionEffectPrefab;

    [Header("Delay")] 
    public float bulletSpawnDelay = 0.25f;
    public float bulletFiringDelay = 1;
    
    int attempts, maxAttempts = 1000;


    void Start()
    {
        if (targetPlayer)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) transform.position = player.transform.position;
        }
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FlashAndExplode(bulletSpawnDelay));
    }

    IEnumerator FlashAndExplode(float delay)
    {
        // Flash effect
        Color original = spriteRenderer.color;
        float t = 0f;
        while (t < flashDuration)
        {
            spriteRenderer.color =
                Color.Lerp(original, flashColor, Mathf.PingPong(t * 5f, 1f));
            t += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = original;

        // Explosion VFX
        if (explosionEffectPrefab != null)
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(delay);

        // Generate angles with at least minAngleGap between them
        List<float> angles = GenerateAngles(bulletCount, minAngleGap);

        // Spawn all bullets (you can still stagger with additional yields if desired)
        foreach (float angle in angles)
        {
            SpawnBulletAtAngle(angle);
            // If you want a tiny spacing in time, uncomment the next line:
            // yield return new WaitForSeconds(0.02f);
        }

        Destroy(gameObject);
    }

    List<float> GenerateAngles(int count, float minGap)
    {
        var angles = new List<float>(count);

        while (angles.Count < count && attempts < maxAttempts)
        {
            attempts++;
            float candidate = Random.Range(0f, 360f);
            bool ok = true;
            foreach (var a in angles)
            {
                float d = Mathf.Abs(Mathf.DeltaAngle(candidate, a));
                if (d < minGap) { ok = false; break; }
            }
            if (ok)
            {
                angles.Add(candidate);
                attempts = 0; // reset to give full tries for next slot
            }
        }

        // If we fail to find enough with strict minGap, you can either:
        // - lower the minGap and try again, or
        // - just fill the rest randomly.
        while (angles.Count < count)
            angles.Add(Random.Range(0f, 360f));

        return angles;
    }

    void SpawnBulletAtAngle(float angle)
    {
        Vector3 offset = new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad),
            0f
        ) * spawnRadius;

        Vector3 spawnPos = transform.position + new Vector3(0f, 0f, -2f);

        Quaternion spawnRot = Quaternion.Euler(0f, 0f, 0f);

        GameObject bullet = Instantiate(bulletPrefab, spawnPos+ offset, spawnRot);
        
        Vector2 dir = ((Vector2)transform.position - (Vector2)bullet.transform.position).normalized;
        var rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = dir * bulletSpeed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
