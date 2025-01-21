using UnityEngine;

public class BindingOfIsaacShooting : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public float shootCooldown = 0.5f; 
    public Transform firePoint; 

    private float lastShotTime;
    public static bool disableShooting = false;

    private void Update()
    {
        if (disableShooting) return;

        if (Time.time >= lastShotTime + shootCooldown)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Shoot(Vector2.up);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Shoot(Vector2.down);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Shoot(Vector2.left);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Shoot(Vector2.right);
            }
        }
    }

    private void Shoot(Vector2 direction)
    {
        lastShotTime = Time.time;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * projectile.GetComponent<Projectile>().speed;
    }
}