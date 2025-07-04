using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateRarity : MonoBehaviour
{
    public TurretRarity SelectRarity(TurretRarity excludeRarity, TurretStats turretStats)
    {
        // Base chances
        float LegendaryChance = 0.20f;
        float RareChance = 0.30f;
        //float NormalChance = 0.50f;

        float roll = Random.Range(0f, 1f);
        TurretRarity selectedRarity;

        if (roll <= LegendaryChance)
        {
            selectedRarity = TurretRarity.Legendary;
        }
        else if (roll <= LegendaryChance + RareChance)
        {
            selectedRarity = TurretRarity.Rare;
        }
        else
        {
            selectedRarity = TurretRarity.Normal;
        }

        return selectedRarity;
    }
}