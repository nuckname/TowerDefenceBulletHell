using UnityEngine;

[CreateAssetMenu(fileName = "FireBackwards", menuName = "Upgrades/Effects/TurretFireBackwards")]
public class TurretShootBackwardsSO : UpgradeEffect
{
    //Only allowed once.?
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.activeDirections += 1;
            turretStats.allow4ShootPoints = true;
            
            //To display in turretStats
            turretStats.TurretShootsBackwards = true;
            
            TurretShoot turretShoot = targetTurret.GetComponent<TurretShoot>();
            turretShoot.FireBackwards();

        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}
