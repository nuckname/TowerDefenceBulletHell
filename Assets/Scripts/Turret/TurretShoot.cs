using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    public TurretConfig turretConfig;

    [Header("Shoot Transforms")] [SerializeField]
    private Transform ShootPointUp, ShootPointDown, ShootPointLeft, ShootPointRight;

    [Header("Shoot points")] private Transform[] shootPoints;

    private List<Vector2> directions = new List<Vector2>();

    //Should be part of a scriptable object
    [Header("Stats")] private float fireCooldown;
    private int shootPointIndex = 0;

    [Header("Shoot Directions")] [SerializeField]
    private bool upShootDirection;

    [SerializeField] private bool downShootDirection;
    [SerializeField] private bool leftShootDirection;
    [SerializeField] private bool rightShootDirection;

    [Header("Can shoot?")] public bool AllowTurretToShoot;

    public int numberOfProjectiles = 1;

    private TurretStats turretStats;

    private void Awake()
    {
        shootPoints = new Transform[] { ShootPointUp, ShootPointDown, ShootPointLeft, ShootPointRight };
        turretStats = GetComponent<TurretStats>();
    }

    private void Start()
    {
        AddDirectionsToTurret();
    }

    private void AddDirectionsToTurret()
    {
        if (upShootDirection) directions.Add(Vector2.up);
        if (downShootDirection) directions.Add(Vector2.down);
        if (leftShootDirection) directions.Add(Vector2.left);
        if (rightShootDirection) directions.Add(Vector2.right);
    }

    private void Update()
    {
        if (AllowTurretToShoot)
        {
            if (turretConfig == null)
            {
                Debug.LogWarning("TurretConfig is missing.");
                return;
            }

            fireCooldown -= Time.deltaTime;
            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = 1f / (turretConfig.fireRate + turretStats.modifierFireRate);
            }
        }
    }

    private void Shoot()
    {
        if (turretConfig.bulletPrefab == null)
        {
            Debug.LogWarning("Bullet Prefab is missing in TurretConfig.");
            return;
        }

        foreach (Vector2 baseDirection in directions)
        {
            Transform currentShootPoint = shootPoints[shootPointIndex];
            if (currentShootPoint == null)
            {
                Debug.LogWarning("Shoot Point is missing.");
                continue;
            }

            // Calculate the starting angle
            float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg;

            for (int i = 0; i <= turretStats.extraProjectiles; i++)
            {
                // Calculate the angle offset for each projectile
                float angleOffset = turretStats.angleSpread * (i - turretStats.extraProjectiles / 2f);

                // Convert the new angle back to a direction vector
                float newAngle = baseAngle + angleOffset;
                Vector2 newDirection =
                    new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));

                // Fire the initial projectile
                FireProjectile(currentShootPoint, newDirection);

                // Start multi-shot coroutines for delayed projectiles
                if (turretStats.multiShotCount > 0)
                {
                    StartCoroutine(FireMultiShot(currentShootPoint, newDirection, turretStats.multiShotCount, turretStats.multiShotDelay));
                }
            }

            // Cycle through shoot points
            shootPointIndex = (shootPointIndex + 1) % shootPoints.Length;
        }
    }
    
    private void FireProjectile(Transform shootPoint, Vector2 direction)
    {
        GameObject bullet = Instantiate(
            turretConfig.bulletPrefab,
            shootPoint.position,
            Quaternion.identity
        );

        BasicBullet bulletScript = bullet.GetComponent<BasicBullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
            bulletScript.SetSpeed(turretConfig.bulletSpeed + turretStats.modifierBulletSpeed);
        }

        Destroy(bullet, turretConfig.bulletLifeTime * turretStats.modifierBulletLifeTime);
    }

    private IEnumerator FireMultiShot(Transform shootPoint, Vector2 direction, int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(delay);
            FireProjectile(shootPoint, direction);
        }
    }

}

