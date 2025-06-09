using System;
using UnityEngine;

/// <summary>
/// Data container for upgrade options and their associated metadata.
/// </summary>
[Serializable]
public class UpgradeOption
{
    public string description;
    public string rarity;
    public int cost;
    public Sprite icon;
    
    public UpgradeOption(string desc, string rarityType, int price, Sprite upgradeIcon = null)
    {
        description = desc;
        rarity = rarityType;
        cost = price;
        icon = upgradeIcon;
    }
}

/// <summary>
/// Contains all upgrade-related data for a specific turret session.
/// </summary>
[Serializable]
public class TurretUpgradeSession
{
    public UpgradeOption[] availableUpgrades;
    public string selectedRarity;
    public int upgradeCost;
    public int rerollCost;
    public bool hasGeneratedUpgrades;
    
    public TurretUpgradeSession()
    {
        availableUpgrades = new UpgradeOption[3];
        hasGeneratedUpgrades = false;
        rerollCost = 30; // Default reroll cost
    }
    
    public bool IsValid()
    {
        return availableUpgrades != null && availableUpgrades.Length == 3 && hasGeneratedUpgrades;
    }
}

/// <summary>
/// Interface for components that can generate upgrade options.
/// </summary>
public interface IUpgradeGenerator
{
    UpgradeOption[] GenerateUpgrades(TurretStats turretStats, string excludedRarity = null);
    string GenerateRarity(TurretStats turretStats);
}

/// <summary>
/// Interface for components that handle upgrade application.
/// </summary>
public interface IUpgradeApplicator
{
    bool ApplyUpgrade(string upgradeDescription, GameObject targetTurret);
}

/// <summary>
/// Interface for components that manage upgrade costs.
/// </summary>
public interface IUpgradePricing
{
    int CalculateUpgradeCost(string rarity, int existingUpgrades);
    int CalculateRerollCost(int rerollCount);
}

/// <summary>
/// Events for the upgrade system to maintain loose coupling between components.
/// </summary>
public static class UpgradeEvents
{
    public static event Action<GameObject> OnUpgradeUIOpened;
    public static event Action<GameObject, string> OnUpgradePurchased;
    public static event Action<GameObject> OnUpgradeRerolled;
    public static event Action<GameObject> OnUpgradeUIClosed;
    
    public static void TriggerUpgradeUIOpened(GameObject turret) => OnUpgradeUIOpened?.Invoke(turret);
    public static void TriggerUpgradePurchased(GameObject turret, string upgrade) => OnUpgradePurchased?.Invoke(turret, upgrade);
    public static void TriggerUpgradeRerolled(GameObject turret) => OnUpgradeRerolled?.Invoke(turret);
    public static void TriggerUpgradeUIClosed(GameObject turret) => OnUpgradeUIClosed?.Invoke(turret);
}

/// <summary>
/// Configuration data for the upgrade system.
/// </summary>
[CreateAssetMenu(fileName = "UpgradeSystemConfig", menuName = "Upgrade System/Config")]
public class UpgradeSystemConfig : ScriptableObject
{
    [Header("Costs")]
    public int baseRerollCost = 30;
    public int[] upgradeCostsByRarity = { 50, 100, 200, 500 }; // Common, Uncommon, Rare, Legendary
    
    [Header("UI Settings")]
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 5f;
    public Color insufficientFundsColor = Color.red;
    
    [Header("Rarity Colors")]
    public Color commonColor = Color.white;
    public Color uncommonColor = Color.green;
    public Color rareColor = Color.blue;
    public Color legendaryColor = Color.yellow;
    
    public Color GetRarityColor(string rarity)
    {
        return rarity.ToLower() switch
        {
            "common" => commonColor,
            "uncommon" => uncommonColor,
            "rare" => rareColor,
            "legendary" => legendaryColor,
            _ => commonColor
        };
    }
}