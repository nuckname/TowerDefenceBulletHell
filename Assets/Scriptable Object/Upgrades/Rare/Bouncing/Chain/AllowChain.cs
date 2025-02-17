using UnityEngine;

[CreateAssetMenu(fileName = "AllowChain", menuName = "Upgrades/Effects/BouncingBullet/Chain/AllowBulletsToChainOffBounces")]
public class AllowChain : UpgradeEffect
{
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.AllowBulletsToBouncesToChain = true;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}
