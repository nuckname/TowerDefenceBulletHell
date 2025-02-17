using System.Collections;
using UnityEngine;

public class SnakeBossSegment : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject projectilePrefab;  // Assign your projectile (box) prefab
    public float shootInterval = 2f;       // Time between volleys
    public int projectileCount = 8;        // How many projectiles per volley
    public float projectileSpeed = 5f;

    private void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);
            ShootVolley();
        }
    }

    // Shoots projectiles in a full circle
    private void ShootVolley()
    {
        float angleStep = 360f / projectileCount;
        float angle = 0f;

        for (int i = 0; i < projectileCount; i++)
        {
            // Calculate direction for this projectile
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 direction = new Vector2(dirX, dirY).normalized;

            // Instantiate and launch the projectile
            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * projectileSpeed;
            }

            angle += angleStep;
        }
    }
}