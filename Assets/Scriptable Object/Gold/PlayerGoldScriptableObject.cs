using System;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerGold", menuName = "Gold/Player Gold")]
public class PlayerGoldScriptableObject : ScriptableObject
{
    public int currentGold;
    public int startingAmountOfGold;
    
    private void OnEnable()
    {
        ResetGold();
    }
    
    public void ResetGold()
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
            currentGold -= amount;
            return true;
        }
        Debug.Log("Not enoguh Gold");

        return false;
    }
}