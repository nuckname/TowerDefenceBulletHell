using System.Collections.Generic;
using Mono.Cecil.Cil;
using System.Linq;
using System;
using UnityEngine;

public class ApplyUpgrade : MonoBehaviour
{
    public UpgradeData upgradeData;
    public string raritySelected;
    
    public void ChosenUpgrade(string upgradeSelected, GameObject targetTurret)
    {
        if (upgradeSelected != null)
        {
            //Gets the correct Scriptable Object
            var upgrades = raritySelected switch
            {
                "Normal Rarity" => upgradeData.normalUpgrades,
                "Rare Rarity" => upgradeData.rareUpgrades,
                "Legendary Rarity" => upgradeData.legendaryUpgrades,
                _ => throw new System.ArgumentException($"Invalid rarity: {raritySelected}")
            };

            for (int i = 0; i < upgrades.Count; i++)
            {
                if (upgrades[i].description == upgradeSelected)
                {
                    upgrades[i].effect.Apply(this.gameObject);
                    //exit the loop once the upgrade is applied
                    break; 
                }
            }
        }
    }
}