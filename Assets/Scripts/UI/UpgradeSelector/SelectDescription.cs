using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectDescription : MonoBehaviour
{
    public string[] Get3Descriptions(string selectedRarity)
    {
        if (selectedRarity == "Normal Rarity")
        {
            string[] upgrades = NormailRaritUpgrades();
            return upgrades;
        }

        else if (selectedRarity == "Common Rarity")
        {
            
        }
        
        else if (selectedRarity == "Rare Rarity")
        {
            
        }
        
        else if (selectedRarity == "Legendary Rarity")
        {
            
        }
        return null;
    }
    
    private int[] SelectUpgrades()
    {
        //Returns 3 unique numbers
        List<int> upgradeOptions = new List<int> { 1, 2, 3, 4 };
        upgradeOptions = upgradeOptions.OrderBy(x => Random.value).ToList(); // Shuffle the list

        int[] selectedUpgrades = upgradeOptions.Take(3).ToArray(); // Take the first three unique numbers

        return selectedUpgrades;
    }

    private string[] NormailRaritUpgrades()
    {
        int[] selectRandomUpgrade = SelectUpgrades();

        foreach (int upgrade in selectRandomUpgrade)
        {
            if (upgrade == 1)
            {
                print("increase bullet size");
                //increase bullet size
            }
        
            else if (upgrade == 2)
            {
                print("increase bullet speed");
                //increase bullet speed
            }
            
            else if (upgrade == 3)
            {
                print("increase bullet lifetime");

                //increase bullet life time
            }
            
            else if (upgrade == 4)
            {
                print("increase turret fire rate");

                //increase Turret fire rate
            }
        }

        return null;
    }
}
