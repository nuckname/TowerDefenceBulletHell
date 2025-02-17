using UnityEngine;

public class ForkingBullet : MonoBehaviour
{
    [SerializeField] private TurretConfig turretConfig;

    public int forkAmount = 2; 
    public float forkSpeed = 5f;
    public float forkAngle = 30f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) 
        {
            SplitBullet();
            Destroy(gameObject); // Destroy the original bullet
        }
    }

    void SplitBullet()
    {
        for (int i = 0; i < forkAmount; i++)
        {
            float angleOffset = (i - (forkAmount - 1) / 2f) * forkAngle; // Spread bullets evenly
            SpawnForkedBullet(angleOffset);
        }
    }

    void SpawnForkedBullet(float angleOffset)
    {
        GameObject forkedBullet = Instantiate(turretConfig.bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = forkedBullet.GetComponent<Rigidbody2D>();

        // Rotate the direction of the forked bullets
        Vector2 originalDirection = GetComponent<Rigidbody2D>().linearVelocity.normalized;
        Vector2 newDirection = Quaternion.Euler(0, 0, angleOffset) * originalDirection;

        rb.linearVelocity = newDirection * forkSpeed;
    }
}