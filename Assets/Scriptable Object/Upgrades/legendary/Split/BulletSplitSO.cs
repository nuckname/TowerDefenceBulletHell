using UnityEngine;

[CreateAssetMenu(fileName = "BulletSplit", menuName = "Upgrades/Effects/Bullet Split")]
public class BulletSplitSO : UpgradeEffect
{
    // Optionally, you can add extra parameters here if you want to tweak how the split behaves.
    // For example: public int splitProjectileCountOverride = 0;

    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.enableBulletSplit = true;
            if (turretStats.allow4ShootPoints)
            {
                turretStats.splitAmount += 4;
            }

            if (turretStats.allowDiagonalShooting)
            {
                turretStats.splitAmount += 4;
            }
            
            turretStats.splitAmount += turretStats.extraProjectiles + turretStats.multiShotCount;
            
            turretStats.multiShotCount = 0;
            turretStats.extraProjectiles = 0;
            turretStats.allow4ShootPoints = false;
            turretStats.allowDiagonalShooting = false;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}