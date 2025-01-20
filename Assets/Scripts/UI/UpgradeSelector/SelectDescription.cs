using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SelectDescription : MonoBehaviour
{
    private string[] upgradesSelected = new string[3];
    private int upgradeIndex = 0;

    private Dictionary<int, string> normalUpgradeDescription = new Dictionary<int, string>();
    private Dictionary<int, string> rareUpgradeDescription = new Dictionary<int, string>();
    private Dictionary<int, string> legendaryUpgradeDescription = new Dictionary<int, string>();

    //Currently Cant read in dictionary from LoadUpgradesFromFiles.cs
    //Not sure why
    //This is bad as it reads the file every time the user goes to upgrade.
    private void Awake()
    {
        LoadUpgradesFromFile("NormalUpgrades.txt", normalUpgradeDescription);
        LoadUpgradesFromFile("RareUpgrades.txt", rareUpgradeDescription);
        LoadUpgradesFromFile("LegendaryUpgrades.txt", legendaryUpgradeDescription);
    }

    private void LoadUpgradesFromFile(string fileName, Dictionary<int, string> upgradeDictionary)
    {
        string path = Path.Combine(Application.dataPath, fileName);
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 2 && int.TryParse(parts[0], out int key))
                {
                    upgradeDictionary.Add(key, parts[1]);
                }
            }
        }
        else
        {
            Debug.LogWarning($"File not found: {path}");
        }
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
        return SelectRarityUpgrades(legendaryUpgradeDescription);
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
