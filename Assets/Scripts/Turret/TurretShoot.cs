// TurretShoot.cs
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
    [Header("Can shoot?")] public bool AllowTurretToShoot;

    private TurretStats turretStats;
    private bool homingEnabled = false;
    private BulletFactory bulletFactory;

    [SerializeField] private BulletPool bulletPool;
    [Header("Bullet Sprites")] [SerializeField] private Sprite[] bulletSprites;
    [Header("Audio")] [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] shootClips = new AudioClip[18];
    public int selectSound = 0;

    private void Awake()
    {
        roundStateManager = GameObject.FindGameObjectWithTag("StateManager")
            .GetComponent<RoundStateManager>();
        bulletPool = GameObject.FindGameObjectWithTag("BulletPooling")
            .GetComponent<BulletPool>();

        turretStats = GetComponent<TurretStats>();
        bulletFactory = GetComponent<BulletFactory>();
        bulletFactory.bulletPrefab = turretConfig.bulletPrefab;
    }

    private void Start()
    {
        directions.Add(Vector2.up);
        activeShootPoints.Add(ShootPointUp);

        if (roundStateManager.currentState == roundStateManager.roundInProgressState)
            AllowTurretToShoot = true;
    }

    public void EnableHomingBullets(bool enable) => homingEnabled = enable;
    public void FireBackwards() { activeShootPoints.Add(ShootPointDown); directions.Add(Vector2.down); }
    public void FireSideways()
    {
        directions.Add(Vector2.up);
        directions.Add(Vector2.left);
        directions.Add(Vector2.right);

        activeShootPoints.Add(ShootPointUp);
        activeShootPoints.Add(ShootPointLeft);
        activeShootPoints.Add(ShootPointRight);
    }
    public void DiagonalTopLeft()
    {
        directions.Add(new Vector2(1, 1).normalized);
        directions.Add(new Vector2(-1, -1).normalized);
        activeShootPoints.Add(TopLeft);
        activeShootPoints.Add(BottomRight);
    }
    public void DiagonalBotLeft()
    {
        directions.Add(new Vector2(-1, 1).normalized);
        directions.Add(new Vector2(1, -1).normalized);
        activeShootPoints.Add(TopRight);
        activeShootPoints.Add(BottomLeft);
    }

    private void Update()
    {
        if (!AllowTurretToShoot || turretConfig == null) return;

        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / (turretConfig.fireRate + turretStats.modifierFireRate);
        }
    }

    private void Shoot()
    {
        if (turretConfig.bulletPrefab == null)
        {
            Debug.LogWarning("Bullet Prefab is missing in TurretConfig.");
            return;
        }
        if (directions.Count == 0 || activeShootPoints.Count == 0) return;
        if (directions.Count != activeShootPoints.Count)
        {
            Debug.LogError($"Directions ({directions.Count}) != ShootPoints ({activeShootPoints.Count})");
            return;
        }

        bool multiDir = turretStats.allow4ShootPoints || turretStats.allowDiagonalShooting;
        int count = directions.Count;

        for (int i = 0; i < (multiDir ? count : 1); i++)
        {
            FireProjectilesInDirection(activeShootPoints[i], directions[i]);
        }
    }

    private void FireProjectilesInDirection(Transform shootPoint, Vector2 baseDirection)
    {
        float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg
                          + transform.eulerAngles.z;
        int totalProj = 1 + turretStats.extraProjectiles;

        for (int i = 0; i < totalProj; i++)
        {
            float offset = turretStats.angleSpread * (i - (totalProj - 1) / 2f);
            float angle = baseAngle + offset;
            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad),
                                      Mathf.Sin(angle * Mathf.Deg2Rad));

            FireProjectile(shootPoint, dir);

            if (turretStats.multiShotCount > 0)
                StartCoroutine(FireMultiShot(shootPoint, dir,
                                             turretStats.multiShotCount,
                                             turretStats.multiShotDelay));
        }
    }

    private void FireProjectile(Transform shootPoint, Vector2 direction)
    {
        GameObject bullet = bulletPool != null
            ? bulletPool.GetBullet(shootPoint.position, Quaternion.identity)
            : bulletFactory.CreateBullet(shootPoint.position, Quaternion.identity,
                                         turretStats, homingEnabled);

        if (bulletSprites.Length > 0)
        {
            var sr = bullet.GetComponentInChildren<SpriteRenderer>();
            if (sr != null) sr.sprite = bulletSprites[Random.Range(0, bulletSprites.Length)];
        }

        if (bulletPool != null)
            bulletFactory.ConfigureBullet(bullet, turretStats, homingEnabled);

        var basic = bullet.GetComponent<BasicBullet>();
        if (basic != null)
        {
            basic.SetDirection(direction);
            float life = turretConfig.bulletLifeTime * turretStats.modifierBulletLifeTime;
            basic.SetLifetime(life);
        }

        // Schedule return & reset if using pool
        if (bulletPool != null)
            StartCoroutine(ReturnBulletAfterTime(bullet,
                turretConfig.bulletLifeTime * turretStats.modifierBulletLifeTime));
        else
            Destroy(bullet, turretConfig.bulletLifeTime * turretStats.modifierBulletLifeTime);
    }

    private IEnumerator ReturnBulletAfterTime(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        if (bullet != null)
        {
            var basic = bullet.GetComponent<BasicBullet>();
            if (basic != null) basic.ResetVisuals();
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
}
