using UnityEngine;

public class SnakeBullet : MonoBehaviour
{
    public float speed = 5f;          // Forward speed
    public float waveFrequency = 5f;  // How fast the wave oscillates
    public float waveAmplitude = 1f;  // How wide the wave motion is
    public float lifetime = 5f;       // Time before bullet is destroyed

    private float startTime;
    private Vector3 startPosition;

    void Start()
    {
        startTime = Time.time;
        startPosition = transform.position;
        Destroy(gameObject, lifetime); // Destroy after some time
    }

    void Update()
    {
        float timeSinceStart = Time.time - startTime;

        // Move forward along the Z-axis
        float zMovement = speed * Time.deltaTime;

        // Create S-pattern motion using sine wave along X-axis
        float xMovement = Mathf.Sin(timeSinceStart * waveFrequency) * waveAmplitude;

        // Apply movement to the bullet
        transform.position += new Vector3(xMovement, 0, zMovement);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply damage to player (implement player health system)
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}