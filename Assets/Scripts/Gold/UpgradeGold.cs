using System;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class UpgradeGold : MonoBehaviour
{
    public TMP_Text DisplayUpgradeGoldAmount;
    [SerializeField] private float scalingFactor; 

    private void Start()
    {
        scalingFactor = 1.50f;
    }

    [SerializeField] private Dictionary<string, int> basePrices = new Dictionary<string, int>
    {
        { "Normal Rarity", 50 },
        { "Rare Rarity", 100 },
        { "Legendary Rarity", 200 }
    };

    public void HardCodedUpdateGoldAmount(int amount)
    {
        DisplayUpgradeGoldAmount.text = "upgrade cost: $" + amount;
    }


    public int DisplayGold(string rarity, int totalUpgradeAmount)
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
        return roundedCost;
    }
}