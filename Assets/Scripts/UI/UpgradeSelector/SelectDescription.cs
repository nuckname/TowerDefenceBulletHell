using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SelectDescription : MonoBehaviour
{
    private string[] upgradesSelected = new string[3];
    private int upgradeIndex = 0;

    //private Dictionary<int, string> normalUpgradeDescription = new Dictionary<int, string>();
    private Dictionary<int, string> normalUpgradeDescription;
    private Dictionary<int, string> rareUpgradeDescription = new Dictionary<int, string>();
    private Dictionary<int, string> LegendaryUpgradeDescription = new Dictionary<int, string>();

    private void Start()
    {
        print("a");
        //Normal
        normalUpgradeDescription.Add(1, "Increases the size of the turret's projectiles");
        normalUpgradeDescription.Add(2, "Increases the speed of the turret's projectiles");
        normalUpgradeDescription.Add(3, "Increases the lifetime of the turret's projectiles");
        normalUpgradeDescription.Add(4, "Increases the turret's firing rate");
        Debug.Log($"Normal Upgrade Count: {normalUpgradeDescription.Count}");

        //Rare
        rareUpgradeDescription.Add(1, "Fires an additional projectile");
        rareUpgradeDescription.Add(2, "Allows the turret to shoot multiple projectiles at once");
        rareUpgradeDescription.Add(3, "Allows projectiles to pierce through enemies");
        Debug.Log($"Rare Upgrade Count: {rareUpgradeDescription.Count}");

        //Legendary
        LegendaryUpgradeDescription.Add(1, "Projectiles chain to additional targets");
        LegendaryUpgradeDescription.Add(2, "Summons a meteor strike at the target area");
        Debug.Log($"Legendary Upgrade Count: {LegendaryUpgradeDescription.Count}");
    }

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

    
    private int[] SelectUpgrades(Dictionary<int, string> potentialUpgrades)
    {
        List<int> upgradeOptions = potentialUpgrades.Keys.ToList();
        upgradeOptions = upgradeOptions.OrderBy(x => Random.Range(0f, 1f)).ToList();
        return upgradeOptions.Take(3).ToArray();
    }

    private string[] NormalRarityUpgrades()
    {
        return SelectRarityUpgrades(normalUpgradeDescription);
    }

    private string[] RareRarityUpgrades()
    {
        return SelectRarityUpgrades(rareUpgradeDescription);
    }

    private string[] LegendaryRarityUpgrades()
    {
        return SelectRarityUpgrades(LegendaryUpgradeDescription);
    }

    private string[] SelectRarityUpgrades(Dictionary<int, string> potentialUpgrades)
    {
        int[] selectRandomUpgrade = SelectUpgrades(potentialUpgrades);

        upgradeIndex = 0;
        foreach (int key in selectRandomUpgrade)
        {
            upgradesSelected[upgradeIndex] = potentialUpgrades[key];
            print($"Selected Upgrade: {key} - {potentialUpgrades[key]}");
            upgradeIndex++;
        }

        return upgradesSelected;
    }
    
}
