using System.Collections;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private int currentBounces = 0;
    
    [Header("Bounce Settings")]
    public int maxBounces = 3;
    public LayerMask colliderLayer;

    [Header("Chain Bounce Upgrade")]
    public bool canChain = false; // Enable chain bounce
    public float chainRange = 5f; // Range to find the nearest enemy

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    public void SetSpeed(float bulletSpeed)
    {
        speed = bulletSpeed;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentBounces >= maxBounces)
        {
            Destroy(gameObject);
            return;
        }

        Bounce(other);
    }

    private void Bounce(Collider2D other)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, colliderLayer);
        Debug.DrawRay(transform.position, direction * 1f, Color.red, 0.5f);

        if (hit.collider != null)
        {
            if (canChain)
            {
                Transform nearestEnemy = FindNearestEnemy();
                if (nearestEnemy != null)
                {
                    direction = (nearestEnemy.position - transform.position).normalized;
                }
                else
                {
                    direction = Vector2.Reflect(direction, hit.normal).normalized; // Fallback to normal bounce
                }
            }
            else
            {
                direction = Vector2.Reflect(direction, hit.normal).normalized;
            }

            currentBounces++;
        }
    }

    private Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestEnemy = null;
        float closestDistance = chainRange;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }
}
