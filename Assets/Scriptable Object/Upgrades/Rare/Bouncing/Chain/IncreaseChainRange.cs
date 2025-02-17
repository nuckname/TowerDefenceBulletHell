using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseChainRange", menuName = "Upgrades/Effects/BouncingBullet/Chain/IncreaseChainRange")]
public class IncreaseChainRange : UpgradeEffect
{
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.chainRange += 3;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}
