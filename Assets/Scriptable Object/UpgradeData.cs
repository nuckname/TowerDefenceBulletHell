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
    public int key;
    public string description;
    public bool allowUpgrade;
}