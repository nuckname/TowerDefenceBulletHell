using System.Collections.Generic;
using UnityEngine;

public class ApplyUpgrade : MonoBehaviour
{
    public UpgradeData upgradeData;
    public string raritySelected;

    private StoreTurretDescription storeTurretDescription;
    private UpgradeUiManager upgradeUiManager;

    private void Awake()
    {
        upgradeUiManager = GetComponent<UpgradeUiManager>();
    }

    public void ChosenUpgrade(string upgradeSelected, GameObject targetTurret)
    {
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
        print("1 new rarity Selected: " + raritySelected);
    }

    private void ApplySelectedUpgrade(string upgradeSelected, GameObject targetTurret)
    {
        if (string.IsNullOrEmpty(raritySelected))
        {
            Debug.LogError("raritySelected is null, defaulting to Normal Rarity");
            raritySelected = "Normal Rarity";
        }

        print("raritySelected: " + raritySelected);

        var upgrades = GetUpgradesByRarity();
        ApplyUpgradeEffect(upgradeSelected, upgrades, targetTurret);
    }

    private List<Upgrade> GetUpgradesByRarity()
    {
        return raritySelected switch
        {
            "Normal Rarity" => upgradeData.normalUpgrades,
            "Rare Rarity" => upgradeData.rareUpgrades,
            "Legendary Rarity" => upgradeData.legendaryUpgrades,
            _ => throw new System.ArgumentException($"ERROR: Invalid rarity: {raritySelected}")
        };
    }

    private void ApplyUpgradeEffect(string upgradeSelected, List<Upgrade> upgrades, GameObject targetTurret)
    {
        foreach (var upgrade in upgrades)
        {
            if (upgrade.description == upgradeSelected)
            {
                upgrade.effect.Apply(targetTurret);
                ClearUpgradesDescription(targetTurret);
                upgradeUiManager.SetDescriptionsForUpgrades(targetTurret);
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
