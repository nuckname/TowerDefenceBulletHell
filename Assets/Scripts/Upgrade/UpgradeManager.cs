using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    
    //Select What Game Obhect has gottne the upgrade
    
    //Ui determines what to give gameObject

    private void Awake()
    {
           
    }
    
    public void SelectTurretToUpGrade(GameObject turretSelected, GameObject UiElement)
    {
        if (turretSelected.CompareTag("Turret"))
        {
            print("Turret selected: " + turretSelected);
            
            //Go to Turret and Get Turret array stuff
            string[] upgrades = turretSelected.GetComponent<BasicTurretUpgrades>().TierOneText_BasicTurretUpgrades();
            
            //Displays Turret UI
            UpgradeUiSwap3Lane upgradeUiSwapLnae = UiElement.GetComponent<UpgradeUiSwap3Lane>();
            if (upgradeUiSwapLnae != null)
            {
                upgradeUiSwapLnae.SetDescriptionsForUpgrades(upgrades, turretSelected);
            }
            else
            {
                print("UpgradeUiSwap3Lane is null");
            }
            
        }
        
        else if (turretSelected.CompareTag("SpiralTurret"))
        {
            
        }
    }

}
