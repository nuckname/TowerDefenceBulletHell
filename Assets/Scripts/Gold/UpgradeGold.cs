using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class UpgradeGold : MonoBehaviour
{
    public TMP_Text DisplayUpgradeGoldAmount;

    [SerializeField] private Dictionary<string, int> basePrices = new Dictionary<string, int>
    {
        { "Normal Rarity", 50 },
        { "Rare Rarity", 100 },
        { "Legendary Rarity", 200 }
    };

    [SerializeField] private float scalingFactor = 1.15f; 

    public int DisplayGold(string rarity, int totalUpgradeAmount)
    {
        if (!basePrices.ContainsKey(rarity))
        {
            Debug.LogError("Price Error: Invalid Rarity");
            return 0;
        }
        
        int basePrice = basePrices[rarity];
        int upgradedCost = Mathf.RoundToInt(basePrice * Mathf.Pow(scalingFactor, totalUpgradeAmount));
        
        DisplayUpgradeGoldAmount.text = "$" + upgradedCost.ToString();
        return upgradedCost;
    }
}