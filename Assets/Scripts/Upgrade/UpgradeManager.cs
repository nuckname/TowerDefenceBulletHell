using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public void SelectTurretToUpGrade(GameObject turretSelected, GameObject UiElement)
    {
        UpgradeUiSwap3Lane upgradeUiSwapLane = UiElement.GetComponent<UpgradeUiSwap3Lane>();
        if (upgradeUiSwapLane != null)
        {
            upgradeUiSwapLane.SetDescriptionsForUpgrades(turretSelected);
        }
        else
        {
            print("UpgradeUiSwap3Lane is null");
        }
    }
}
