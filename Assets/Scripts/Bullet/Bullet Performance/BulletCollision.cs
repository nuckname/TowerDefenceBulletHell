using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BulletCollision : MonoBehaviour
{
    
    [SerializeField] private PlayerGoldScriptableObject playerGoldScriptableObject;
    //Upgrades
    
    public TurretStats turretStats;
    
    private GameObject basicTurret;

    public bool GoldOnHit = false;
    public int pierceIndex = 0;
    
    [SerializeField] private BulletPool bulletPool;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Not sure how this works with mutiplayer. 
            if (GoldOnHit)
            {
                playerGoldScriptableObject.AddGold(1);
            }
            
            if (pierceIndex > 0)
            {
                pierceIndex--;
            }
            else
            {
                bulletPool.ReturnBullet(gameObject);

                //Destroy(gameObject);
            }
            
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
        
        if (other.gameObject.CompareTag("Turret"))
        {
            Destroy(gameObject);
        }
    }
}
