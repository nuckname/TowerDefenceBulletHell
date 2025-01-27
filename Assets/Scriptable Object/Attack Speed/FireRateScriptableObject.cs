using UnityEngine;

[CreateAssetMenu(fileName = "FireRate", menuName = "Upgrades/Effects/FireRate")]
public class FireRateScriptableObject : UpgradeEffect
{
    [SerializeField] private float attackIncreaseAmount;
    
    public override void Apply(GameObject targetTurret)
    {
        Debug.Log("Hi from FireRateScriptableObject");
        
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            float currentUpgradeAmount = turretStats.fireRateAmount;
            
            TurretShoot turretshoot = targetTurret.GetComponent<TurretShoot>();
            turretshoot.modifierFireRate += currentUpgradeAmount;
            
            Debug.Log($"Increased attack of {targetTurret.name} by {attackIncreaseAmount}");
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}
