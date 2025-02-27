using UnityEngine;

public class BoomerangBullet : MonoBehaviour
{
    private Vector3 startPoint;
    private Rigidbody2D rb;
    private bool returning = false;
    public float speed = 5f;

    public void Initialize(Vector3 spawnPoint)
    {
        startPoint = spawnPoint;
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
        Invoke("ReturnToSender", 1f);
    }

    void ReturnToSender()
    {
        returning = true;
    }

    void Update()
    {
        if (returning)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPoint) < 0.1f)
                Destroy(gameObject);
        }
    }
}