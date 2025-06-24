using System;
using System.Collections;
using UnityEngine;

public class BasicBullet : MonoBehaviour, ISpeedModifiable
{
    private Vector2 direction;
    public float speed;
    private int currentBounces = 0;

    [Header("Bounce Settings")]
    public bool canBounce = false;
    public int maxBounces = 3;
    public LayerMask colliderLayer;
    
    public void ModifySpeed(float multiplier)
    {
        speed *= multiplier;
    }
    
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

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
        //transform.Translate(direction * speed * Time.deltaTime);
        transform.Translate(direction * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canBounce)
        {
            if (currentBounces >= maxBounces)
            {
                Destroy(gameObject);
                return;
            }

            Bounce(other);
        }
        
        if (other.gameObject.CompareTag("IceOnDeathEffect"))
        {
            IceExplosionZone iceZone = other.gameObject.GetComponent<IceExplosionZone>();
            if (iceZone != null)
            {
                iceZone.IceOnDeathEffect(this.gameObject, 0.5f);
            }
        }
    }

    private void Bounce(Collider2D other)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, colliderLayer);
        Debug.DrawRay(transform.position, direction * 1f, Color.red, 0.5f);
        direction = Vector2.Reflect(direction, hit.normal).normalized;

        currentBounces++;
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("IceOnDeathEffect"))
        {
            IceExplosionZone iceZone = other.gameObject.GetComponent<IceExplosionZone>();
            if (iceZone != null)
            {
                iceZone.IceOnDeathEffect(this.gameObject, 2f);
            }
        }
    }
}
