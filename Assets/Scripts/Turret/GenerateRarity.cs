using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateRarity : MonoBehaviour
{
    public string SelectRarity()
    {
        float rarityRoll = Random.Range(0, 1f);
        print(rarityRoll);
        
        if (rarityRoll <= 0.20f)
        {
            Debug.Log("Legendary Rarity");
            return "Legendary Rarity";
        }
        if (rarityRoll <= 0.50f)
        {
            Debug.Log("Rare Rarity");
            return "Rare Rarity";
        }
        if (rarityRoll <= 0.70f)
        {
            Debug.Log("Normal Rarity");
            return "Normal Rarity";
        }
        else
        {
            Debug.LogWarning("Rarity Error Returned Normal");
            return "Normal Rarity";
        }
    }
}