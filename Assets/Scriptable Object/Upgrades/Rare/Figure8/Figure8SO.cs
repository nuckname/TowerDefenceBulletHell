using UnityEngine;

[CreateAssetMenu(fileName = "Figure8SO", menuName = "Upgrades/Effects/Figure8SO")]

public class Figure8SO : UpgradeEffect
{
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.enableFigure8 = true;
            targetTurret.GetComponent<BulletCollision>();
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}
