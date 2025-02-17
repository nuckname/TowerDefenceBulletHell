using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseAmountOfBounces", menuName = "Upgrades/Effects/BouncingBullet/IncreaseAmountOfBounces")]
public class IncreaseMaxBounce : UpgradeEffect
{
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.amountOfBounces += 1;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}
