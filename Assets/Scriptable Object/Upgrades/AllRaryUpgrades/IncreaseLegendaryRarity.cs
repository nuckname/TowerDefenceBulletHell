using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseLegendaryRarity", menuName = "Upgrades/Effects/Rarity/IncreaseLegendaryRarity")]
public class IncreaseLegendaryRarity : UpgradeEffect
{
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.LegendaryIncreaseChanceForRarity += 1;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretShoot component!");
        }
    }
}
