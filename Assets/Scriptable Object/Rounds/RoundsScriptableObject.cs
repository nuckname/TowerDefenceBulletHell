using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRound", menuName = "Round/NewRound", order = 1)]
public class RoundsScriptableObject : ScriptableObject
{
    [Header("Wave Configuration")]
    [Tooltip("Name of the wave (for organization purposes)")]
    public string waveName;

    public int currentWave;
    
    [Tooltip("List of enemy groups in this wave")]
    public List<EnemyGroup> enemyGroups;

    [Tooltip("Delay after this wave is completed (before next wave starts)")]
    public float delayAfterWave = 2f;

    public float speedModifer = 0f;

    [Header("Wave Behavior")]
    [Tooltip("Time between spawning each enemy in the wave")]
    public float spawnInterval = 0.5f;

    [Tooltip("Total number of enemies in this wave (auto-calculated)")]
    [SerializeField] private int totalEnemies;
    
    [Header("Gold Modifier")] 
    public int amountOfGoldGainedForEachCoin = 5;
    //for freeplay?
    public float increaseGoldAmoutForEachCoinMutipler = 0;
    public int amountOfGoldToDrop = 4;
    public int amountOfHeartToDrop = 0;

    [Header("Boss")]
    public GameObject boss;
    
    // Auto-calculate total enemies when the ScriptableObject is loaded or modified
    private void OnValidate()
    {
        totalEnemies = 0;
        foreach (var group in enemyGroups)
        {
            totalEnemies += group.count;
        }

        if (boss != null)
        {
            totalEnemies++;
        }
        
        Debug.Log("SO totalEnemies: " + totalEnemies);
    }

    public int GetTotalEnemies()
    {
        return totalEnemies;
    }
}



[System.Serializable]
public class EnemyGroup
{
    [Tooltip("Determines what kind of enemy to spawn based off int to colour")]
    public int enemyHp;

    [Tooltip("Number of enemies to spawn in this group")]
    public int count;

    [Tooltip("Delay before this group starts spawning")]
    public float delayBeforeGroup = 0f;

    [Tooltip("Time between spawning each enemy in this group")]
    public float spawnInterval = 0.5f;

    [Tooltip("Does boss spawn")]
    public bool bossSpawn = false;
}

