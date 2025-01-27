using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletCollision : MonoBehaviour
{
    //Upgrades
    public int pirceUpgradeValue = 1;
    private PiercingBulletUpgrade piercingBulletUpgrade;
    
    private GameObject basicTurret;
    public int pierceUpgradeValue = 1;
    private int pierceCounter;
    private void Awake()
    {
        piercingBulletUpgrade = GetComponentInChildren<PiercingBulletUpgrade>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Takes damage in Enemy Collision
            piercingBulletUpgrade.PierceUpgrade(1);
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
