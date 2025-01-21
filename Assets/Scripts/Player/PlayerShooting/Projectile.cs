using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Logic for what happens when the projectile hits something
        Destroy(gameObject);
    }
}