using UnityEngine;

public class OrbitTrigger : MonoBehaviour
{
    public float orbitSpeed = 90f;  // Orbit speed in degrees per second
    public float orbitRadiusMultiplier = 1.5f; // Increase radius to make orbit larger
    public float bulletSpacing = 10f; // Adjust bullet spacing to spread them out

    [SerializeField] private TurretStats turretStats;
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // Check if the object entering the trigger is a bullet (could be based on tag or other conditions)
        if (other.CompareTag("Bullet")) // Ensure your bullets have a "Bullet" tag
        {
            if (turretStats.enableOrbit)
            {
                // Get the bullet's script and make it start orbiting
                OrbitalBullet orbitingBullet = other.GetComponent<OrbitalBullet>();
                if (orbitingBullet != null)
                {
                    orbitingBullet.StartOrbiting(GetComponent<CircleCollider2D>(), orbitSpeed, orbitRadiusMultiplier, bulletSpacing);
                }
            }

        }
        
    }
}