using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Rarity
{
    Legendary,
    Rare,
    Normal
}

public class ApplyUpgrade : MonoBehaviour
{
    public UpgradeData upgradeData;
    public TurretRarity raritySelected;

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
        
        if (raritySelected == null)
        {
            SetRarity(targetTurret);
        }

        if (!string.IsNullOrEmpty(upgradeSelected))
        {
            ApplySelectedUpgrade(upgradeSelected, targetTurret);
            showTurretStatsButton.UpdateStatsUI();
        }
    }

    private void SetRarity(GameObject targetTurret)
    {
        _storeTurretDescriptionAndRarity = targetTurret.GetComponent<StoreTurretDescriptionAndRarity>();
        raritySelected = _storeTurretDescriptionAndRarity.storedTurretSelectedRarity;
    }

    private void ApplySelectedUpgrade(string upgradeSelected, GameObject targetTurret)
    {
        if (raritySelected == null)
        {
            Debug.LogError("raritySelected is null, defaulting to Normal Rarity");
            raritySelected = TurretRarity.Normal;
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
            case TurretRarity.Normal:
                return upgradeDataOnTurret.normalUpgrades;
            case TurretRarity.Rare:
                return upgradeDataOnTurret.rareUpgrades;
            case TurretRarity.Legendary:
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
           
            //if ony allow once will remove it from the pool - not working
            //This must go before Generate Description as if you reroll it could re into what has been hidden
            if (upgrade.onlyAllowedOnce)
            {
                OnlyAllowedOnce(upgrade, targetTurret, upgradeDataOnTurret);
            }
            
            upgradeUiManager.SetDescriptionsForUpgrades(targetTurret);

            //if it has an upgrade path with select more - not working

            if (upgrade.hasUpgradePaths)
            {
                HasUpgradePath(upgrade, turretStats, upgradeDataOnTurret, upgradeDataOnTurret, setNewUpgradePaths);
            }
           
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

    private void HasUpgradePath(Upgrade upgrade, TurretStats turretStats, UpgradeDataOnTurret upgradeDataOnTurret, UpgradeDataOnTurret targetTurret, SetNewUpgradePaths setNewUpgradePaths)
    {
        setNewUpgradePaths.EnableNewUpgradePath(upgrade.upgradeName, turretStats, upgradeDataOnTurret, targetTurret);
    }

    private void OnlyAllowedOnce(Upgrade upgrade, GameObject targetTurret, UpgradeDataOnTurret upgradeDataOnTurret)
    {
        TurretRarity currentRarity = targetTurret.GetComponent<StoreTurretDescriptionAndRarity>().GetCurrentRarity();

        List<Upgrade> poolToHide;
        switch (currentRarity)
        {
            case TurretRarity.Normal:
                poolToHide = upgradeDataOnTurret.normalUpgrades;
                break;

            case TurretRarity.Rare:
                poolToHide = upgradeDataOnTurret.rareUpgrades;
                break;

            case TurretRarity.Legendary:
                poolToHide = upgradeDataOnTurret.legendaryUpgrades;
                break;

            default:
                Debug.LogWarning($"Unknown rarity '{currentRarity}' on {targetTurret.name}");
                return;
        }

        // 3) Hide the one you picked
        int idx = poolToHide.FindIndex(u => u.upgradeName == upgrade.upgradeName);
        if (idx >= 0)
        {
            poolToHide[idx].hideUpgrade = true;
            Debug.Log($"-- Hiding upgrade {upgrade.upgradeName} from the {currentRarity} pool. Set to {poolToHide[idx].hideUpgrade}");
        }
    }

}
