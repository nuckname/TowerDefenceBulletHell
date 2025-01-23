using System;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed;

    // Called by the turret to set the bullet's direction
    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    // Called by the turret to set the bullet's speed
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
}