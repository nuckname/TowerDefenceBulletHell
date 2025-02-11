using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseNormalRarity", menuName = "Upgrades/Effects/Rarity/IncreaseNormalRarity")]
public class IncreaseNormalRarity : UpgradeEffect
{
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.NormalIncreaseChanceForRarity += 1;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretShoot component!");
        }
    }
}
