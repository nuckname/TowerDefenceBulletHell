using UnityEngine;

[CreateAssetMenu(fileName = "GoldOnHit", menuName = "Upgrades/Effects/Gold On Hit")]
public class GoldOnHitScriptableObject : UpgradeEffect
{
    [SerializeField] private float speedIncreaseAmount;
        
    
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.modifierBulletSpeed += speedIncreaseAmount;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}