using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Upgrades/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public List<Upgrade> normalUpgrades = new List<Upgrade>();
    public List<Upgrade> rareUpgrades = new List<Upgrade>();
    public List<Upgrade> legendaryUpgrades = new List<Upgrade>();
}

[System.Serializable]
public class Upgrade
{
    public string upgradeName;
    public string description;
    public UpgradeEffect effect; 
    public Rarity rarity;
}

public enum Rarity
{
    Normal,
    Rare,
    Legendary
}

public abstract class UpgradeEffect : ScriptableObject
{
    public abstract void Apply(GameObject targetTurret);
}
