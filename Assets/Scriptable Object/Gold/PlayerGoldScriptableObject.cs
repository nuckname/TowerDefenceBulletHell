using System;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerGold", menuName = "Gold/Player Gold")]
public class PlayerGoldScriptableObject : ScriptableObject
{
    public int currentGold;
    public int StartingAmountOfGold = 300; 
    
    // Event to notify when gold changes
    public event Action<int> OnGoldChanged;

    private TextMeshPro goldText;

    private void Awake()
    {
        //Set Starting amount of Gold
        StartingAmountOfGold = 300;
        currentGold = StartingAmountOfGold;
        
        //Get Display Gold Text
        goldText = GameObject.FindWithTag("Display Gold").GetComponent<TextMeshPro>();

        if (goldText == null)
        {
            Debug.LogError("Text Display is Null");
        }
    }

    public void AddGold(int amount)
    {
        Debug.Log("Added Gold.");

        currentGold += amount;
        goldText.text += currentGold;
    }

    public bool SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            Debug.Log("Spent Gold");
            currentGold -= amount;
            goldText.text += currentGold;
            return true;
        }
        Debug.Log("Not enoguh Gold");

        return false;
    }
}