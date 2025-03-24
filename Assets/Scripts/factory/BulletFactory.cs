using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    public GameObject bulletPrefab;
    public TurretConfig turretConfig;

    public GameObject CreateBullet(Vector2 position, Quaternion rotation, TurretStats turretStats, bool homingEnabled)
    {
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);

        // Configure the bullet based on the turret's stats
        ConfigureBullet(bullet, turretStats, homingEnabled);

        return bullet;
    }

    public void ConfigureBullet(GameObject bullet, TurretStats turretStats, bool homingEnabled)
    {
        // Configure BasicBullet
        BasicBullet basicBullet = bullet.GetComponent<BasicBullet>();

        if (turretStats.enableBulletSplit)
        {
            BulletSplitter bulletSplitter = bullet.GetComponent<BulletSplitter>();
            if (bulletSplitter !=null)
            {
                bulletSplitter.enabled = true;
                
                bulletSplitter.bulletLifetime = turretStats.modifierBulletLifeTime;
                
                bulletSplitter.bulletPrefab = bulletPrefab;
                
                //It should give the bullets stats.
                bulletSplitter.amountOfProjectilesToSplit = turretStats.splitAmount;
                
                //2 is default bullet speed
                //+1 it feels better and matches default speed better
                bulletSplitter.bulletSpeed = turretStats.modifierBulletSpeed + turretConfig.bulletSpeed + 1;
            }
        }
        
        if (basicBullet != null)
        {

            //basicBullet.SetSpeed(turretStats.modifierBulletSpeed);
            
            //basicBullet.SetSpeed(turretConfig.bulletSpeed + turretStats.modifierBulletSpeed);
            basicBullet.SetSpeed(3 + turretStats.modifierBulletSpeed);

            // Add spiral behavior if enabled   
            if (turretStats.spiralBullets)
            {
                SpiralBullet spiral = bullet.AddComponent<SpiralBullet>();
                spiral.enabled = true;
            }
        }

        // Configure BulletCollision
        BulletCollision bulletCollision = bullet.GetComponent<BulletCollision>();
        if (bulletCollision != null)
        {
            // Set pierce count
            if (turretStats.pierceCount > 1)
            {
                bulletCollision.pierceIndex = turretStats.pierceCount;
            }

            // Enable gold on hit
            if (turretStats.GoldOnHit)
            {
                bulletCollision.GoldOnHit = true;
            }

            // Enable bouncing
            if (turretStats.amountOfBounces > 0)
            {
                //ouncingBullet bouncing = bullet.AddComponent<BouncingBullet>();
                //bouncing.bounceCount = turretStats.amountOfBounces;
            }

            // Enable projectile return
            if (turretStats.projectilesReturn)
            {
               // ReturnBullet returnBullet = bullet.AddComponent<ReturnBullet>();
                //returnBullet.enabled = true;
            }
        }

        // Configure HomingBullet
        if (homingEnabled)
        {
            HomingBullet homing = bullet.AddComponent<HomingBullet>();
            homing.enabled = true;
        }

        // Configure Orbit (if applicable)
        if (turretStats.enableOrbit)
        {
           // OrbitBullet orbit = bullet.AddComponent<OrbitBullet>();
           // orbit.orbitRadius = turretStats.orbitRadius;
           // orbit.orbitSpeed = turretStats.orbitSpeed;
        }
    }
}