using System;
using UnityEngine;
using System.Collections;

public class BindingOfIsaacShooting : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint; // Where the projectile spawns
    public float projectileSpeed = 10f;

    [Header("Shooting Settings")]
    public float shootCooldown = 0.2f;
    public int magazineSize = 6;            // Number of bullets per magazine
    public float reloadTime = 2f;           // Time to reload

    private int bulletsRemaining;
    private bool isReloading = false;
    private float lastShotTime;
    public static bool disableShooting = false;

    private void Start()
    {
        bulletsRemaining = magazineSize;
        lastShotTime = -shootCooldown;
    }

    private void Update()
    {
        if (disableShooting || isReloading)
            return;

        // Trigger reload if out of bullets
        if (bulletsRemaining <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // Fire only if cooldown is over and mouse button held
        if (Time.time >= lastShotTime + shootCooldown && Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        lastShotTime = Time.time;
        bulletsRemaining--;

        // Get mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Ensure it's on the same 2D plane

        // Calculate shooting direction from firePoint
        Vector2 direction = (mousePos - firePoint.position).normalized;

        // Spawn projectile and set velocity
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileSpeed;
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        bulletsRemaining = magazineSize;
        isReloading = false;
        Debug.Log("Reload complete");
    }

    // Optional: expose current ammo count for UI
    public int GetBulletsRemaining() => bulletsRemaining;
    public bool IsReloading() => isReloading;
}
