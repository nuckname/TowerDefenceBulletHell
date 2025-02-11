using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Upgrades/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public List<Upgrade> normalUpgrades = new List<Upgrade>();
    public List<Upgrade> rareUpgrades = new List<Upgrade>();
    public List<Upgrade> legendaryUpgrades = new List<Upgrade>();

    private void OnEnable()
    {
        ResetOnlyAllowedOnceUpgrades();
    }

    /// <summary>
    /// Iterates through all upgrade lists and for each upgrade that is only allowed once,
    /// sets the hideUpgrade flag to false.
    /// </summary>
    private void ResetOnlyAllowedOnceUpgrades()
    {
        foreach (var upgrade in normalUpgrades)
        {
            if (upgrade.onlyAllowedOnce)
            {
                upgrade.hideUpgrade = false;
            }
            
            if (upgrade.onlyAllowedOnce)
            {
                upgrade.hideUpgrade = false;
            }
            
            if (upgrade.isAnUpgradePaths)
            {
                upgrade.hideUpgrade = true;
            }
        }

        foreach (var upgrade in rareUpgrades)
        {
            if (upgrade.onlyAllowedOnce)
            {
                upgrade.hideUpgrade = false;
            }
            
            if (upgrade.isAnUpgradePaths)
            {
                upgrade.hideUpgrade = true;
            }
        }

        foreach (var upgrade in legendaryUpgrades)
        {
            if (upgrade.onlyAllowedOnce)
            {
                upgrade.hideUpgrade = false;
            }
            
            if (upgrade.isAnUpgradePaths)
            {
                upgrade.hideUpgrade = true;
            }
        }
    }
}

[System.Serializable]
public class Upgrade
{
    public string upgradeName;
    public string description;
    public UpgradeEffect effect; 
    public bool onlyAllowedOnce = false;
    public bool hideUpgrade = false;
    
    public bool hasUpgradePaths = false;
    public bool isAnUpgradePaths = false;
}

public abstract class UpgradeEffect : ScriptableObject
{
    public abstract void Apply(GameObject targetTurret);
}