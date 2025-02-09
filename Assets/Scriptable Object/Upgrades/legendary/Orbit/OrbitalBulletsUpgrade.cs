using UnityEngine;

[CreateAssetMenu(fileName = "OrbitalBulletsUpgrade", menuName = "Upgrades/Effects/OrbitalBullets")]
public class OrbitalBulletsUpgrade : UpgradeEffect
{
    public GameObject orbitalBulletPrefab;
    public int numberOfBullets = 4;  // How many bullets orbit
    public float orbitRadius = 1.5f;
    public float orbitSpeed = 180f;

    public override void Apply(GameObject targetTurret)
    {
        if (targetTurret.TryGetComponent<TurretStats>(out TurretStats turretStats))
        {
            //To upgrade later. Split later?
            turretStats.orbitRadius += 1;
            turretStats.orbitSpeed += 100;

            turretStats.enableOrbit = true;
        }
        else
        {
            Debug.LogWarning("Target turret does not have a TurretShoot component!");
        }
    }
}