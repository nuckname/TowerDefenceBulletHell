using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateRarity : MonoBehaviour
{
    public string SelectRarity(string excludeRarity, TurretStats turretStats)
    {
        excludeRarity = null; // Currently not used in selection logic

        // Base chances
        float baseLegendaryChance = 0.20f;
        float baseRareChance = 0.30f;
        float baseNormalChance = 0.50f;

        // Adjust weights based on turret stats
        // if you want max rolls. I dont want to right now.
        /*
        float legendaryModifier = Mathf.Clamp(turretStats.LegendaryIncreaseChanceForRarity * 0.05f, 0f, 0.4f);
        print("legendaryModifier: " + legendaryModifier);
        float rareModifier = Mathf.Clamp(turretStats.RareIncreaseChanceForRarity * 0.05f, 0f, 0.3f);
        print("rareModifier: " + rareModifier);
        float normalModifier = Mathf.Clamp(turretStats.NormalIncreaseChanceForRarity * 0.05f, 0f, 0.2f);
        print("rareModifier: " + rareModifier);
        */
        
        float legendaryModifier = turretStats.LegendaryIncreaseChanceForRarity * 0.05f;
        float rareModifier = turretStats.RareIncreaseChanceForRarity * 0.05f;
        float normalModifier = turretStats.NormalIncreaseChanceForRarity * 0.05f;

        // Apply modified weights
        float legendaryChance = baseLegendaryChance + legendaryModifier;
        float rareChance = baseRareChance + rareModifier;
        float normalChance = baseNormalChance + normalModifier;

        // Ensure total probability remains at 100%
        float total = legendaryChance + rareChance + normalChance;
        legendaryChance /= total;
        rareChance /= total;
        normalChance /= total;

        // Roll for rarity
        float roll = Random.Range(0f, 1f);
        string selectedRarity;

        if (roll <= legendaryChance)
        {
            selectedRarity = "Legendary Rarity";
        }
        else if (roll <= legendaryChance + rareChance)
        {
            selectedRarity = "Rare Rarity";
        }
        else
        {
            selectedRarity = "Normal Rarity";
        }

        return selectedRarity;
    }
}