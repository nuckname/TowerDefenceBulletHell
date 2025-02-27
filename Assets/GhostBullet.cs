using UnityEngine;

public class GhostBullet : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;
    [SerializeField] private TurretConfig turretConfig;
    
    public void SetDirection(Quaternion rotation)
    {
        // Convert turret's facing direction into movement direction
        direction = rotation * Vector2.right; 
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        Destroy(gameObject, turretConfig.bulletLifeTime);
    }
}