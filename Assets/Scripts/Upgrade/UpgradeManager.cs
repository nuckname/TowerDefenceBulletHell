using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public void SelectTurretToUpGrade(GameObject turretSelected, GameObject UiElement)
    {
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
}
