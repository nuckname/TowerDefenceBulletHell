using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateRarity : MonoBehaviour
{

    public string SelectRarity()
    {
        float rarityRoll = Random.Range(0, 0.99f);

        if (rarityRoll <= 0.20f)
        {
            Debug.Log("Legendary Rarity");
            return "Legendary Rarity";
        }
        if (rarityRoll <= 0.40f)
        {
            Debug.Log("Rare Rarity");
            return "Rare Rarity";
        }
        if (rarityRoll <= 0.60f)
        {
            Debug.Log("Common Rarity");
            return "Common Rarity";
        }
        else
        {
            Debug.Log("Error");
            return "Error";
        }
    }
}