using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BulletCollision : MonoBehaviour
{
    
    [SerializeField] private PlayerGoldScriptableObject playerGoldScriptableObject;
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
                playerGoldScriptableObject.AddGold(5);
            }
            
            if (slowOnHitEnabledBullet)
            {
                Debug.LogWarning("Slow is not set up correctly");
                float slow = other.GetComponent<EnemyMovement>().speed;

                ApplySlow(turretStats.slowAmount, turretStats.slowDuration);
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
            if (destroyBulletOnCollision)
            {
                Destroy(gameObject);
            }
        }
    }
}
