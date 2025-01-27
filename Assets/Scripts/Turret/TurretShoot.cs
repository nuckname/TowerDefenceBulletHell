using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    public TurretConfig turretConfig;
    
    [Header("Shoot Transforms")]

    [SerializeField] private Transform ShootPointUp, ShootPointDown, ShootPointLeft, ShootPointRight;
    
    [Header("Shoot points")]

    private Transform[] shootPoints;
    
    private List<Vector2> directions = new List<Vector2>();
    //Should be part of a scriptable object
    [Header("Stats")]

    private float fireCooldown;
    private int shootPointIndex = 0;

    [Header("Shoot Directions")]
    [SerializeField] private bool upShootDirection; 
    [SerializeField] private bool downShootDirection; 
    [SerializeField] private bool leftShootDirection; 
    [SerializeField] private bool rightShootDirection;

    [Header("Can shoot?")]

    public bool AllowTurretToShoot;
    
    public int numberOfProjectiles = 1;
    
    [Header("Upgrades")]
    public float modifierFireRate = 0;
    public float modifierBulletLifeTime = 0;
    public float modifierBulletSpeed = 0;

    private TurretStats turretStats;
    
    private void Awake()
    {
        shootPoints = new Transform[] { ShootPointUp, ShootPointDown, ShootPointLeft, ShootPointRight };
        
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
                fireCooldown = 1f / (turretConfig.fireRate + modifierFireRate);
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

        foreach (Vector2 direction in directions)
        {
            Transform currentShootPoint = shootPoints[shootPointIndex];
            if (currentShootPoint == null)
            {
                Debug.LogWarning("Shoot Point is missing.");
                continue;
            }
            //Set Bullet pierce Here.

            // Instantiate the bullet
            GameObject bullet = Instantiate(
                turretConfig.bulletPrefab,
                currentShootPoint.position,
                Quaternion.identity
            );
            
            

            // Pass the direction to the bullet script
            BasicBullet bulletScript = bullet.GetComponent<BasicBullet>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection(direction);
                bulletScript.SetSpeed(turretConfig.bulletSpeed + modifierBulletSpeed);
            }

            // Cycle through shoot points
            shootPointIndex = (shootPointIndex + 1) % shootPoints.Length;

            // Destroy the bullet after a set time
            Destroy(bullet, turretConfig.bulletLifeTime);
        }
    }

    private void SetBulletUpgrades()
    {
    }
}
