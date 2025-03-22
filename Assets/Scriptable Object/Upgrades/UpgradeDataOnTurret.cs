using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class UpgradeDataOnTurret : MonoBehaviour
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
