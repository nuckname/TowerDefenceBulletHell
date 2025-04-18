using System;
using UnityEngine;

public class BindingOfIsaacShooting : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public float shootCooldown; 
    public Transform firePoint; // Where the projectile spawns
    public float projectileSpeed = 10f; 

    private float lastShotTime;
    public static bool disableShooting = false;

    private void Start()
    {
        lastShotTime = -shootCooldown;
    }

    private void Update()
    {
        if (disableShooting) return;

        // Fire only if cooldown is over
        if (Time.time >= lastShotTime + shootCooldown && Input.GetMouseButton(0)) // Left Mouse Click
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        lastShotTime = Time.time;

        // Get mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Ensure it's on the same 2D plane

        // Calculate shooting direction from firePoint
        Vector2 direction = (mousePos - firePoint.position).normalized;

        // Spawn projectile and set velocity
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileSpeed;
    }
}