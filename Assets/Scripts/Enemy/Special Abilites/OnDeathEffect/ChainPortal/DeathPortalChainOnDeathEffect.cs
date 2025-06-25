// DeathPortalChainOnDeathEffect.cs

using System;
using System.Collections.Generic;
using UnityEngine;

public class DeathPortalChainOnDeathEffect : BaseOnDeathEffect
{
    [Header("Prefabs & Settings")]
    [Tooltip("Portal prefab must have a TeleportPortal component on its root")]
    public GameObject portalPrefab;
    [Tooltip("Enemy prefab to respawn (usually same as original)")]
    public GameObject enemyPrefab;
    [Range(0f,1f)]
    [Tooltip("Alpha for the respawned 'ghost'")]
    public float respawnAlpha = 0.5f;

    public int enemyRespawnHp;

    private Vector3 deathPos;
    
    private bool firstTime;

    private int DeadEnemiesWaypoint;
    
    private SpawnEnemies _spawnEnemies;

    private void Awake()
    {
        _spawnEnemies = GameObject.FindGameObjectWithTag("StateManager").GetComponent<SpawnEnemies>();
    }

    private void Start()
    {
        
         firstTime = true;
    }

    //This code runs when enemy dies.
    public override void TriggerEffect()
    {
        if (firstTime)
        {
            print("Dead enemies waypoint: " + DeadEnemiesWaypoint);
            deathPos = transform.position;

            Instantiate(portalPrefab, deathPos, Quaternion.identity);

            //Respawn another enemy
            GameObject respawnEnemy = Instantiate(enemyPrefab, deathPos, Quaternion.identity);
            
            //Set the dead enemies waypoint index to the new spawned enemy way point index.
            respawnEnemy.GetComponent<EnemyFollowPath>().waypointIndex = _spawnEnemies.enemyHasPortal.GetComponent<EnemyFollowPath>().waypointIndex; 
            
            respawnEnemy.GetComponent<EnemyHealth>().EnemyStartingHealth = enemyRespawnHp;
            respawnEnemy.GetComponent<EnemyHealth>().SetEnemyHealthToSprite();
        
            //Might not need foreach loop
            // fade out its sprite(s)
            foreach (var sr in respawnEnemy.GetComponentsInChildren<SpriteRenderer>())
            {
                var c = sr.color;
                c.a = respawnAlpha;
                sr.color = c;
            }
            
            firstTime = false;
        }
        else
        {
            deathPos = transform.position;
            Instantiate(portalPrefab, deathPos, Quaternion.identity);
        }


    }
}
