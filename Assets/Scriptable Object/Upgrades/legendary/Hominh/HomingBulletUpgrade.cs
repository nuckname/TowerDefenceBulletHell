using UnityEngine;

[CreateAssetMenu(fileName = "HomingUpgrade", menuName = "Upgrades/Effects/HomingBullets")]
public class HomingBulletUpgrade : UpgradeEffect
{
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretShoot>(out TurretShoot turretShoot))
        {
            turretShoot.EnableHomingBullets(true);
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretShoot component!");
        }
    }
}