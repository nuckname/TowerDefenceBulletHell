using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyUpgrade : MonoBehaviour
{
    public UpgradeData upgradeData;
    public string raritySelected;

    [SerializeField] private ShowTurretStatsButton showTurretStatsButton;
    
    
    private StoreTurretDescriptionAndRarity _storeTurretDescriptionAndRarity;
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
            showTurretStatsButton.UpdateStatsUI();
        }
    }

    private void ResetUpgradeSelection()
    {
        print("raritySelected = null now");
        raritySelected = "";
    }

    private void SetRarity(GameObject targetTurret)
    {
        _storeTurretDescriptionAndRarity = targetTurret.GetComponent<StoreTurretDescriptionAndRarity>();
        raritySelected = _storeTurretDescriptionAndRarity.storedTurretSelectedRarity;
    }

    private void ApplySelectedUpgrade(string upgradeSelected, GameObject targetTurret)
    {
        if (string.IsNullOrEmpty(raritySelected))
        {
            Debug.LogError("raritySelected is null, defaulting to Normal Rarity");
            raritySelected = "Normal Rarity";
        }
        List<Upgrade> upgrades = GetUpgradesByRarity(targetTurret);
        
        ApplyUpgradeEffect(upgradeSelected, upgrades, targetTurret);
        
    }

    //using SO. now using turret refactor. 
    private List<Upgrade> GetUpgradesByRarity(GameObject targetTurret)
    {
        UpgradeDataOnTurret upgradeDataOnTurret = targetTurret.GetComponent<UpgradeDataOnTurret>();

        switch (raritySelected)
        {
            case "Normal Rarity":
                return upgradeDataOnTurret.normalUpgrades;
            case "Rare Rarity":
                return upgradeDataOnTurret.rareUpgrades;
            case "Legendary Rarity":
                return upgradeDataOnTurret.legendaryUpgrades;
            default:
                throw new System.ArgumentException($"ERROR: Invalid rarity: {raritySelected}");
        }
    }

private void ApplyUpgradeEffect(string upgradeSelected, List<Upgrade> allUpgradesInSelectedRarity, GameObject targetTurret)
{
    SetNewUpgradePaths setNewUpgradePaths = targetTurret.GetComponent<SetNewUpgradePaths>();
    TurretStats turretStats = targetTurret.GetComponent<TurretStats>();
    UpgradeDataOnTurret upgradeDataOnTurret = targetTurret.GetComponent<UpgradeDataOnTurret>();
    
    foreach (Upgrade upgrade in allUpgradesInSelectedRarity)
    {
        if (upgrade.description == upgradeSelected)
        {
            upgrade.effect.Apply(targetTurret);
            ClearUpgradesDescription(targetTurret);
            upgradeUiManager.SetDescriptionsForUpgrades(targetTurret);

            //if ony allow once will remove it from the pool - not working
            OnlyAllowedOnce(upgrade, targetTurret, upgradeDataOnTurret);

            //if it has an upgrade path with select more - not working
            HasUpgradePath(upgrade, turretStats, upgradeDataOnTurret);
           
            break;
        }
    }
}

    private void ClearUpgradesDescription(GameObject targetTurret)
    {
        string[] tempDesc = targetTurret.GetComponent<StoreTurretDescriptionAndRarity>().storedTurretDescription;
        for (int i = 0; i < tempDesc.Length; i++)
        {
            tempDesc[i] = "";
        }
    }

    private void HasUpgradePath(Upgrade upgrade, TurretStats turretStats, UpgradeDataOnTurret upgradeDataOnTurret)
    {
        if (upgrade.hasUpgradePaths)
        {
            setNewUpgradePaths.EnableNewUpgradePath(upgrade.upgradeName, turretStats, upgradeDataOnTurret);
        }
    }

    private void OnlyAllowedOnce(Upgrade upgrade, GameObject targetTurret, UpgradeDataOnTurret upgradeDataOnTurret)
    {
        if (upgrade.onlyAllowedOnce)
        {
            switch (targetTurret.GetComponent<StoreTurretDescriptionAndRarity>().storedTurretSelectedRarity)
            {
                case "Normal Rarity":
                    int normalIndex = upgradeDataOnTurret.normalUpgrades.FindIndex(u => u.upgradeName == upgrade.upgradeName);
                    if (normalIndex >= 0)
                    {
                        upgradeDataOnTurret.normalUpgrades[normalIndex].hideUpgrade = true;
                    }
                    break;
                case "Rare Rarity":
                    int rareIndex = upgradeDataOnTurret.rareUpgrades.FindIndex(u => u.upgradeName == upgrade.upgradeName);
                    if (rareIndex >= 0)
                    {
                        upgradeDataOnTurret.rareUpgrades[rareIndex].hideUpgrade = true;
                    }
                    break;
                case "Legendary Rarity":
                    int legendaryIndex = upgradeDataOnTurret.legendaryUpgrades.FindIndex(u => u.upgradeName == upgrade.upgradeName);
                    if (legendaryIndex >= 0)
                    {
                        upgradeDataOnTurret.legendaryUpgrades[legendaryIndex].hideUpgrade = true;
                    }
                    break;
            }
        }
    }
}
