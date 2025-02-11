using System.Collections.Generic;
using UnityEngine;

public class SetNewUpgradePaths : MonoBehaviour
{
    public UpgradeData upgradeData;

    void Start()
    {

    }

    public void EnableNewUpgradePath(string upgradeName)
    {
        if (upgradeName == "Homing")
        {
            for (int i = 0; i <= upgradeData.rareUpgrades.Count; i++)
            {
                if (upgradeData.rareUpgrades[i].upgradeName == "Increase Homing Speed")
                {
                    upgradeData.rareUpgrades[i].hideUpgrade = false;
                }
                
                else if (upgradeData.rareUpgrades[i].upgradeName == "Increase Homing Speed")
                {
                    upgradeData.rareUpgrades[i].hideUpgrade = false;
                }
            }

        }
    }
}
