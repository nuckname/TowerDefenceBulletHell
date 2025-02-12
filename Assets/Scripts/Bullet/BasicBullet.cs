using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private bool isReversed = false;

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    public void SetSpeed(float bulletSpeed)
    {
        speed = bulletSpeed;
    }

    public void ReverseBullet(Vector2 newDirection, float newSpeed)
    {
        direction = newDirection;
        speed = newSpeed;
        isReversed = true;
    }

    private void Update()
    {
        if (!isReversed)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}