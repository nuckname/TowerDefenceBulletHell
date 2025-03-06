using UnityEngine;

[CreateAssetMenu(fileName = "4DifferentDirections", menuName = "Upgrades/Effects/4ShootDifferentDirections")]
public class FourDifferentDirectionsScriptableObject : UpgradeEffect
{
    //Only allowed once.?
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.activeDirections += 4;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}
