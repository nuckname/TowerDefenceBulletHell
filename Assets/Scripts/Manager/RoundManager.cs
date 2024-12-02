using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RoundManager : MonoBehaviour
{
    public static int CURRENT_ROUND;

    public int totalEnemiesSpawnAmount = 0;

    [SerializeField] private GameObject Enemy;
    [SerializeField] private Transform enemySpawnPoint;

    [SerializeField] private List<GameObject> AllEnemiesInCurrentRound;

    [SerializeField] private GameStateManager _gameStateManager;

    private void Start()
    {
        //_gameStateManager.SwitchState();
    }


    public void CreateRound()
    {
        //Made up stats 
        float enemySpawnRateMultiplier = 1.2f;
        int spawnCount = Mathf.CeilToInt(enemySpawnRateMultiplier * CURRENT_ROUND + 4);
        Debug.Log($"Current Round: {CURRENT_ROUND}, Enemies to Spawn: {spawnCount}");

        StartCoroutine(SpawnEnemies(spawnCount, 0.75f)); 
        CURRENT_ROUND++;
    }

    private IEnumerator SpawnEnemies(int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            totalEnemiesSpawnAmount++;

            //Get Health Compoent from Enemy
            //Set EnemyStartingHealth to something.
            Instantiate(Enemy, enemySpawnPoint.position, Quaternion.identity);

            //If all enemies die then we start next round. Logic in enemy death. 
            // Wait before spawning the next enemy
            yield return new WaitForSeconds(delay);
        }
    }
}