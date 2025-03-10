using UnityEngine;

public class OrbitTrigger : MonoBehaviour
{
    public float orbitSpeed = 90f;
    public float orbitRadiusMultiplier = 1.5f;
    public float bulletSpacing = 10f;

    [SerializeField] private TurretStats turretStats;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (turretStats.enableOrbit)
            {
                OrbitalBullet orbitingBullet = other.GetComponent<OrbitalBullet>();
                if (orbitingBullet != null)
                {
                    orbitingBullet.StartOrbiting(GetComponent<CircleCollider2D>(), orbitSpeed, orbitRadiusMultiplier, bulletSpacing);
                }
            }

        }
        
    }
}