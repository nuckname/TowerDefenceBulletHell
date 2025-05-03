using System.Collections.Generic;
using UnityEngine;

public class SetNewUpgradePaths : MonoBehaviour
{

    [SerializeField] 
    private int amountOfRares = 6;
    public void EnableNewUpgradePath(string upgradeName, TurretStats turretStats, UpgradeDataOnTurret upgradeDataOnTurret, UpgradeDataOnTurret targetTurret)
    {
        print("EnableNewUpgradePath");
        if (turretStats.pierceCount >= 2)
        {
            //CanBounce();
        }

        if (turretStats.TurretShootsBackwards)
        {
            CanShootSideWays(upgradeDataOnTurret);
        }
        
        if (turretStats.pierceCount >= 4)
        {
           // CanChain();
        }

        if (upgradeName == "Homing")
        {
            print("Homing upgrade enabled");
           // EnableHomingUpgrade();
        }
        
        OutOfRareUpgrade(turretStats);
        SetRanOutOfUpgrades(upgradeDataOnTurret);
    }

    private void CanShootSideWays(UpgradeDataOnTurret upgradeDataOnTurret)
    {
        for (int i = 0; i < upgradeDataOnTurret.rareUpgrades.Count; i++)
        {
            if (upgradeDataOnTurret.rareUpgrades[i].upgradeName == "Shoot Sideways")
            {
                upgradeDataOnTurret.rareUpgrades[i].hideUpgrade = false;
                Debug.Log($"<color=green> Upgrade Unlocked: {upgradeDataOnTurret.rareUpgrades[i].upgradeName} </color> ");
            }
        }
    }

    private void SetRanOutOfUpgrades(UpgradeDataOnTurret upgradeDataOnTurret)
    {
        if (amountOfRares == 3)
        {
            for (int i = 0; i < upgradeDataOnTurret.rareUpgrades.Count; i++)
            {
                //Should Put this in a function. But whatever. 
                if (upgradeDataOnTurret.rareUpgrades[i].upgradeName == "Rare Increase FireRate")
                {
                    upgradeDataOnTurret.rareUpgrades[i].hideUpgrade = false;
                    Debug.Log($"<color=green> Upgrade Unlocked: {upgradeDataOnTurret.rareUpgrades[i].upgradeName} </color> ");
                }
            }
        }
        
        else if (amountOfRares == 2)
        {
            for (int i = 0; i < upgradeDataOnTurret.rareUpgrades.Count; i++)
            {
                if (upgradeDataOnTurret.rareUpgrades[i].upgradeName == "Rare Increase Bullet Speed")
                {
                    upgradeDataOnTurret.rareUpgrades[i].hideUpgrade = false;
                    Debug.Log($"<color=green> Upgrade Unlocked: {upgradeDataOnTurret.rareUpgrades[i].upgradeName} </color> ");
                }
            }
        }
        
        else if (amountOfRares == 1)
        {
            for (int i = 0; i < upgradeDataOnTurret.rareUpgrades.Count; i++)
            {
                if (upgradeDataOnTurret.rareUpgrades[i].upgradeName == "Rare Increase Projectile Life")
                {
                    upgradeDataOnTurret.rareUpgrades[i].hideUpgrade = false;
                    Debug.Log($"<color=green> Upgrade Unlocked: {upgradeDataOnTurret.rareUpgrades[i].upgradeName} </color> ");
                }
            }
        }
    }

    private void OutOfRareUpgrade(TurretStats turretStats)
    {

        if (turretStats.activeDirections >= 2)
        {
            print("-1");
            amountOfRares--;
        }
        
        else if (turretStats.activeDirections >= 4)
        {
            print("-1");

            amountOfRares--;
        }
        
        else if (turretStats.activeDirections >= 6)
        {
            print("-1");

            amountOfRares--;
        }
        else if (turretStats.activeDirections >= 8)
        {
            print("-1");

            amountOfRares--;
        }
        
        else if (turretStats.enableOrbit)
        {
            print("-1");

            amountOfRares--;
        }

        else if (turretStats.GoldOnHit)
        {
            print("-1");

            amountOfRares--;
            
        }

        //if()
        
    }

    //Not sure to include this.
    private void DiagnoalBotLeft(UpgradeDataOnTurret upgradeDataOnTurret)
    {
        for (int i = 0; i < upgradeDataOnTurret.rareUpgrades.Count; i++)
        {
            if (upgradeDataOnTurret.rareUpgrades[i].upgradeName == "Diagonal shooting Bot left")
            {
                upgradeDataOnTurret.rareUpgrades[i].hideUpgrade = false;
                Debug.Log($"<color=green> Upgrade Unlocked: {upgradeDataOnTurret.rareUpgrades[i].upgradeName} </color> ");
            }
        }
    }
/*
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
     */
}
