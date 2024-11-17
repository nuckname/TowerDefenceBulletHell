using UnityEngine;

[CreateAssetMenu(fileName = "New Turret Config", menuName = "Turret/Turret Config")]
public class TurretConfig : ScriptableObject
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public float bulletSize = 1f;
    public float bulletSpeed = 10f;
    
    [Header("Shooting Settings")]
    public float fireRate = 1f;
    
    [Header("Rotation Settings")]
    public bool rotateTowardsTarget = true;
    public float rotationSpeed = 5f;
    
}