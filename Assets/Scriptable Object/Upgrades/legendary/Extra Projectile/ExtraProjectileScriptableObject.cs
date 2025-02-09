using UnityEngine;

[CreateAssetMenu(fileName = "ExtraProjectile", menuName = "Upgrades/Effects/ExtraProjectile")]
public class ExtraProjectileScriptableObject : UpgradeEffect
{
    [SerializeField] private float minusAngleSpreadAmount;
    [SerializeField] private float angleSpreadHardCap;
    
    [SerializeField] private int extraProjectilesAmount;
    
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            if (turretStats.angleSpread <= angleSpreadHardCap)
            {
                turretStats.angleSpread = angleSpreadHardCap;
            }
            else
            {
                turretStats.angleSpread -= minusAngleSpreadAmount;
            }
            
            turretStats.extraProjectiles += extraProjectilesAmount;
            
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}
