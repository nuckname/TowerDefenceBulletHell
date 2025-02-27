using UnityEngine;

public class GhostTurretShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    [SerializeField] private TurretConfig turretConfig;
    private float fireCooldown;

    private Quaternion savedRotation;

    void Start()
    {
        // Start with the turret facing up
        transform.rotation = Quaternion.Euler(0, 0, 0);

        // Ensure bullets shoot upwards initially
        savedRotation = transform.rotation;
    }

    void Update()
    {
        // Rotate turret when 'R' is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.Rotate(0, 0, -90);
            savedRotation = transform.rotation;
        }

        // Save turret's current rotation on 'Space'
        if (Input.GetKeyDown(KeyCode.Space))
        {
            savedRotation = transform.rotation;
            Debug.Log("Rotation Saved: " + savedRotation.eulerAngles);
        }

        // Fire bullets based on cooldown
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / turretConfig.fireRate;
        }
    }

    void Shoot()
    {
        // Rotate bullet by +90 degrees to align with turret direction
        Quaternion bulletRotation = savedRotation * Quaternion.Euler(0, 0, 90);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
        GhostBullet bulletScript = bullet.GetComponent<GhostBullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDirection(bulletRotation);
        }
    }
}