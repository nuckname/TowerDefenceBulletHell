using UnityEngine;

public class TurretShootOOP : MonoBehaviour
{
    public GameObject projectilePrefab;

    public Transform firePoint;

    public float fireRate = 1f;

    public float projectileSpeed = 10f;

    private float fireTimer;

    void Start()
    {
        fireTimer = 0f;
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            Shoot();
            fireTimer = 1f / fireRate;
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.forward * projectileSpeed;
            }
            else
            {
                Debug.LogWarning("No Rigidbody component found on the projectile prefab.");
            }
        }
        else
        {
            Debug.LogWarning("Projectile prefab or fire point is not assigned.");
        }
    }
}
