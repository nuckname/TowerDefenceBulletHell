using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 2f;
    public float bulletSpeed = 5f;
    private float nextShootTime;

    void Update()
    {
        if (Time.time >= nextShootTime)
        {
            ShootRandomDirection();
            nextShootTime = Time.time + shootCooldown;
        }
    }

    void ShootRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        Quaternion rotation = Quaternion.Euler(0, 0, randomAngle);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = bullet.transform.up * bulletSpeed;
    }
}