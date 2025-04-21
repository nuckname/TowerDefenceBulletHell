using UnityEngine;

[CreateAssetMenu(fileName = "DiagonalBotLeft", menuName = "Upgrades/Effects/DiagonalBotLeft")]
public class DiagonalBotLeft : UpgradeEffect
{
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.allowDiagonalShooting = true;
            turretStats.activeDirections += 2;
            
            TurretShoot turretShoot = targetTurret.GetComponent<TurretShoot>();
            
            //Names are confusing idk
            turretShoot.DiagonalTopLeft();

        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}
