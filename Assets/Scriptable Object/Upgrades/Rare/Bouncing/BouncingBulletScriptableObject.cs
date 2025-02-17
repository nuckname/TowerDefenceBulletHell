using UnityEngine;

[CreateAssetMenu(fileName = "AllowBouncingBullet", menuName = "Upgrades/Effects/BouncingBullet/AllowBouncingBullet")]
public class BouncingBulletScriptableObject : UpgradeEffect
{
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.AllowBulletsToBounce = true;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}
