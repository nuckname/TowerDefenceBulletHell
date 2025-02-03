using UnityEngine;

[CreateAssetMenu(fileName = "FireRate", menuName = "Upgrades/Effects/FireRate")]
public class FireRateScriptableObject : UpgradeEffect
{
    [SerializeField] private float attackIncreaseAmount;
    
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.modifierFireRate += attackIncreaseAmount;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}
