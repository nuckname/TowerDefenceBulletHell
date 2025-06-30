using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BulletCollision : MonoBehaviour
{
    
    //Upgrades
    public bool destroyBulletOnCollision = true;
    public TurretStats turretStats;
    
    private GameObject basicTurret;

    public bool GoldOnHit = false;
    public int pierceIndex = 0;

    public bool slowOnHitEnabledBullet;
    public float slowAmountBullet = 0f;
    public float slowDurationBullet = 0f;
    private Coroutine slowRoutine;
    
    [SerializeField] private BulletPool bulletPool;

    
    //Make null after round ends.
    private Transform _senderTeleportLocation;
    private Transform _receiverTeleportLocation;
    public void ApplySlow(float amount, float duration)
    {
        if (slowRoutine != null)
            StopCoroutine(slowRoutine);

        //slowRoutine = StartCoroutine(SlowRoutine(amount, duration));
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Not sure how this works with mutiplayer. 
            if (GoldOnHit)
            {
                PlayerGold.Instance.AddGold(5);
            }
            
             if (pierceIndex <= 0)
             {
                 bulletPool.ReturnBullet(gameObject);
                 return;
             }

             pierceIndex--;
        }
        
        /*
        if (other.gameObject.CompareTag("Bullet"))
        {
            print("Bullet on Bullet Collision");
            Destroy(other);
            
            Destroy(gameObject);
        }
        */

        if (other.gameObject.CompareTag("teleportSender"))
        {
            if (_senderTeleportLocation == null)
            {
                _senderTeleportLocation = GameObject.FindGameObjectWithTag("teleportSender").transform;
            }
            else
            {
                TeleportBullet(_senderTeleportLocation);
            }
        }

        if (other.gameObject.CompareTag("teleportReceiver"))
        {
            if (_receiverTeleportLocation == null)
            {
                _receiverTeleportLocation = GameObject.FindGameObjectWithTag("teleportReceiver").transform; 
            }
            else
            {
                TeleportBullet(_receiverTeleportLocation);
            }

        }

        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("FogEffect"))
        {
            Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("PlayerCollision"))
        {
            Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("Turret"))
        {
            if (destroyBulletOnCollision)
            {
                Destroy(gameObject);
            }
        }
    }


    private void TeleportBullet(Transform target)
    {
        // grab and store current velocity
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 incomingVelocity = rb.linearVelocity;

        // teleport the bullet
        transform.position = target.position;

        // restore its velocity
        rb.linearVelocity = incomingVelocity;
    }
}
