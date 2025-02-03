using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    
    [SerializeField] private Transform spawnPoint; // Where enemies will spawn
    public List<RoundsScriptableObject> roundsScriptableObject; // List of scriptable objects for each round
    public int amountOfEnemiesSpawned = 0; // Counter for enemies spawned

    private void Start()
    {
        
    }

    public int SpawnEnemiesPerRound(int currentRoundIndex)
    {
        // Check if there are rounds left to spawn
        if (currentRoundIndex < roundsScriptableObject.Count)
        {
            // Get the current round's scriptable object
            RoundsScriptableObject currentRound = roundsScriptableObject[currentRoundIndex];

            if (currentRound == null)
            {
                Debug.LogError($"Round at index {currentRoundIndex} is null. Please assign a valid RoundsScriptableObject.");
            }

            int totalEnemies = currentRound.GetTotalEnemies();
            
            
            // Start spawning enemies for the current round
            StartCoroutine(SpawnEnemiesWithDelay(currentRound));

            // Move to the next round
            currentRoundIndex++;
            print("totalEnemies: " + totalEnemies);
            return totalEnemies;
        }
        else
        {
            Debug.Log("All rounds completed!");
        }

        return amountOfEnemiesSpawned;
    }

    private IEnumerator SpawnEnemiesWithDelay(RoundsScriptableObject round)
    {
        if (round == null)
        {
            Debug.LogError("Round is null. Cannot spawn enemies.");
            yield break;
        }

        // Check if the wave should spawn enemies in random order
        if (round.randomizeSpawnOrder)
        {
            ShuffleEnemyGroups(round.enemyGroups);
        }

        // Iterate through each enemy group in the round
        foreach (var group in round.enemyGroups)
        {
            if (group == null)
            {
                Debug.LogError("EnemyGroup is null. Skipping this group.");
                continue;
            }

            if (enemyPrefab == null)
            {
                Debug.LogError("EnemyPrefab in EnemyGroup is null. Skipping this group.");
                continue;
            }

            // Wait for the group's delay before spawning
            yield return new WaitForSeconds(group.delayBeforeGroup);

            // Spawn enemies in the current group
            for (int i = 0; i < group.count; i++)
            {
                // Instantiate the enemy prefab
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.InitializeEnemy(group.enemyHp); 
                }
                else
                {
                    Debug.LogError("Enemy prefab is missing the EnemyHealth component.");
                }
                
                if (enemy == null)
                {
                    Debug.LogError("Failed to instantiate enemy prefab.");
                    continue;
                }
                
                //Set enemy Colour

                // Apply speed modifier from the round
                EnemyFollowPath enemyMovement = enemy.GetComponent<EnemyFollowPath>();
                if (enemyMovement != null)
                {
                    enemyMovement.moveSpeed += round.speedModifer;
                }
                else
                {
                    Debug.LogError("Enemy prefab is missing the EnemyFollowPath component.");
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

    // Helper method to shuffle enemy groups (if randomizeSpawnOrder is true)
    private void ShuffleEnemyGroups(List<EnemyGroup> groups)
    {
        for (int i = 0; i < groups.Count; i++)
        {
            EnemyGroup temp = groups[i];
            int randomIndex = Random.Range(i, groups.Count);
            groups[i] = groups[randomIndex];
            groups[randomIndex] = temp;
        }
    }
}