using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyCollision : MonoBehaviour, ISpeedModifiable
{
    private EnemyHealth _enemyHealth;
    private EnemyFollowPath _enemyFollowPath;
    
    [SerializeField] private EnemyOnMapCounter enemyOnMapCounter;

    public float PaintMoveSpeedModifer;
    
    private bool enemySlowed = false;

    public bool enemyHasUsedTeleporter = false;

    private EnemyTeleport _enemyTeleport;
    
    private RoundStateManager _roundStateManager;

    [SerializeField] private bool hasBeenThroughPortal = false;

    // Start is called before the first frame update
    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyFollowPath = GetComponent<EnemyFollowPath>();

        _enemyTeleport = GetComponentInChildren<EnemyTeleport>();

        enemyOnMapCounter = GameObject.FindGameObjectWithTag("StateManager").GetComponent<EnemyOnMapCounter>();
        
        _roundStateManager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<RoundStateManager>();
    }

    public void ModifySpeed(float multiplier)
    {
        _enemyFollowPath.moveSpeed *= multiplier;
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
            enemySlowed = true;
            ModifySpeed(0.5f);
            
        }

        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            _enemyHealth.EnemyHit();  
            Destroy(other.gameObject);
            
        }

        if (other.gameObject.CompareTag("teleportReceiver"))
        {
            
        }

        if (other.gameObject.CompareTag("teleportSender") && _roundStateManager.roundHasTeleporters)
        {
            if (enemyHasUsedTeleporter)
            {
                return;
            }
            
            _enemyTeleport.TeleportEnemyOnCollision();
        }

        if (other.gameObject.CompareTag("ReversePortal"))
        {
            if (hasBeenThroughPortal)
            {
                return;
            }
            
            _enemyFollowPath.reverse = true;
                    
            _enemyFollowPath.currentWaypoint = _enemyFollowPath.waypoints.Length - 2;

            gameObject.transform.position = _enemyFollowPath.waypoints[_enemyFollowPath.waypoints.Length - 2].position;
            
            hasBeenThroughPortal = true;
        }
        

        if (other.gameObject.CompareTag("ZombieOnDeathEffect"))
        {
            GetComponent<ZombieLogic>().SetPathToPlayer();
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
                ModifySpeed(0.5f);
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
            ModifySpeed(2);
        }
    }



    private void ChangeMovementSpeed(float modifier)
    {
        _enemyFollowPath.moveSpeed += modifier;
    }
}
