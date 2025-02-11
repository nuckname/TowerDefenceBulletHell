using UnityEngine;

[CreateAssetMenu(fileName = "GoldOnHit", menuName = "Upgrades/Effects/Gold On Hit")]
public class GoldOnHitScriptableObject : UpgradeEffect
{
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.GoldOnHit = true;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}