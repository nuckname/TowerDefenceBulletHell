using System.Collections.Generic;
using UnityEngine;

public class SetNewUpgradePaths : MonoBehaviour
{
    public UpgradeData upgradeData;

    public void EnableNewUpgradePath(string upgradeName, TurretStats turretStats, UpgradeDataOnTurret upgradeDataOnTurret)
    {
        print("EnableNewUpgradePath");
        if (turretStats.pierceCount >= 2)
        {
            CanBounce();
        }

        if (turretStats.allow4ShootPoints || turretStats.allowDiagonalShooting && turretStats.extraProjectiles >= 1 ||
            turretStats.multiShotCount >= 1)
        {
            
        }
        
        if (turretStats.pierceCount >= 4)
        {
            CanChain();
        }

        if (upgradeName == "Homing")
        {
            print("Homing upgrade enabled");
            EnableHomingUpgrade();
        }

    }

    private void CanBounce()
    {
        for (int i = 0; i < upgradeData.rareUpgrades.Count; i++)
        {
            if (i >= upgradeData.normalUpgrades.Count) 
            {
                Debug.LogError($"Index {i} is out of range for normalUpgrades (Count: {upgradeData.normalUpgrades.Count}).");
                break; // Prevents out-of-bounds errors.
            }
            
            if (upgradeData.rareUpgrades[i].upgradeName == "Can Bounce")
            {
                upgradeData.rareUpgrades[i].hideUpgrade = false;
                Debug.Log($"<color=green> Upgrade Unlocked: {upgradeData.rareUpgrades[i].upgradeName} </color> ");
            }
        }
        
        for (int i = 0; i < upgradeData.rareUpgrades.Count; i++)
        {
            if (i >= upgradeData.normalUpgrades.Count) 
            {
                Debug.LogError($"Index {i} is out of range for normalUpgrades (Count: {upgradeData.normalUpgrades.Count}).");
                break; // Prevents out-of-bounds errors.
            }
            
            if (upgradeData.normalUpgrades[i].upgradeName == "Increase Bounces")
            {
                upgradeData.normalUpgrades[i].hideUpgrade = false;
                Debug.Log($"<color=green> Upgrade Unlocked: {upgradeData.rareUpgrades[i].upgradeName} </color> ");

            }
        }
    }
    
    private void CanChain()
    {
        for (int i = 0; i < upgradeData.rareUpgrades.Count; i++)
        {
            if (i >= upgradeData.normalUpgrades.Count) 
            {
                Debug.LogError($"Index {i} is out of range for normalUpgrades (Count: {upgradeData.normalUpgrades.Count}).");
                break; // Prevents out-of-bounds errors.
            }
            
            if (upgradeData.normalUpgrades[i].upgradeName == "Ricochet")
            {
                upgradeData.normalUpgrades[i].hideUpgrade = false;
                Debug.Log($"<color=green> Upgrade Unlocked: {upgradeData.rareUpgrades[i].upgradeName} </color> ");

            }
        }
    }

private void UnhideIncreasedNormalRarity()
    {
        if (upgradeData.normalUpgrades != null)
        {
            for (int i = 0; i < upgradeData.normalUpgrades.Count; i++)
            {
                if (i >= upgradeData.normalUpgrades.Count) 
                {
                    Debug.LogError($"Index {i} is out of range for normalUpgrades (Count: {upgradeData.normalUpgrades.Count}).");
                    break; // Prevents out-of-bounds errors.
                }
                
                if (upgradeData.normalUpgrades[i].upgradeName == "Increase Normal Rarity")
                {
                    upgradeData.normalUpgrades[i].hideUpgrade = false;
                    Debug.Log($"<color=green> Upgrade Unlocked: {upgradeData.rareUpgrades[i].upgradeName} </color> ");

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
                    Debug.Log($"<color=green> Upgrade Unlocked: {upgradeData.rareUpgrades[i].upgradeName} </color> ");

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
                    Debug.Log($"<color=green> Upgrade Unlocked: {upgradeData.rareUpgrades[i].upgradeName} </color> ");

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
                    Debug.Log($"<color=green> Upgrade Unlocked: {upgradeData.rareUpgrades[i].upgradeName} </color> ");

                }
            }
        }
        else
        {
            Debug.LogError("rareUpgrades list is null!");
        }
    }
}
