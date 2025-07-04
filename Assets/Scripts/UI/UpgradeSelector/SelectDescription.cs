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
    private string[] NormalUpgradesHardCodedOrder = new string[3];


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
    ///
    ///

    public string[] Get3Descriptions(TurretRarity selectedRarity, UpgradeDataOnTurret upgradeDataOnTurret)
    {
        switch (selectedRarity)
        {
            case TurretRarity.Normal:
                //Hard Coded order upgrades
                NormalUpgradesHardCodedOrder[0] = "Increases projectile speed";
                NormalUpgradesHardCodedOrder[1] = "Increases projectile lifetime";
                NormalUpgradesHardCodedOrder[2] = "Increases firing rate";
                return NormalUpgradesHardCodedOrder;
            case TurretRarity.Rare:
                return SelectThreeUpgrades(upgradeDataOnTurret.rareUpgrades);
            case TurretRarity.Legendary:
                return SelectThreeUpgrades(upgradeDataOnTurret.legendaryUpgrades);
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

    private string[] SelectThreeUpgrades(List<Upgrade> upgradeList)
    {
        int[] threeUniqueNumbers = Get3UniqueNumbers(upgradeList);
        upgradeIndex = 0;

        for (int i = 0; i < threeUniqueNumbers.Length; i++)
        {
            int index = threeUniqueNumbers[i];
            threePotentialUpgrades[upgradeIndex] = upgradeList[index].description;
       
            upgradeIndex++;
        }

        return threePotentialUpgrades;
    }
}
