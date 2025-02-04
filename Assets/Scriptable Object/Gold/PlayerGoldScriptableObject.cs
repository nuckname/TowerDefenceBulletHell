using System;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerGold", menuName = "Gold/Player Gold")]
public class PlayerGoldScriptableObject : ScriptableObject
{
    public int currentGold;
    public int startingAmountOfGold;

    private void OnEnable()
    {
        startingAmountOfGold = 300;
        currentGold = startingAmountOfGold;
    }
    
    private void Reset()
    {
        currentGold = startingAmountOfGold;
    }

    public void AddGold(int amount)
    {
        Debug.Log("Added Gold.");

        currentGold += amount;
    }

    public bool SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            Debug.Log("Spent Gold");
            currentGold -= amount;
            return true;
        }
        Debug.Log("Not enoguh Gold");

        return false;
    }
}