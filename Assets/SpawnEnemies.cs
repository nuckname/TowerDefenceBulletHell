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

    private bool isDoubleHP = false;

    private GameModeManager gameModeManager;
    
    public int currentRound;

    private void Start()
    {
        //Call this only once somehow. 
        gameModeManager = GameObject.FindGameObjectWithTag("GameModeManager").GetComponent<GameModeManager>();
    }

    private int _freePlayTotalEnemies = 0;
    public int SpawnEnemiesPerRound(int currentRoundIndex)
    {
        if (gameModeManager.CurrentMode == GameMode.Tutorial)
        {
            currentRound++;
            //_freePlayTotalEnemies = FreePlay();
            

            return _freePlayTotalEnemies;
        }
        //used in enemy prefab
        currentRound = currentRoundIndex;
        
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
        }

        // Check if there are rounds remaining
        if (currentRoundIndex < roundsScriptableObject.Count)
        {
            // Get the current round's scriptable object
            RoundsScriptableObject currentRound = roundsScriptableObject[currentRoundIndex];
            int totalEnemies = currentRound.GetTotalEnemies();
            
            // Start spawning enemies for the current round
            StartCoroutine(SpawnEnemiesWithDelay(currentRound));

            currentRoundIndex++;
            print("totalEnemies: " + totalEnemies);
            
            return totalEnemies;
        }
        Debug.LogError("Returned 0 Enemies ERROR");
        return 0;
    }

    private int FreePlay(int currentRoundIndex)
    {
        int totalAmountOfEnemies = 0;
        
        
        return totalAmountOfEnemies;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SkipRound();
        }
    }

    private void SkipRound()
    {
        if (currentRound < roundsScriptableObject.Count - 1)
        {
            
            currentRound++;
            DeleteAllEnemies();
            StopAllCoroutines(); // Stop any ongoing enemy spawns
            SpawnEnemiesPerRound(currentRound);
            Debug.Log("Skipped to Round: " + currentRound);
        }
        else
        {
            Debug.Log("No more rounds to skip!");
        }
    }
    
    private void DeleteAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }


    private IEnumerator SpawnEnemiesWithDelay(RoundsScriptableObject round)
    {
        // Iterate through each enemy group in the round
        foreach (var group in round.enemyGroups)
        {
            yield return new WaitForSeconds(group.delayBeforeGroup);

            for (int i = 0; i < group.count; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

                EnemyDropItems enemyDropItems = enemy.GetComponent<EnemyDropItems>();

                enemy.GetComponent<EnemyDropItems>().amountOfGoldCoinsToDrop = round.amountOfGoldToDrop;
                
                
                //enemyDropItems.amountOfGoldCoinsToDrop = round.amountOfGoldToDrop;
                //enemyDropItems.amountOfHeartsToDrop = round.amountOfHeartToDrop;
                
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
                // Wait for the group's spawn interval before spawning the next enemy
                yield return new WaitForSeconds(group.spawnInterval);
            }
        }

        // Wait for the wave's delay after completion
        yield return new WaitForSeconds(round.delayAfterWave);
    }
}