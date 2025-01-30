using UnityEngine;

[CreateAssetMenu(fileName = "MultiShot", menuName = "Upgrades/Effects/MultiShot")]
public class MultiShotScriptableObject : UpgradeEffect
{
    [SerializeField] private int multiShotAmount;
    [SerializeField] private float minusMultiShotDelayAmount;
    
    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.multiShotCount += multiShotAmount;
            turretStats.multiShotDelay -= minusMultiShotDelayAmount;

        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}
