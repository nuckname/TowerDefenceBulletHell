using System.Collections;
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
    public float bulletSpacing = 0.25f;    // Angle offset to spread the bullets evenly
    
    public float positionOffset = 0.25f; 
    
    private float orbitStartDelay = 0.25f;

    private IEnumerator StartOrbitingWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the delay
    }

    private float i = 1;

    void Update()
    {
        if (isOrbiting && orbitCollider != null)
        {
            // Rotate the bullet around the orbit center
            angle += orbitSpeed * Time.deltaTime;

            // Ensure the angle stays between 0 and 360
            if (angle >= 360f)
                angle -= 360f;

            // Calculate the new position based on the angle and the orbit radius
            Vector2 offset = new Vector2(
                Mathf.Cos(Mathf.Deg2Rad * angle),
                Mathf.Sin(Mathf.Deg2Rad * angle)
            ) * orbitRadius;

            // Apply the incremental position offset each update
            transform.position = orbitCenter + offset + (Vector2.one * positionOffset);
        }
        else
        {
            positionOffset = 0.25f;
        }
    }

    // Start orbiting the bullet around the circle collider
    public void StartOrbiting(CircleCollider2D newOrbitCollider, float speed, float distanceMultiplier = 1f, float bulletOffset = 0f)
    {
        orbitCollider = newOrbitCollider;
        orbitCenter = orbitCollider.bounds.center;  
        orbitRadius = orbitCollider.radius * distanceMultiplier;  
        orbitSpeed = speed;
    
        // Apply the bullet spacing offset to the angle
        angle = bulletOffset;

        isOrbiting = true;
    }


    // Optionally, you can stop orbiting
    public void StopOrbiting()
    {
        isOrbiting = false;
    }

}