using UnityEngine;

[CreateAssetMenu(fileName = "PiercingBulletUpgrade", menuName = "Upgrades/Effects/PiercingBullet")]
public class PiercingUpgradeScriptableObject : UpgradeEffect
{
    public int pierceCount = 1; // Number of enemies the bullet can pierce

    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.pierceCount += pierceCount;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }

}
