using UnityEngine;
using UnityEngine.TextCore.Text;

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
        //:)
        BulletCollision _bulletCollision = bullet.GetComponent<BulletCollision>();
        _bulletCollision.pierceIndex = 0;
        
        // Configure BasicBullet
        BasicBullet basicBullet = bullet.GetComponent<BasicBullet>();

        if (turretStats.enableFigure8)
        {
            bullet.GetComponent<BulletCollision>().destroyBulletOnCollision = false;
            
            var f8 = bullet.GetComponent<Figure8Bullet>();
            if (f8 == null)
                f8 = bullet.AddComponent<Figure8Bullet>();

            // set pivot to this turret’s position
            f8.pivot         = this.transform.position;
            f8.radiusX       = turretStats.figure8RadiusX;
            f8.radiusY       = turretStats.figure8RadiusY;
            f8.loopsPerSecond = turretStats.figure8LoopsPerSec;

            // disable any other motion (homing/basic)
            // so figure‑8 is the _only_ thing moving the bullet
            var rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;
            bullet.GetComponent<BasicBullet>()?.SetDirection(Vector2.zero);
        }

        if (turretStats.slowOnHitEnabled)
        {
            bullet.GetComponent<BulletCollision>().slowOnHitEnabledBullet = true;
            
        }
        
        if (turretStats.enableBulletSplit)
        {
            BulletSplitter bulletSplitter = bullet.GetComponent<BulletSplitter>();
            if (bulletSplitter !=null)
            {
                bulletSplitter.enabled = true;
                
                bulletSplitter.bulletLifetime = turretStats.modifierBulletLifeTime;
                
                bulletSplitter.bulletPrefab = bullet;
                
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
                SpiralBullet spiral = bullet.GetComponent<SpiralBullet>();
                spiral.enabled = true;
            }
        }
        


        // Configure BulletCollision
        BulletCollision bulletCollision = bullet.GetComponent<BulletCollision>();
        if (bulletCollision != null)
        {
            // Set pierce count
            if (turretStats.pierceCount >= 1)
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

        
        var homing = bullet.GetComponent<HomingBullet>();
        if (homing != null)
        {
            // always override the flag on the pooled bullet:
            homing.useHoming = homingEnabled;
            homing.enabled   = homingEnabled;

            if (!homingEnabled)
            {
                // clear out any old target & stop coroutines
                homing.ResetHomingState();
            }
        }

        // Configure Orbit (if applicable)
        if (turretStats.enableOrbit)
        {
            //bullet.GetComponent<BulletCollision>().destroyBulletOnCollision = false;
        }
    }
}