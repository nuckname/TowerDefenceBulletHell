using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletCollision : MonoBehaviour
{
    //Upgrades
    private PiercingBulletUpgrade piercingBulletUpgrade;
    
    public TurretStats turretStats;
    
    private GameObject basicTurret;
    
    public int pierceIndex = 0;
    private void Awake()
    {
        piercingBulletUpgrade = GetComponentInChildren<PiercingBulletUpgrade>();
    }

    private void Start()
    {
        //Sets Pierce from counter and then lowers it.
        //pierceIndex = turretStats.pierceCount;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            /*
            if (pierceIndex > 0)
            {
                pierceIndex--;
            }
            else
            {
                print("destory from pierce");
            }
            */
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(other);
            Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("PlayerCollision"))
        {
            Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("Bullet"))
        {
            //Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("Turret"))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("OutOfZone"))
        {
            Destroy(gameObject);
        }
    }
}
