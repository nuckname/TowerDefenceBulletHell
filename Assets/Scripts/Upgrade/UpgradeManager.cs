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
        //Basic Turret
        if (turretSelected.CompareTag("Turret"))
        {
            print("Turret selected: " + turretSelected);
            
            //Displays Turret UI
            UpgradeUiSwap3Lane upgradeUiSwapLnae = UiElement.GetComponent<UpgradeUiSwap3Lane>();
            if (upgradeUiSwapLnae != null)
            {
                upgradeUiSwapLnae.SetDescriptionsForUpgrades(turretSelected);
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
