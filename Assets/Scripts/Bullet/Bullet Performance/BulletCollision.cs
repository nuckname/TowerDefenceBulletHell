using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletCollision : MonoBehaviour
{
    //Upgrades
    private PiercingBulletUpgrade piercingBulletUpgrade;
    
    private GameObject basicTurret;
    
    public int pierceCounter = 3;
    private void Awake()
    {
        piercingBulletUpgrade = GetComponentInChildren<PiercingBulletUpgrade>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            print(pierceCounter);
            
            if (pierceCounter > 0)
            {
                pierceCounter--;
            }
            else
            {
                print("destory from pierce");
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("OutOfZone"))
        {
            Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            print("enemy");
            //Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            print("-1 hp?");
        }
    }
}
