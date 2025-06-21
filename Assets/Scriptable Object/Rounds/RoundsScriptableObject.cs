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
    //public float increaseGoldAmoutForEachCoinMutipler = 0;
    public int amountOfGoldToDrop = 4;
    public int amountOfHeartToDrop = 0;
    
    public int DEBUGtotalAmountOfGoldPerRound;


    [Header("Boss")]
    public GameObject boss;

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

        DEBUGtotalAmountOfGoldPerRound = totalEnemies * amountOfGoldGainedForEachCoin * amountOfGoldToDrop;
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

    [Header("Special - must tick to use below")]
    public bool isSpecial = false;

    [Header("Ground Effects")]
    public bool hasOnDeathEffect = false;
    public bool iceOnDeathEffect = false;
    
    [Header("Ground Effects")]
    public bool hasFogOfWar = false;
    public bool hasPaintSpeedEffect = false;

    [Header("Shield Effects")]
    public bool hasSheild = false;
    
    [Header("Shield HP")]
    public int shieldHp;
    
    [Header("Rotation")]
    public bool isRotating = false;
    public bool clockWise = false;
    public bool counterClockWise = false;

    [Header("Direction")]
    public bool north = false;
    public bool east = false;
    public bool south = false;
    public bool west = false;
    
    [Header("Diagonal")]
    public bool northEast = false;
    public bool northWest = false;
    public bool southEast = false;
    public bool southWest = false;


}

