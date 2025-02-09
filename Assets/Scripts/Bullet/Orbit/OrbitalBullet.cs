using UnityEngine;

public class OrbitalBullet : MonoBehaviour
{
    public CircleCollider2D orbitCollider;  // The circle collider to guide the orbit
    public float orbitSpeed = 90f;           // Orbit speed in degrees per second
    private bool isOrbiting = false;
    private Vector2 orbitCenter;             // Center of the orbit
    private float orbitRadius;               // Radius of the orbit
    private float angle;                     // Current angle in the orbit
    public float orbitRadiusMultiplier = 1f; // Multiply the collider radius to scale the orbit size
    public float bulletSpacing = 0f;         // Angle offset to spread the bullets evenly

    private void Update()
    {
        if (isOrbiting && orbitCollider != null)
        {
            // Rotate the bullet around the orbit center
            angle += orbitSpeed * Time.deltaTime;

            // Ensure the angle stays between 0 and 360
            if (angle >= 360f)
                angle -= 360f;

            // Calculate the new position based on the angle and the orbit radius
            Vector2 offset = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)) * orbitRadius;
            transform.position = orbitCenter + offset;
        }
    }

    // Start orbiting the bullet around the circle collider
    public void StartOrbiting(CircleCollider2D newOrbitCollider, float speed, float distanceMultiplier = 1f, float bulletOffset = 0f)
    {
        orbitCollider = newOrbitCollider;
        orbitCenter = orbitCollider.bounds.center;  // Get the center of the collider
        orbitRadius = orbitCollider.radius * distanceMultiplier; // Adjust orbit radius with multiplier
        orbitSpeed = speed;
        bulletSpacing = bulletOffset; // Control bullet spacing
        isOrbiting = true;
    }

    // Optionally, you can stop orbiting
    public void StopOrbiting()
    {
        isOrbiting = false;
    }

}