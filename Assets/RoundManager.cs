using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static int CURRENT_ROUND;

    private int redEnemiesSpawnAmount = 0;

    [SerializeField] private GameObject redEnemy;
    [SerializeField] private Transform enemySpawnPoint;

    void Update()
    {
        // Debugging: Press 'P' to create a round
        if (Input.GetKeyDown(KeyCode.P))
        {
            CreateRound();
        }
    }

    public void CreateRound()
    {
        //Made up stats 
        float enemySpawnRateMultiplier = 1.2f;
        int spawnCount = Mathf.CeilToInt(enemySpawnRateMultiplier * CURRENT_ROUND + 4);
        Debug.Log($"Current Round: {CURRENT_ROUND}, Enemies to Spawn: {spawnCount}");

        StartCoroutine(SpawnEnemies(spawnCount, 1f)); 
        CURRENT_ROUND++;
    }

    private IEnumerator SpawnEnemies(int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            // Spawn a red enemy
            Instantiate(redEnemy, enemySpawnPoint.position, Quaternion.identity);
            redEnemiesSpawnAmount++;
            Debug.Log($"Red Enemies Spawned: {redEnemiesSpawnAmount}");

            // Wait before spawning the next enemy
            yield return new WaitForSeconds(delay);
        }
    }
}