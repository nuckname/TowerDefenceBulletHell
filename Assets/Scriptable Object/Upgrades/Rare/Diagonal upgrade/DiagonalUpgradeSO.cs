using UnityEngine;

[CreateAssetMenu(fileName = "DiagonalUpgradeSO", menuName = "Upgrades/Effects/DiagonalUpgradeSO")]
public class DiagonalUpgradeSO : UpgradeEffect
{
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.allowDiagonalShooting = true;
            turretStats.activeDirections += 4;
            
            TurretShoot turretShoot = targetTurret.GetComponent<TurretShoot>();
            turretShoot.AddsDiagonalShootPoints();

        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}
