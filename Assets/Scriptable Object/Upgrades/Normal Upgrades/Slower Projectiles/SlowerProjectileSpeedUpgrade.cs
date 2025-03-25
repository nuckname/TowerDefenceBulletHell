using UnityEngine;

[CreateAssetMenu(fileName = "SlowerBulletUpgrade", menuName = "Upgrades/Effects/Slower Bullet")]
public class SlowerProjectileSpeedUpgrade : UpgradeEffect
{
    [SerializeField] private float speedDecreaseAmount;

    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            turretStats.modifierSlowerBulletSpeed += speedDecreaseAmount;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretStats component!");
        }
    }
}
