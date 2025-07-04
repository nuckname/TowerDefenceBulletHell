using System;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class UpgradeGold : MonoBehaviour
{
    public TMP_Text DisplayUpgradeGoldAmount;
    [SerializeField] private float scalingFactor; 

    [SerializeField] private Dictionary<TurretRarity, int> basePrices = new Dictionary<TurretRarity, int>
    {
        { TurretRarity.Normal, 50 },
        { TurretRarity.Rare, 100 },
        { TurretRarity.Legendary, 200 }
    };

    public void HardCodedUpdateGoldAmount(int amount)
    {
        DisplayUpgradeGoldAmount.text = "upgrade cost: $" + amount;
    }


    public int DisplayGold(TurretRarity rarity, int totalUpgradeAmount)
    {
        if (!basePrices.ContainsKey(rarity))
        {
            Debug.LogError("Price Error: Invalid Rarity");
            return 0;
        }
        
        int basePrice = basePrices[rarity];
        int upgradedCost = Mathf.RoundToInt(basePrice * Mathf.Pow(scalingFactor, totalUpgradeAmount));

        // Round to the nearest multiple of 5
        int roundedCost = Mathf.RoundToInt(upgradedCost / 5f) * 5;

        DisplayUpgradeGoldAmount.text = "upgrade cost: $" + roundedCost.ToString();

        if (roundedCost == 0)
        {
            Debug.LogError("rounded cost is zero");
            print("upgradedCost" + upgradedCost);
            print("basePrice " + basePrice);
        }
        return roundedCost;
    }
}