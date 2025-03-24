using UnityEngine;

public class BulletSplitter : MonoBehaviour
{
    [Header("Bullet Split Settings")]
    public int amountOfProjectilesToSplit = 0;
    public GameObject bulletPrefab;
    public float bulletSpeed;

    public float bulletLifetime;
    
    // Angle (in degrees) to leave as a gap in the circle.
    public float gapAngle = 30f;

    // Distance offset from the original bullet's position for spawning the new bullets.
    public float spawnOffset = 1.5f;

    // Ensure the bullet only splits once.
    private bool hasSplit = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasSplit && other.gameObject.CompareTag("Enemy"))
        {
            SplitBullet();
        }
    }

    private void SplitBullet()
    {
        // Calculate the angle step by subtracting the gap from the full circle.
        float availableAngle = 360f - gapAngle;
        float angleStep = availableAngle / amountOfProjectilesToSplit;
        
        // Center the gap by starting the bullet spawn at half the gap angle.
        float startAngle = gapAngle / 2f;

        Debug.Log("Splitting bullet into " + amountOfProjectilesToSplit + " projectiles with a " + gapAngle + "° gap.");

        for (int i = 0; i < amountOfProjectilesToSplit; i++)
        {
            float angle = startAngle + i * angleStep;
            // Convert angle to a direction vector.
            Vector2 splitDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            
            // Calculate the spawn position using the offset so bullets don't spawn on top of each other.
            Vector3 spawnPosition = transform.position + (Vector3)(splitDirection * spawnOffset);
            
            // Instantiate a new bullet at the computed spawn position.
            GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

            // Set the direction and speed of the new bullet.
            BasicBullet basicBullet = newBullet.GetComponent<BasicBullet>();
            if (basicBullet != null)
            {
                basicBullet.SetDirection(splitDirection);
                basicBullet.SetSpeed(bulletSpeed);
            }
            else
            {
                Debug.LogWarning("Spawned bullet does not have a BasicBullet component!");
            }
            
            // Remove the BulletSplitter from the spawned bullet so it does not split further.
            BulletSplitter newSplitter = newBullet.GetComponent<BulletSplitter>();
            if (newSplitter != null)
            {
                Destroy(newSplitter, bulletLifetime);
            }
            
            Destroy(newBullet, bulletLifetime);
        }
        
        // Destroy the original bullet after splitting.
        //Destroy(gameObject);
    }
}
