using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SelectDescription : MonoBehaviour
{
    private string[] threePotentialUpgrades = new string[3];
    private int upgradeIndex = 0;

    public UpgradeData upgradeData;

    public string[] Get3Descriptions(string selectedRarity)
    {
        switch (selectedRarity)
        {
            case "Normal Rarity":
                return NormalRarityUpgrades();
            case "Rare Rarity":
                return RareRarityUpgrades();
            case "Legendary Rarity":
                return LegendaryRarityUpgrades();
            default:
                Debug.LogWarning("No Rarity Error");
                return null;
        }
    }


    private int[] Get3UniqueNumbers(List<Upgrade> upgradeData)
    {
        //Only add indexes of upgrades that are NOT hidden
        List<int> indexPool = new List<int>();
        for (int i = 0; i < upgradeData.Count; i++)
        {
            if (!upgradeData[i].hideUpgrade)
            {
                indexPool.Add(i);
                
            }
        }

        int[] uniqueNumbers = new int[3];
        for (int i = 0; i < uniqueNumbers.Length; i++)
        {
            int randomIndex = Random.Range(0, indexPool.Count);
            uniqueNumbers[i] = indexPool[randomIndex];
            indexPool.RemoveAt(randomIndex);
        }

        return uniqueNumbers;
    }

private string[] NormalRarityUpgrades()
    {
        return SelectRarityUpgrades(upgradeData.normalUpgrades);
    }

    private string[] RareRarityUpgrades()
    {
        return SelectRarityUpgrades(upgradeData.rareUpgrades);
    }

    private string[] LegendaryRarityUpgrades()
    {
        return SelectRarityUpgrades(upgradeData.legendaryUpgrades);
    }

    private string[] SelectRarityUpgrades(List<Upgrade> upgradeData)
    {
        int[] ThreeUniqueNumbers = Get3UniqueNumbers(upgradeData);

        upgradeIndex = 0;
        foreach (int index in ThreeUniqueNumbers)
        {
            threePotentialUpgrades[upgradeIndex] = upgradeData[ThreeUniqueNumbers[upgradeIndex]].description;
            upgradeIndex++;
        }

        return threePotentialUpgrades;
    }
    
}
