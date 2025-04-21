using UnityEngine;

[CreateAssetMenu(fileName = "FireSideWays", menuName = "Upgrades/Effects/TurretFireSideWays")]
public class TurretShootSideWays : UpgradeEffect
{
    //Only allowed once.?
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.activeDirections += 2;

            //To display in turretStats 
            turretStats.TurretShootsUpAndDown = true;
            
            TurretShoot turretShoot = targetTurret.GetComponent<TurretShoot>();
            turretShoot.FireSideways();

        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}
