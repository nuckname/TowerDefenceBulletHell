using UnityEngine;

[CreateAssetMenu(fileName = "SlowOnHitSO", menuName = "Upgrades/Effects/SlowOnHit")]
public class SlowOnHitSO : UpgradeEffect
{
    public float slowAmountIncrease = 0.1f; 
    public float slowDurationIncrease = 0.5f; 

    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.slowOnHitEnabled = true;
            turretStats.slowAmount += slowAmountIncrease;
            turretStats.slowDuration += slowDurationIncrease;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}