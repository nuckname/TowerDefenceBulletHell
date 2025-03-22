using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateRarity : MonoBehaviour
{
    public string SelectRarity(string excludeRarity, TurretStats turretStats)
    {
        excludeRarity = null; // Currently not used in selection logic

        // Base chances
        float LegendaryChance = 0.20f;
        float RareChance = 0.30f;
        //float NormalChance = 0.50f;

        float roll = Random.Range(0f, 1f);
        string selectedRarity;

        if (roll <= LegendaryChance)
        {
            selectedRarity = "Legendary Rarity";
        }
        else if (roll <= LegendaryChance + RareChance)
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