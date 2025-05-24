using Unity.Services.Authentication;
using UnityEngine;

public class CircleAttackController : MonoBehaviour
{
    public float rotationDuration = 3f;  // Duration to complete a full 360° rotation
    public float fireRate = 0.5f;          // Time between shots (will be randomized by the boss)
    public float bulletSpeed = 4f;         // Speed of the bullets (will be randomized by the boss)
    public GameObject bulletPrefab;        // Prefab for the projectile bullet
    
    private float elapsedTime = 0f;
    private float fireTimer = 0f;
    private float rotationSpeed;         // Computed as 360° / rotationDuration
    
    void Start()
    {
        rotationSpeed = 360f / rotationDuration;
    }
    
    void Update()
    {
        elapsedTime += Time.deltaTime;
        fireTimer -= Time.deltaTime;
        
        // Rotate the circle over time.
        float deltaAngle = rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, 0f, deltaAngle);
        
        // Fire projectiles at intervals based on fireRate.
        if (fireTimer <= 0f)
        {
            FireBullet();
            fireTimer = fireRate;
        }
        
        // After a full 360° rotation, destroy this object.
        if (elapsedTime >= rotationDuration)
        {
            Destroy(gameObject);
        }
    }
    
    void FireBullet()
    {
        if (bulletPrefab == null) return;

        Vector3 spawnPos = transform.position + new Vector3(0f, 0f, -2f);

        Quaternion spawnRot = Quaternion.Euler(0f, 0f, transform.eulerAngles.z);

        GameObject bullet = Instantiate(bulletPrefab, spawnPos, spawnRot);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = transform.right * bulletSpeed;
        }
    }

}