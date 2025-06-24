using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyCollision : MonoBehaviour
{
    private EnemyHealth _enemyHealth;
    private EnemyFollowPath _enemyFollowPath;
    
    [SerializeField] private EnemyOnMapCounter enemyOnMapCounter;

    public float PaintMoveSpeedModifer;
    
    private bool enemySlowed = false;
        
    // Start is called before the first frame update
    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyFollowPath = GetComponent<EnemyFollowPath>();

        enemyOnMapCounter = GameObject.FindGameObjectWithTag("StateManager").GetComponent<EnemyOnMapCounter>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _enemyHealth.EnemyHit();  
            //Destroy(other.gameObject);
            
        }
        
        //if (other.gameObject.CompareTag("IceOnDeathEffect") && !enemySlowed)
        if (other.gameObject.CompareTag("IceOnDeathEffect"))
        {
            print("Enter Collision");
            
            IceExplosionZone iceZone = other.gameObject.GetComponent<IceExplosionZone>();
            if (iceZone != null)
            {
                enemySlowed = true;
                iceZone.IceOnDeathEffect(this.gameObject, 0.5f);
            }
        }

        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            _enemyHealth.EnemyHit();  
            Destroy(other.gameObject);
            
        }
        
        if (other.gameObject.CompareTag("RedBox"))
        {
            if (enemyOnMapCounter != null)
            {
                enemyOnMapCounter.DecreaseEnemyCount();
                
                PlayerHealth.Instance.TakeDamage(1);
            }
            else
            {
                Debug.LogWarning("EnemyOnMapCounter component not found on RedBox.");
            }
            
            Destroy(gameObject);
            print("-1 hp");
        }

        if (other.gameObject.CompareTag("PaintEffect"))
        {
            ChangeMovementSpeed(PaintMoveSpeedModifer);
        }
    }

    //If Ice effect spawns on top of enemy. 
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("IceOnDeathEffect"))
        {
            if (!enemySlowed)
            {
                print("Enter Stay");

                IceExplosionZone iceZone = other.gameObject.GetComponent<IceExplosionZone>();
                if (iceZone != null)
                {
                    iceZone.IceOnDeathEffect(this.gameObject, 0.5f);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PaintEffect"))
        {
            ChangeMovementSpeed(-PaintMoveSpeedModifer);

        }
        
        if (other.gameObject.CompareTag("IceOnDeathEffect"))
        {
            print("Exit Collision");

            IceExplosionZone iceZone = other.gameObject.GetComponent<IceExplosionZone>();
            if (iceZone != null)
            {
                iceZone.IceOnDeathEffect(this.gameObject, 2f);
            }
        }
    }



    private void ChangeMovementSpeed(float modifier)
    {
        _enemyFollowPath.moveSpeed += modifier;
    }
}
