using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewRound", menuName = "Round/NewRound", order = 1)]
public class RoundDataSO : ScriptableObject
{
    [Header("Wave Configuration")]
    [Tooltip("Name of the wave (for organization purposes)")]
    public string waveName;

    public int currentWave;
    
    [Tooltip("List of enemy groups in this wave")]
    public List<EnemyGroupStats> enemyGroups;

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
    public int DEBUGtotalEnemyHpThisRound;

    [TextArea]
    public string DEBUGsummaryInfo;

    [Header("Boss")]
    public GameObject boss;

    private void OnValidate()
    {
        totalEnemies = 0;
        DEBUGtotalEnemyHpThisRound = 0;
        
        foreach (var group in enemyGroups)
        {
            totalEnemies += group.count;
            DEBUGtotalEnemyHpThisRound += group.count * group.enemyHp;
        }

        if (boss != null)
        {
            totalEnemies++;
        }

        DEBUGtotalAmountOfGoldPerRound = totalEnemies * amountOfGoldGainedForEachCoin * amountOfGoldToDrop;
        
        DEBUGsummaryInfo = 
                           $"- Total Enemies: {totalEnemies}\n" +
                           $"- Total Enemy HP: {DEBUGtotalEnemyHpThisRound}\n" +
                           $"- Gold Drop: {amountOfGoldToDrop} per enemy * {amountOfGoldGainedForEachCoin} value\n" +
                           $"- Total Gold This Round: {DEBUGtotalAmountOfGoldPerRound}\n" +
                           $"- Boss Included: {(boss != null ? "Yes" : "No")}";
    }

    public int GetTotalEnemies()
    {
        return totalEnemies;
    }
}




