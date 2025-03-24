using System.Collections.Generic;
using UnityEngine;

public class ApplyUpgrade : MonoBehaviour
{
    public UpgradeData upgradeData;
    public string raritySelected;

    private StoreTurretDescription storeTurretDescription;
    private UpgradeUiManager upgradeUiManager;
    private SetNewUpgradePaths setNewUpgradePaths;
    
    private void Awake()
    {
        setNewUpgradePaths = GetComponent<SetNewUpgradePaths>();
        upgradeUiManager = GetComponent<UpgradeUiManager>();
    }

    public void ChosenUpgrade(string upgradeSelected, GameObject targetTurret)
    {
        targetTurret.GetComponent<TurretStats>().totalAmountOfUpgrades++;

        if (string.IsNullOrEmpty(raritySelected))
        {
            SetRarity(targetTurret);
        }

        if (!string.IsNullOrEmpty(upgradeSelected))
        {
            ApplySelectedUpgrade(upgradeSelected, targetTurret);
        }
    }

    private void ResetUpgradeSelection()
    {
        print("raritySelected = null now");
        raritySelected = "";
    }

    private void SetRarity(GameObject targetTurret)
    {
        storeTurretDescription = targetTurret.GetComponent<StoreTurretDescription>();
        raritySelected = storeTurretDescription.storedTurretSelectedRarity;
    }

    private void ApplySelectedUpgrade(string upgradeSelected, GameObject targetTurret)
    {
        print(upgradeSelected);
        if (string.IsNullOrEmpty(raritySelected))
        {
            Debug.LogError("raritySelected is null, defaulting to Normal Rarity");
            raritySelected = "Normal Rarity";
        }
        var upgrades = GetUpgradesByRarity(targetTurret);
        
        ApplyUpgradeEffect(upgradeSelected, upgrades, targetTurret);
    }

    private List<Upgrade> GetUpgradesByRarity(GameObject targetTurret)
    {
        UpgradeDataOnTurret upgradeDataOnTurret = targetTurret.GetComponent<UpgradeDataOnTurret>();
        return raritySelected switch
        {
            "Normal Rarity" => upgradeDataOnTurret.normalUpgrades,
            "Rare Rarity" => upgradeDataOnTurret.rareUpgrades,
            "Legendary Rarity" => upgradeDataOnTurret.legendaryUpgrades,
            _ => throw new System.ArgumentException($"ERROR: Invalid rarity: {raritySelected}")
        };
    }

    private void ApplyUpgradeEffect(string upgradeSelected, List<Upgrade> upgrades, GameObject targetTurret)
    {
        SetNewUpgradePaths setNewUpgradePaths = targetTurret.GetComponent<SetNewUpgradePaths>();
        TurretStats turretStats = targetTurret.GetComponent<TurretStats>();
        UpgradeDataOnTurret upgradeDataOnTurret = targetTurret.GetComponent<UpgradeDataOnTurret>();
        
        foreach (var upgrade in upgrades)
        {
            if (upgrade.description == upgradeSelected)
            {
                upgrade.effect.Apply(targetTurret);
                ClearUpgradesDescription(targetTurret);
                upgradeUiManager.SetDescriptionsForUpgrades(targetTurret);

                if (upgrade.onlyAllowedOnce)
                {
                    //upgradePaths
                    //upgrade.hideUpgrade = true;
                }
                
                if (upgrade.hasUpgradePaths)
                {
                    //setNewUpgradePaths.EnableNewUpgradePath(upgrade.upgradeName, turretStats, upgradeDataOnTurret);
                }
                break;
            }
        }
    }

    private void ClearUpgradesDescription(GameObject targetTurret)
    {
        string[] tempDesc = targetTurret.GetComponent<StoreTurretDescription>().storedTurretDescription;
        for (int i = 0; i < tempDesc.Length; i++)
        {
            tempDesc[i] = "";
        }
    }
}
