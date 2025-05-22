using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    private RoundStateManager roundStateManager;

    public TurretConfig turretConfig;

    [SerializeField] private Transform ShootPointUp, ShootPointDown, ShootPointLeft, ShootPointRight;

    [SerializeField] private Transform TopLeft, TopRight, BottomLeft, BottomRight;

    private List<Vector2> directions = new List<Vector2>();
    private List<Transform> activeShootPoints = new List<Transform>();

    [Header("Stats")] public float fireCooldown;
    private int shootPointIndex = 0;

    [Header("Can shoot?")] public bool AllowTurretToShoot;

    private TurretStats turretStats;

    private bool homingEnabled = false;

    private BulletFactory bulletFactory;

    [SerializeField] private BulletPool bulletPool;
    
    [Header("Bullet Sprites")]
    [SerializeField] private Sprite[] bulletSprites;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] shootClips = new AudioClip[18];

    public int selectSound = 0;
    public void EnableHomingBullets(bool enable)
    {
        homingEnabled = enable;
    }

    private void Awake()
    {
        roundStateManager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<RoundStateManager>();
        bulletPool = GameObject.FindGameObjectWithTag("BulletPooling").GetComponent<BulletPool>();

        turretStats = GetComponent<TurretStats>();

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

    public void FireBackwards()
    {
        activeShootPoints.Add(ShootPointDown);
        directions.Add(Vector2.down);

    }

    public void FireSideways()
    {
        directions.Add(Vector2.up);
        directions.Add(Vector2.left);
        directions.Add(Vector2.right);
        
        activeShootPoints.Add(ShootPointUp);
        activeShootPoints.Add(ShootPointLeft);
        activeShootPoints.Add(ShootPointRight);
    }

    //Names are confusing idk

    public void DiagonalTopLeft()
    {
        directions.Add(new Vector2(1, 1).normalized); 
        directions.Add(new Vector2(-1, -1).normalized);
        
        activeShootPoints.Add(TopLeft);
        activeShootPoints.Add(BottomRight);
    }
    //Names are confusing idk
//this is currently correct though
    public void DiagonalBotLeft()
    {
        directions.Add(new Vector2(-1, 1).normalized); 
        directions.Add(new Vector2(1, -1).normalized); 
        
        activeShootPoints.Add(TopRight);
        activeShootPoints.Add(BottomLeft);
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
     
        //Bullet Sprite Random?
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
            if (activeShootPoints.Count > 0 && directions.Count > 0)
            {
                FireProjectilesInDirection(activeShootPoints[0], directions[0]);
            }
        }
        
        //audioSource.PlayOneShot(shootClips[Random.Range(0, shootClips.Length - 1)]);
        audioSource.PlayOneShot(shootClips[selectSound]);

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
        }
        else
        {
            bullet = bulletFactory.CreateBullet(shootPoint.position, Quaternion.identity, turretStats, homingEnabled);
        }
    
        if (bulletSprites != null && bulletSprites.Length > 0)
        {
            var sr = bullet.GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
            {
                int idx = Random.Range(0, bulletSprites.Length);
                sr.sprite = bulletSprites[idx];
            }
            else
            {
                Debug.LogWarning("No SpriteRenderer found on bullet to assign random sprite.");
            }
        }
        if (bulletPool != null)
        {
            bulletFactory.ConfigureBullet(bullet, turretStats, homingEnabled);
        }

        BasicBullet basicBullet = bullet.GetComponent<BasicBullet>();
        if (basicBullet != null)
            basicBullet.SetDirection(direction);
        
        

        float bulletLifeTime = turretConfig.bulletLifeTime * turretStats.modifierBulletLifeTime;
        if (bulletPool != null)
            StartCoroutine(ReturnBulletAfterTime(bullet, bulletLifeTime));
        else
            Destroy(bullet, bulletLifeTime);
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