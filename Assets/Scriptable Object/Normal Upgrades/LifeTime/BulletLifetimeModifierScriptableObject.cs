using UnityEngine;

[CreateAssetMenu(fileName = "BulletLifetimeModifier", menuName = "Upgrades/Effects/Bullet Lifetime")]
public class BulletLifetimeModifierScriptableObject : UpgradeEffect
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