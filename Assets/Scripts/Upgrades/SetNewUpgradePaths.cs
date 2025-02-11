using System.Collections.Generic;
using UnityEngine;

public class SetNewUpgradePaths : MonoBehaviour
{
    public UpgradeData upgradeData;

    public void EnableNewUpgradePath(string upgradeName, TurretStats turretStats)
    {
        if (upgradeName == "Homing")
        {
            EnableHomingUpgrade();
        }
        else if (upgradeName == "Reduce Blanks")
        {
            UnhideIncreaseRareRarity();
            UnhideIncreasedNormalRarity();
            UnhideIncreasedLegendaryRarity();            
        }

        if (upgradeName == "Increase Normal Rarity" ||
            upgradeName == "Increase Rare Rarity" ||
            upgradeName == "Increase Legendary Rarity" ||
            upgradeName == "Reduce Blanks")
        {
            DoubleTurretStats(turretStats);
        }
    }

    private void UnhideIncreasedNormalRarity()
    {
        if (upgradeData.normalUpgrades != null)
        {
            for (int i = 0; i < upgradeData.normalUpgrades.Count; i++)
            {
                if (upgradeData.normalUpgrades[i].upgradeName == "Increase Normal Rarity")
                {
                    upgradeData.normalUpgrades[i].hideUpgrade = false;
                }
            }
        }
        else
        {
            Debug.LogError("normalUpgrades list is null!");
        }
    }
    
    private void UnhideIncreaseRareRarity()
    {
        if (upgradeData.rareUpgrades != null)
        {
            for (int i = 0; i < upgradeData.rareUpgrades.Count; i++)
            {
                if (upgradeData.rareUpgrades[i].upgradeName == "Increase Rare Rarity")
                {
                    upgradeData.rareUpgrades[i].hideUpgrade = false;
                }
            }
        }
        else
        {
            Debug.LogError("rareUpgrades list is null!");
        }
    }
    
    private void UnhideIncreasedLegendaryRarity()
    {
        if (upgradeData.legendaryUpgrades != null)
        {
            for (int i = 0; i < upgradeData.legendaryUpgrades.Count; i++)
            {
                if (upgradeData.legendaryUpgrades[i].upgradeName == "Increase Legendary Rarity")
                {
                    upgradeData.legendaryUpgrades[i].hideUpgrade = false;
                }
            }
        }
        else
        {
            Debug.LogError("legendaryUpgrades list is null!");
        }
    }

    private void EnableHomingUpgrade()
    {
        if (upgradeData == null)
        {
            Debug.LogError("upgradeData is null!");
            return;
        }

        // Loop through rare upgrades to unhide "Increase Homing Speed"
        if (upgradeData.rareUpgrades != null)
        {
            for (int i = 0; i < upgradeData.rareUpgrades.Count; i++)
            {
                if (upgradeData.rareUpgrades[i].upgradeName == "Increase Homing Speed")
                {
                    upgradeData.rareUpgrades[i].hideUpgrade = false;
                }
            }
        }
        else
        {
            Debug.LogError("rareUpgrades list is null!");
        }
    }

    
    private void DoubleTurretStats(TurretStats turretStats)
    {
        if (turretStats.NormalIncreaseChanceForRarity > 1 &&
            turretStats.RareIncreaseChanceForRarity > 1 &&
            turretStats.LegendaryIncreaseChanceForRarity > 1 &&
            turretStats.ReduceTurretBlankChance > 1)
        {
            Debug.Log("Special Upgrade Unlocked!");
            // Add additional logic to enable/display the special upgrade.
        }
    }
}
