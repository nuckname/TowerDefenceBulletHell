using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseRareRarity", menuName = "Upgrades/Effects/Rarity/IncreaseRareRarity")]
public class IncreaseRareRarity : UpgradeEffect
{
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.RareIncreaseChanceForRarity += 1;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretShoot component!");
        }
    }
}
