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

    /// <summary>
    /// Returns three upgrade descriptions based on the selected rarity and turret's ability to reduce blanks.
    /// </summary>
    /// <param name="selectedRarity">The rarity level (Normal, Rare, Legendary)</param>
    /// <param name="reduceBlankChance">
    /// An integer value that reduces the chance of rolling a blank.
    /// (e.g. each point might reduce blank chance by 5%)
    /// </param>
    /// <returns>Array of three upgrade descriptions (may include empty strings for blanks)</returns>
    public string[] Get3Descriptions(string selectedRarity, int reduceBlankChance)
    {
        switch (selectedRarity)
        {
            case "Normal Rarity":
                return NormalRarityUpgrades(reduceBlankChance);
            case "Rare Rarity":
                return RareRarityUpgrades(reduceBlankChance);
            case "Legendary Rarity":
                return LegendaryRarityUpgrades(reduceBlankChance);
            default:
                Debug.LogWarning("No Rarity Error");
                return null;
        }
    }

    private int[] Get3UniqueNumbers(List<Upgrade> upgradeList)
    {
        // Only add indexes of upgrades that are NOT hidden.
        List<int> indexPool = new List<int>();
        for (int i = 0; i < upgradeList.Count; i++)
        {
            if (!upgradeList[i].hideUpgrade)
            {
                indexPool.Add(i);
            }
        }

        // If there are fewer than 3 available upgrades, adjust accordingly.
        int count = Mathf.Min(3, indexPool.Count);
        int[] uniqueNumbers = new int[count];
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, indexPool.Count);
            uniqueNumbers[i] = indexPool[randomIndex];
            indexPool.RemoveAt(randomIndex);
        }

        return uniqueNumbers;
    }

    private string[] NormalRarityUpgrades(int reduceBlankChance)
    {
        return SelectRarityUpgrades(upgradeData.normalUpgrades, reduceBlankChance);
    }

    private string[] RareRarityUpgrades(int reduceBlankChance)
    {
        return SelectRarityUpgrades(upgradeData.rareUpgrades, reduceBlankChance);
    }

    private string[] LegendaryRarityUpgrades(int reduceBlankChance)
    {
        return SelectRarityUpgrades(upgradeData.legendaryUpgrades, reduceBlankChance);
    }

    private string[] SelectRarityUpgrades(List<Upgrade> upgradeList, int reduceBlankChance)
    {
        int[] threeUniqueNumbers = Get3UniqueNumbers(upgradeList);
        upgradeIndex = 0;

        // Define a base blank chance (e.g., 25%).
        float baseBlankChance = 0.25f;
        // Define how much each point of reduceBlankChance decreases the blank chance (e.g., 5% per point).
        float reductionFactor = 0.05f;
        // Compute the effective blank chance, clamped to a minimum of 0.
        float blankChance = Mathf.Max(0f, baseBlankChance - (reduceBlankChance * reductionFactor));

        for (int i = 0; i < threeUniqueNumbers.Length; i++)
        {
            // Roll for blank chance.
            if (Random.Range(0f, 1f) < blankChance)
            {
                // Roll resulted in a blank upgrade.
                threePotentialUpgrades[upgradeIndex] = "You have rolled a blank upgrade :v(";
            }
            else
            {
                // No blank; use the upgrade description.
                int index = threeUniqueNumbers[i];
                threePotentialUpgrades[upgradeIndex] = upgradeList[index].description;
            }
            upgradeIndex++;
        }

        return threePotentialUpgrades;
    }
}
