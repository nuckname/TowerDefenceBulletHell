using UnityEngine;

[CreateAssetMenu(fileName = "HomingUpgrade", menuName = "Upgrades/Effects/HomingBullets")]
public class HomingBulletUpgrade : UpgradeEffect
{
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretShoot>(out TurretShoot turretShoot))
        {
            turretShoot.EnableHomingBullets(true);
            
            if(targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
            {
                turretStats.isTurretHoming = true;
            }
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretShoot component!");
        }
    }
}