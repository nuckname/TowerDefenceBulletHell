using System;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    public TurretConfig turretConfig;
    public Transform shootPoint;
    private float fireCooldown;

    private Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Rigidbody2D rb;

    private void Awake()
    {
        GameObject bulletGameObject = Instantiate(turretConfig.bulletPrefab, shootPoint.position, Quaternion.identity);
    }

    void Update()
    {
        //rb.velocity = direction * turretConfig.bulletSpeed;
        if (turretConfig == null || shootPoint == null)
        {
            Debug.LogWarning("TurretConfig or Shoot Point is missing.");
            return;
        }

        // Handle shooting based on fire rate
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / turretConfig.fireRate;
        }
    }

    private void Shoot()
    {
        if (turretConfig.bulletPrefab == null)
        {
            Debug.LogWarning("Bullet Prefab is missing in TurretConfig.");
            return;
        }

        // Loop through each direction and shoot a bullet
        foreach (Vector2 direction in directions)
        {
            // Create the bullet and set its properties
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            rb = bullet.GetComponent<Rigidbody2D>();

            
            
            //bullet.transform.localScale *= turretConfig.bulletSize;

            // Apply force to the bullet
            
            if (rb != null)
            {
                rb.velocity = direction * turretConfig.bulletSpeed;
                print(direction);
            }

            Destroy(bullet, 2.4f);
        }
    }
}