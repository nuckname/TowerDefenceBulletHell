using UnityEngine;

[CreateAssetMenu(fileName = "BulletLifetimeModifier", menuName = "Upgrades/Effects/Rare Normal Upgrades/Bullet Lifetime")]
public class RareBulletLifetimeModifierScriptableObject : UpgradeEffect
{
    [SerializeField] private float lifetimeMultiplier;

    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.modifierBulletLifeTime += lifetimeMultiplier;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}