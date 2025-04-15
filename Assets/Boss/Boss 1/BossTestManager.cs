using UnityEngine;
using System.Collections;

public class BossTestManager : MonoBehaviour
{
 [Header("Prefabs")]
    public GameObject projectilePrefab;         // Prefab for the projectile (must have a Rigidbody2D)
    public GameObject circlePrefab;               // Prefab for the circle enemy/shooter
    public GameObject circleProjectilePrefab;     // Prefab for projectiles fired by the circles (must have a Rigidbody2D)

    [Header("Move 1: Projectile Set Settings")]
    public int groupCount = 3;                    // Number of groups in each formation.
    public int projectilesPerGroup = 3;           // Number of projectiles in each group.
    public float smallSpacing = 1.0f;             // Spacing between projectiles within a group.
    public float largeSpacing = 3.0f;             // Additional spacing between groups.
    
    [Header("Vertical Set Settings")]
    public Vector2 bottomOrigin;                  // Starting point for the vertical set (projectiles move up).
    public float verticalProjectileSpeed = 5f;    // Speed at which vertical projectiles move upward.
    
    [Header("Horizontal Set Settings")]
    public Vector2 rightOrigin;                   // Starting point for the horizontal set (projectiles move left).
    public float horizontalProjectileSpeed = 5f;  // Speed at which horizontal projectiles travel leftward.
    
    [Header("Move 2: Circle Attack Settings")]
    //public Transform circleSpawnCentercircleSpawnCenter;           // Center point for circle spawning.
    public float circleSpawnRadius = 5f;            // Maximum radius for random spawn positions relative to center.
    public float circleProjectileSpeed = 4f;        // Speed for projectiles fired by the circles.
    public float timeBeforeShooting = 1f;           // Delay before circles begin shooting.
    public float circleCooldown = 0.5f;             // Cooldown between consecutive bursts of projectiles.
    public int shotsCount = 10;                     // Number of bursts for the circle attack.
    public int circleCount = 6;                     // Total number of circles to spawn.

    void Start()
    {
        StartCoroutine(BossRoutine());
    }

    // BossRoutine cycles indefinitely between the projectile sets and the circle attack.
    IEnumerator BossRoutine()
    {
        while (true)
        {
            // Execute Move 1: Fire both projectile sets.
            yield return StartCoroutine(Move1_ProjectileSets());
            yield return new WaitForSeconds(0.75f); // Short delay between moves.

            // Execute Move 2: Random circle placement with mutual aiming.
            yield return StartCoroutine(Move2_CircleAttack());
            yield return new WaitForSeconds(0.75f); // Delay before restarting the cycle.
        }
    }

    // Move 1: Fires both the vertical and horizontal sets.
    IEnumerator Move1_ProjectileSets()
    {
        // Spawn the vertical set (from bottom going up).
        SpawnVerticalSet(bottomOrigin);
        // Spawn the horizontal set (from right going left).
        SpawnHorizontalSet(rightOrigin);

        // Allow time for projectiles to move before switching moves.
        yield return new WaitForSeconds(3f);
    }

    // Spawns a vertical formation from a fixed x position, only incrementing y.
    void SpawnVerticalSet(Vector2 origin)
    {
        float spawnY = origin.y;
        float currentX = origin.x;

        for (int group = 0; group < groupCount; group++)
        {
            for (int i = 0; i < projectilesPerGroup; i++)
            {
                Vector2 spawnPos = new Vector2(currentX, spawnY);
                GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.up * verticalProjectileSpeed;
                }

                currentX += smallSpacing;
            }

            if (group < groupCount - 1)
            {
                currentX += largeSpacing;
            }
        }
    }


    // Spawns a horizontal formation: projectiles start at the specified origin and move leftward.
    // Their vertical positions are adjusted, but if needed you can modify this logic.
    void SpawnHorizontalSet(Vector2 origin)
    {
        Vector2 currentPosition = origin;
        for (int group = 0; group < groupCount; group++)
        {
            for (int i = 0; i < projectilesPerGroup; i++)
            {
                GameObject proj = Instantiate(projectilePrefab, currentPosition, Quaternion.identity);
                // Set projectile to move leftwards.
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.left * horizontalProjectileSpeed;
                }
                // Here the vertical position is adjusted so the formation appears vertical.
                currentPosition.y -= smallSpacing;
            }
            if (group < groupCount - 1)
            {
                currentPosition.y -= largeSpacing;
            }
        }
    }

    // Move 2: Spawns circles at random positions, then each circle fires at every other circle.
    IEnumerator Move2_CircleAttack()
    {
        GameObject[] circles = new GameObject[circleCount];

        // Randomly spawn circles within a circle around the center.
        for (int i = 0; i < circleCount; i++)
        {
            Transform circleSpawnCenter = GameObject.FindGameObjectWithTag("Player").transform;
            if (circleSpawnCenter != null)
            {
                Vector2 randomOffset = Random.insideUnitCircle * circleSpawnRadius;
                Vector2 spawnPos = (Vector2)circleSpawnCenter.position + randomOffset;
                circles[i] = Instantiate(circlePrefab, spawnPos, Quaternion.identity);
            }
        }

        // Wait before shooting begins.
        yield return new WaitForSeconds(timeBeforeShooting);

        // For a number of bursts, have each circle shoot at every other circle.
        for (int shot = 0; shot < shotsCount; shot++)
        {
            foreach (GameObject shooter in circles)
            {
                if (shooter == null)
                    continue;
                // Each shooter fires at all other circles.
                foreach (GameObject target in circles)
                {
                    if (target == null || target == shooter)
                        continue;
                    Vector2 direction = ((Vector2)target.transform.position - (Vector2)shooter.transform.position).normalized;
                    GameObject proj = Instantiate(circleProjectilePrefab, shooter.transform.position, Quaternion.identity);
                    Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.linearVelocity = direction * circleProjectileSpeed;
                    }
                }
            }
            yield return new WaitForSeconds(circleCooldown);
        }

        // Destroy all circle objects after the attack.
        foreach (GameObject circle in circles)
        {
            if (circle != null)
            {
                Destroy(circle);
            }
        }
    }
}
