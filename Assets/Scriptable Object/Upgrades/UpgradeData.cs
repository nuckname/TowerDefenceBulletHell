using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Upgrades/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public List<Upgrade> normalUpgrades = new List<Upgrade>();
    public List<Upgrade> rareUpgrades = new List<Upgrade>();
    public List<Upgrade> legendaryUpgrades = new List<Upgrade>();

    private void OnEnable()
    {
        InitializeUpgrades(normalUpgrades);
        InitializeUpgrades(rareUpgrades);
        InitializeUpgrades(legendaryUpgrades);
    }

    private void InitializeUpgrades(List<Upgrade> upgrades)
    {
        foreach (var upgrade in upgrades)
        {
            // Set runtime variables based on default values
            upgrade.onlyAllowedOnce = upgrade.defaultOnlyAllowedOnce;
            upgrade.hideUpgrade = upgrade.defaultHideUpgrade;
            upgrade.hasUpgradePaths = upgrade.defaultHasUpgradePaths;
            upgrade.isAnUpgradePaths = upgrade.defaultIsAnUpgradePaths;
        }
    }

}

// Helper class for serialization
[System.Serializable]
public class UpgradeSaveData
{
    public List<SerializableUpgrade> normalUpgrades;
    public List<SerializableUpgrade> rareUpgrades;
    public List<SerializableUpgrade> legendaryUpgrades;
}

// Serializable version of Upgrade (without ScriptableObject fields)
[System.Serializable]
public class SerializableUpgrade
{
    public string upgradeName;
    public bool hideUpgrade;
    public bool onlyAllowedOnce;
    public bool hasUpgradePaths;
    public bool isAnUpgradePaths;
}

[System.Serializable]
public class Upgrade
{
    public string upgradeName;
    public string description;
    public UpgradeEffect effect;

    // Runtime variables
    [Header("Only Allowed Once")]
    public bool defaultOnlyAllowedOnce = false; // Default for onlyAllowedOnce
    public bool onlyAllowedOnce = false;
    
    [Header("Hide Upgrade")]
    public bool defaultHideUpgrade = false;    // Default for hideUpgrade
    public bool hideUpgrade = false;
    
    [Header("This Upgrade has an Path that leads to another upgrade")]
    public bool defaultHasUpgradePaths = false; // Default for hasUpgradePaths
    public bool hasUpgradePaths = false;
    
    [Header("This is an Upgrade Path. Conditions must be hit so it can spawn")]
    public bool defaultIsAnUpgradePaths = false; // Default for isAnUpgradePaths
    public bool isAnUpgradePaths = false;
}

public abstract class UpgradeEffect : ScriptableObject
{
    public abstract void Apply(GameObject targetTurret);
}