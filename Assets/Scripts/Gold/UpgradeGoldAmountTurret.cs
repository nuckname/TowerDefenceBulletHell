using System;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class UpgradeGoldAmountTurret : MonoBehaviour
{
    public TMP_Text DisplayUpgradeGoldAmount;
    [SerializeField] private float scalingFactor; 
    [SerializeField] private float scalingPerWeight = 40f;
    
    [Header("Base Prices")]
    public int baseNormalPrice = 50;
    public int baseRarePrice = 100;
    public int baseLegendaryPrice = 200;
    
    [Header("Scaling Multipliers")]
    public float normalScaling = 1.5f;
    public float rareScaling = 1.5f;
    public float legendaryScaling = 1.5f;
    
    
    public void HardCodedUpdateGoldAmount(int amount)
    {
        DisplayUpgradeGoldAmount.text = "upgrade cost: $" + amount;
    }

    // Tracks how many upgrades of each rarity have been bought
    private Dictionary<TurretRarity, int> upgradeCounts = new Dictionary<TurretRarity, int>
    {
        { TurretRarity.Normal,    0 },
        { TurretRarity.Rare,      0 },
        { TurretRarity.Legendary, 0 },
    };

    /// <summary>
    /// Call this *after* a successful purchase to bump that rarity’s count.
    /// </summary>
    public void RegisterUpgradePurchase(TurretRarity rarity)
    {
        if (!upgradeCounts.ContainsKey(rarity))
            upgradeCounts[rarity] = 0;

        upgradeCounts[rarity]++;
    }

    public int DisplayGold(TurretRarity rarity, int totalUpgradeAmount, bool isReroll)
    {
        if (!upgradeCounts.ContainsKey(rarity))
            upgradeCounts[rarity] = 0;

        if (!isReroll)
        {
            RegisterUpgradePurchase(rarity);
        }
        
        int count = upgradeCounts[rarity];                // how many times this rarity has been bought
        int basePrice = GetBasePrice(rarity);
        float  scale     = GetScaling(rarity);

        // exponential scaling: basePrice * (scale ^ count)
        float rawCost = basePrice * Mathf.Pow(scale, count);

        // round to nearest multiple of 5
        int roundedCost = Mathf.RoundToInt(rawCost / 5f) * 5;

        DisplayUpgradeGoldAmount.text = $"upgrade cost: ${roundedCost}";
        return roundedCost;
    }
    
    
    public void RegisterPurchase(TurretRarity rarity)
    {
        upgradeCounts[rarity]++;
    }

    private int GetBasePrice(TurretRarity rarity) => rarity switch
    {
        TurretRarity.Normal    => baseNormalPrice,
        TurretRarity.Rare      => baseRarePrice,
        TurretRarity.Legendary => baseLegendaryPrice,
        _                      => baseNormalPrice
    };

    private float GetScaling(TurretRarity rarity) => rarity switch
    {
        TurretRarity.Normal    => normalScaling,
        TurretRarity.Rare      => rareScaling,
        TurretRarity.Legendary => legendaryScaling,
        _                      => 1f
    };
}



