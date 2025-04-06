using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    private RoundStateManager roundStateManager;
    
    public TurretConfig turretConfig;

    [SerializeField]
    private Transform ShootPointUp, ShootPointDown, ShootPointLeft, ShootPointRight;
    
    [SerializeField]
    private Transform TopLeft,TopRight, BottomLeft, BottomRight;

    private List<Vector2> directions = new List<Vector2>();
    private List<Transform> activeShootPoints = new List<Transform>();

    [Header("Stats")] public float fireCooldown;
    private int shootPointIndex = 0;

    [Header("Can shoot?")] public bool AllowTurretToShoot;

    private TurretStats turretStats;
    
    private bool homingEnabled = false; 

    private BulletFactory bulletFactory;
    
    private BulletCollision bulletCollision;
    
    [SerializeField] private BulletPool bulletPool;

    public void EnableHomingBullets(bool enable)
    {
        homingEnabled = enable;
    }
    
    private void Awake()
    {
        roundStateManager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<RoundStateManager>();
        bulletPool = GameObject.FindGameObjectWithTag("BulletPooling").GetComponent<BulletPool>();
        
        turretStats = GetComponent<TurretStats>();
        bulletCollision = GetComponent<BulletCollision>();
        
        bulletFactory = GetComponent<BulletFactory>();
        bulletFactory.bulletPrefab = turretConfig.bulletPrefab;
    }

    private void Start()
    {
        directions.Add(Vector2.up);
        activeShootPoints.Add(ShootPointUp);
        
        if (roundStateManager.currentState == roundStateManager.roundInProgressState)
        {
            AllowTurretToShoot = true;
        }
    }
    

    public void AddUpDownLeftRightShootPoints()
    {
        directions.Add(Vector2.up);
        directions.Add(Vector2.down);
        directions.Add(Vector2.left);
        directions.Add(Vector2.right);
        
        activeShootPoints.Add(ShootPointUp);
        activeShootPoints.Add(ShootPointDown);
        activeShootPoints.Add(ShootPointLeft);
        activeShootPoints.Add(ShootPointRight);
    }

    public void AddsDiagonalShootPoints()
    {
        directions.Add(new Vector2(1, 1).normalized);  // Up-Right
        directions.Add(new Vector2(-1, 1).normalized); // Up-Left
        directions.Add(new Vector2(1, -1).normalized); // Down-Right
        directions.Add(new Vector2(-1, -1).normalized);// Down-Left
        
        activeShootPoints.Add(TopLeft);
        activeShootPoints.Add(TopRight);
        activeShootPoints.Add(BottomLeft);
        activeShootPoints.Add(BottomRight);
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

        if (directions.Count == 0 || activeShootPoints.Count == 0)
        {
            Debug.LogError("No available directions or shoot points.");
            return;
        }

        // Ensure both lists are the same size
        if (directions.Count != activeShootPoints.Count)
        {
            Debug.LogError("Mismatch: directions.Count (" + directions.Count + ") != activeShootPoints.Count (" + activeShootPoints.Count + ")");
            return;
        }

        // Check if the upgrade is active
        if (turretStats.allow4ShootPoints || turretStats.allowDiagonalShooting)
        {
            // Fire in all four directions
            for (int i = 0; i < directions.Count; i++)
            {
                if (activeShootPoints[i] != null)
                {
                    FireProjectilesInDirection(activeShootPoints[i], directions[i]);
                }
                else
                {
                    Debug.LogWarning("Shoot point at index " + i + " is null.");
                }
            }
        }
        else
        {
            // Fire in only the first available direction (default behavior)
            if (activeShootPoints.Count > 0 && directions.Count > 0)
            {
                FireProjectilesInDirection(activeShootPoints[0], directions[0]);
            }
        }
    }


    private void FireProjectilesInDirection(Transform shootPoint, Vector2 baseDirection)
    {
        // Calculate the starting angle based on the turret's rotation
        float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg + transform.eulerAngles.z;

        // Calculate the total number of projectiles (1 base + extraProjectiles)
        int totalProjectiles = 1 + turretStats.extraProjectiles;

        for (int i = 0; i < totalProjectiles; i++)
        {
            // Calculate the angle offset for each projectile
            float angleOffset = turretStats.angleSpread * (i - (totalProjectiles - 1) / 2f);

            // Convert the new angle back to a direction vector
            float newAngle = baseAngle + angleOffset;
            Vector2 newDirection =
                new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));

            
            // Fire the initial projectile
            FireProjectile(shootPoint, newDirection);

            // Start multi-shot coroutines for delayed projectiles
            if (turretStats.multiShotCount > 0)
            {
                StartCoroutine(FireMultiShot(shootPoint, newDirection, turretStats.multiShotCount, turretStats.multiShotDelay));
            }
        }
    }

    private void FireProjectile(Transform shootPoint, Vector2 direction)
    {
        GameObject bullet;
        if (bulletPool != null)
        {
            bullet = bulletPool.GetBullet(shootPoint.position, Quaternion.identity);
            
            // Re-configure the pooled bullet so it moves correctly.
            bulletFactory.ConfigureBullet(bullet, turretStats, homingEnabled); // Make sure ConfigureBullet is public.
        }
        else
        {
            bullet = bulletFactory.CreateBullet(shootPoint.position, Quaternion.identity, turretStats, homingEnabled);
        }

        BasicBullet basicBullet = bullet.GetComponent<BasicBullet>();
        if (basicBullet != null)
        {
            basicBullet.SetDirection(direction);
        }

        float bulletLifeTime = turretConfig.bulletLifeTime * turretStats.modifierBulletLifeTime;
        if (bulletPool != null)
        {
            StartCoroutine(ReturnBulletAfterTime(bullet, bulletLifeTime));
        }
        else
        {
            Destroy(bullet, bulletLifeTime);
        }
    }

    private IEnumerator ReturnBulletAfterTime(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        // Only return the bullet to the pool if it hasn't been destroyed.
        if (bullet != null)
        {
            bulletPool.ReturnBullet(bullet);
        }
    }



    
    private IEnumerator FireMultiShot(Transform shootPoint, Vector2 direction, int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(delay);
            FireProjectile(shootPoint, direction);
        }
    }

    private Transform GetShootPointForDirection(Vector2 direction)
    {
        if (direction == Vector2.up) return ShootPointUp;
        if (direction == Vector2.down) return ShootPointDown;
        if (direction == Vector2.left) return ShootPointLeft;
        if (direction == Vector2.right) return ShootPointRight;

        Debug.LogWarning("No shoot point found for direction: " + direction);
        return null;
    }
}