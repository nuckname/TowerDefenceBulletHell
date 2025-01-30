using UnityEngine;

public class ApplyUpgrade : MonoBehaviour
{
    public UpgradeData upgradeData;
    public string raritySelected;

    private StoreTurretDescription storeTurretDescription;
    private UpgradeUiManager upgradeUiManager;
    
    private void Awake()
    {
        upgradeUiManager = GetComponent<UpgradeUiManager>();
    }

    public void ChosenUpgrade(string upgradeSelected, GameObject targetTurret)
    {
        //fixes a bug where UpgradeUiMager was getting destroied and so was raritySelected.
        //EG: when you press q and then reselect and pick an upgrade
        if (raritySelected == "")
        {
            storeTurretDescription = targetTurret.GetComponent<StoreTurretDescription>();
            raritySelected = storeTurretDescription.storedTurretSelectedRarity;
            print("1 new rarity Selected:" + raritySelected);
        }
        
        if (upgradeSelected != null)
        {
            if (raritySelected == null)
            {
                Debug.LogError("raritySelected is null, made is Normal Rarity");
                raritySelected = "Normal Rarity";
            }
            print("raritySelected: " + raritySelected);
            //Gets the correct Scriptable Object
            var upgrades = raritySelected switch
            {
                "Normal Rarity" => upgradeData.normalUpgrades,
                "Rare Rarity" => upgradeData.rareUpgrades,
                "Legendary Rarity" => upgradeData.legendaryUpgrades,
                _ => throw new System.ArgumentException($"ERROR: Invalid rarity: {raritySelected}")
                //_ => upgradeData.normalUpgrades

            };

            for (int i = 0; i < upgrades.Count; i++)
            {
                if (upgrades[i].description == upgradeSelected)
                {
                    upgrades[i].effect.Apply(targetTurret);

                    ClearUpgradesDescription(targetTurret);
                    
                    //Regenerate Description
                    upgradeUiManager.SetDescriptionsForUpgrades(targetTurret);

                    //Maybe a setting that turns off ui?
                    //upgradeUiManager.TurnOffAllUi(true);
                    
                    break; 
                }
            }
        }
    }

    private void ClearUpgradesDescription(GameObject targetTurret)
    {
        string[] tempDesc = targetTurret.GetComponent<StoreTurretDescription>().storedTurretDescription;
        for (int j = 0; j < tempDesc.Length; j++)
        {
            tempDesc[j] = "";
        }
    }
    
}