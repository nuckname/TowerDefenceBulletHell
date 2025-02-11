using UnityEngine;

[CreateAssetMenu(fileName = "LessBlanksUpgrade", menuName = "Upgrades/Effects/Rarity/LessBlanksUpgrade")]
public class LessBlanksUpgrade : UpgradeEffect
{
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.ReduceTurretBlankChance += 1;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretShoot component!");
        }
    }
}
