using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    
    [SerializeField] private Transform spawnPoint; // Where enemies will spawn
    public List<RoundsScriptableObject> roundsScriptableObject; // List of scriptable objects for each round
    public int amountOfEnemiesSpawned = 0; // Counter for enemies spawned

    private bool isDoubleHP = false;

    private GameModeManager gameModeManager;
    private void Start()
    {
        gameModeManager = GameObject.FindGameObjectWithTag("GameModeManager").GetComponent<GameModeManager>();

    }

    public int SpawnEnemiesPerRound(int currentRoundIndex)
    {
        
        if (gameModeManager.CurrentMode == GameMode.DoubleHP)
        {
            isDoubleHP = true;
        }
        else
        {
            isDoubleHP = false;
        }
        
        if (roundsScriptableObject[currentRoundIndex].boss != null)
        {
            Instantiate(roundsScriptableObject[currentRoundIndex].boss, spawnPoint.position, Quaternion.identity);
            amountOfEnemiesSpawned++;
        }
        
        // Check if there are rounds left to spawn
        if (currentRoundIndex < roundsScriptableObject.Count)
        {
            // Get the current round's scriptable object
            RoundsScriptableObject currentRound = roundsScriptableObject[currentRoundIndex];
            int totalEnemies = currentRound.GetTotalEnemies();
            
            // Start spawning enemies for the current round
            StartCoroutine(SpawnEnemiesWithDelay(currentRound));

            // Move to the next round
            currentRoundIndex++;
            print("totalEnemies: " + totalEnemies);
            return totalEnemies;
        }

        return amountOfEnemiesSpawned;
    }

    private IEnumerator SpawnEnemiesWithDelay(RoundsScriptableObject round)
    {
        // Iterate through each enemy group in the round
        foreach (var group in round.enemyGroups)
        {
            yield return new WaitForSeconds(group.delayBeforeGroup);

            for (int i = 0; i < group.count; i++)
            {
                // Instantiate the enemy prefab
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

                EnemyDropItems enemyDropItems = enemy.GetComponent<EnemyDropItems>();
                    
                enemyDropItems.minimumGoldCoins = round.minAmountOfGold;
                enemyDropItems.maximumGoldCoins = round.maxAmountOfGold;
                
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    if (isDoubleHP)
                    {
                        print("double HP set");
                        enemyHealth.InitializeEnemy(group.enemyHp * 2); 
                    }
                    else
                    {
                        print("NO double HP set");
                        enemyHealth.InitializeEnemy(group.enemyHp); 
                    }
                }
                
                // Apply speed modifier from the round
                EnemyFollowPath enemyMovement = enemy.GetComponent<EnemyFollowPath>();
                if (enemyMovement != null)
                {
                    enemyMovement.moveSpeed += round.speedModifer;
                }

                // Increment the enemy counter
                amountOfEnemiesSpawned++;

                // Wait for the group's spawn interval before spawning the next enemy
                yield return new WaitForSeconds(group.spawnInterval);
            }
        }

        // Wait for the wave's delay after completion
        yield return new WaitForSeconds(round.delayAfterWave);
    }
}