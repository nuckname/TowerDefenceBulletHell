using UnityEngine;

public class OrbitTrigger : MonoBehaviour
{
    public float orbitSpeed = 90f;
    public float orbitRadiusMultiplier = 1.5f;

    [SerializeField] private TurretStats turretStats;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!turretStats.enableOrbit) return;
        if (!other.CompareTag("Bullet"))      return;

        OrbitalBullet orbitingBullet = other.GetComponent<OrbitalBullet>();
        if (orbitingBullet != null)
        {
            orbitingBullet.StartOrbiting(
                GetComponent<CircleCollider2D>(),
                orbitSpeed,
                orbitRadiusMultiplier
            );
            
        }
    }
}