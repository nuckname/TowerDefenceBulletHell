using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadUpgradesFromFiles : MonoBehaviour
{
    public Dictionary<int, string> normalUpgradeDescription = new Dictionary<int, string>();
    public Dictionary<int, string> rareUpgradeDescription = new Dictionary<int, string>();
    public Dictionary<int, string> legendaryUpgradeDescription = new Dictionary<int, string>();
    
    string path = Path.Combine(Application.dataPath, "NormalUpgrades.txt");

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
}
