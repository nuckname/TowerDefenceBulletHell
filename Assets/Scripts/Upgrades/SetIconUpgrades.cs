using System;
using UnityEngine.UI;
using UnityEngine;

public class SetIconUpgrades : MonoBehaviour
{
    [SerializeField] private Image imageBoxTop;
    [SerializeField] private Image imageBoxMiddle;
    [SerializeField] private Image imageBoxBottom;

    [SerializeField] private GameObject turret;
    private UpgradeDataOnTurret upgradeDataOnTurret;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        upgradeDataOnTurret = turret.GetComponent<UpgradeDataOnTurret>();
    }

    public void SetIcons(string[] UpgradeDescriptionSelected, string raritySelected)
    {
        if (raritySelected == "Legendary Rarity")
        {
            foreach (Upgrade upgrade in upgradeDataOnTurret.legendaryUpgrades)
            {
                if (upgrade.description == UpgradeDescriptionSelected[0])
                {
                    imageBoxTop.sprite = upgrade.upgradeIcon;
                }
            
                if (upgrade.description == UpgradeDescriptionSelected[1])
                {
                    imageBoxMiddle.sprite = upgrade.upgradeIcon;
                }
                        
                if (upgrade.description == UpgradeDescriptionSelected[2])
                {
                    imageBoxBottom.sprite = upgrade.upgradeIcon;
                }
            }
        }
        if (raritySelected == "Rare Rarity")
        {
            foreach (Upgrade upgrade in upgradeDataOnTurret.rareUpgrades)
            {
                if (upgrade.description == UpgradeDescriptionSelected[0])
                {
                    imageBoxTop.sprite = upgrade.upgradeIcon;
                }
            
                if (upgrade.description == UpgradeDescriptionSelected[1])
                {
                    imageBoxMiddle.sprite = upgrade.upgradeIcon;
                }
                        
                if (upgrade.description == UpgradeDescriptionSelected[2])
                {
                    imageBoxBottom.sprite = upgrade.upgradeIcon;
                }
            }
        }

        if (raritySelected == "Normal Rarity")
        {
            foreach (Upgrade upgrade in upgradeDataOnTurret.normalUpgrades)
            {
                if (upgrade.description == UpgradeDescriptionSelected[0])
                {
                    imageBoxTop.sprite = upgrade.upgradeIcon;
                }
            
                if (upgrade.description == UpgradeDescriptionSelected[1])
                {
                    imageBoxMiddle.sprite = upgrade.upgradeIcon;
                }
                        
                if (upgrade.description == UpgradeDescriptionSelected[2])
                {
                    imageBoxBottom.sprite = upgrade.upgradeIcon;

                }
            }
        }
        
    }
}
