using System;
using UnityEngine;

public class GhostTurretShoot : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Turret Settings")]
    [SerializeField] private TurretConfig turretConfig;

    private GhostTurretRotate ghostTurretRotate;

    private float fireCooldown;

    private void Start()
    {
        fireCooldown = 0.2f;
        ghostTurretRotate = GetComponent<GhostTurretRotate>();
        if (ghostTurretRotate == null)
        {
            Debug.LogError("GhostTurretRotate is null!");
        }
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / turretConfig.fireRate;
        }
    }

    void Shoot()
    {
        // Rotate bullet +90° so “up” bullet sprite aligns with turret forward
        Quaternion bulletRot = ghostTurretRotate.savedRotation * Quaternion.Euler(0, 0, 90);

        GameObject b = Instantiate(bulletPrefab, firePoint.position, bulletRot);
        GhostBullet bulletScript = b.GetComponent<GhostBullet>();
        if (bulletScript != null)
            bulletScript.SetDirection(bulletRot);
    }
}
