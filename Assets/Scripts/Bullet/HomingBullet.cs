using UnityEngine;
using System.Collections;

public class HomingBullet : MonoBehaviour
{
    public float homingDelay = 0f;
    public float homingStrength = 5f;
    public float homingRadius = 100f;
    public float speed = 10f;  // Bullet speed
    
    private Rigidbody2D rb;
    private Transform target;
    private bool isHomingActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (rb == null)
        {
            Debug.LogError("[HomingBullet] Rigidbody2D component is missing!");
            return;
        }

        target = FindClosestTarget("Enemy");  // Find the closest enemy

        if (target != null)
        {
            Debug.Log($"[HomingBullet] Target found: {target.name} at {target.position}");
        }
        else
        {
            Debug.LogWarning("[HomingBullet] No target found within range.");
        }

        StartCoroutine(ActivateHoming());  // Start homing after delay
    }

    void Update()
    {
        DrawHomingRadius();
    }

    void FixedUpdate()
    {
        if (!isHomingActive)
        {
            Debug.Log("[HomingBullet] Homing not active yet.");
            return;
        }

        if (target == null)
        {
            Debug.LogWarning("[HomingBullet] Target lost or destroyed.");
            return;
        }

        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        Vector2 newVelocity = Vector2.Lerp(rb.linearVelocity, direction * speed, homingStrength * Time.fixedDeltaTime);
        rb.linearVelocity = newVelocity;
        
        Debug.Log($"[HomingBullet] Homing towards {target.name} | New Velocity: {newVelocity} | Target Position: {target.position}");
    }

    IEnumerator ActivateHoming()
    {
        Debug.Log($"[HomingBullet] Homing activation delayed by {homingDelay} seconds.");
        yield return new WaitForSeconds(homingDelay);
        isHomingActive = true;
        Debug.Log("[HomingBullet] Homing activated.");
    }

    private Transform FindClosestTarget(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        Transform closest = null;
        float minDistance = homingRadius;
        
        if (enemies.Length == 0)
        {
            Debug.LogWarning("[HomingBullet] No enemies found in the scene.");
        }

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            Debug.Log($"[HomingBullet] Checking enemy {enemy.name} at distance {distance}");
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }

        if (closest != null)
        {
            Debug.Log($"[HomingBullet] Closest target selected: {closest.name} at distance {minDistance}");
        }
        else
        {
            Debug.LogWarning("[HomingBullet] No valid target within range.");
        }

        return closest;
    }

    void DrawHomingRadius()
    {
        int segments = 40;
        float angleStep = 360f / segments;
        Vector3 center = transform.position;
        float radius = homingRadius;

        for (int i = 0; i < segments; i++)
        {
            float angle1 = Mathf.Deg2Rad * angleStep * i;
            float angle2 = Mathf.Deg2Rad * angleStep * (i + 1);

            Vector3 point1 = center + new Vector3(Mathf.Cos(angle1), Mathf.Sin(angle1)) * radius;
            Vector3 point2 = center + new Vector3(Mathf.Cos(angle2), Mathf.Sin(angle2)) * radius;

            Debug.DrawLine(point1, point2, Color.red);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, homingRadius);
    }
}
