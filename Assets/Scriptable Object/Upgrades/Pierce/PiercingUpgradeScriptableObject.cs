using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PiercingBulletUpgrade", menuName = "Upgrades/Effects/PiercingBullet")]
public class PiercingUpgradeScriptableObject : UpgradeEffect
{
    public int pierceUpgrade = 1;
    private GameObject bullet;
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.pierceCount += pierceUpgrade;
            /*
             //so i dont have this mistake again. im leaving this here.
             //doesnt work becuase we are still changing the bullet prefab. I would have to pass in the bullet to turret shoot. but that would work becase there are more than 1 upgrade.
            bullet = targetTurret.GetComponent<TurretShoot>().turretConfig.bulletPrefab;
            bullet.GetComponent<BulletCollision>().pierceIndex = turretStats.pierceCount;
            */
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }

}
