using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateRarity : MonoBehaviour
{
    public string SelectRarity(string excludeRarity = null)
    {
        string selectedRarity;

        do
        {
            float rarityRoll = Random.Range(0, 1f);

            if (rarityRoll <= 0.20f)
            {
                selectedRarity = "Legendary Rarity";
            }
            else if (rarityRoll <= 0.50f)
            {
                selectedRarity = "Rare Rarity";
            }
            else //50 %
            {
                selectedRarity = "Normal Rarity";
            }
        }
        //Ensure the selected rarity is not the same as the excluded one
        while (selectedRarity == excludeRarity); 

        Debug.Log($"Selected Rarity: {selectedRarity}");
        return selectedRarity;
    }
}