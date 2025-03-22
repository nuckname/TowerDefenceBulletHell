using System.Collections;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private int currentBounces = 0;

    [Header("Bounce Settings")]
    public bool canBounce = false;
    public int maxBounces = 3;
    public LayerMask colliderLayer;
    
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    public void SetSpeed(float bulletSpeed)
    {
        speed = bulletSpeed;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canBounce)
        {
            if (currentBounces >= maxBounces)
            {
                Destroy(gameObject);
                return;
            }

            Bounce(other);
        }
    }

    private void Bounce(Collider2D other)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, colliderLayer);
        Debug.DrawRay(transform.position, direction * 1f, Color.red, 0.5f);
        direction = Vector2.Reflect(direction, hit.normal).normalized;

        currentBounces++;
        
    }

}
