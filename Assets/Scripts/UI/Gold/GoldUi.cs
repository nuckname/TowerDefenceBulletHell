using System;
using TMPro;
using UnityEngine;

public class GoldUi : MonoBehaviour
{
    [SerializeField] private PlayerGoldScriptableObject playerGold;

    [SerializeField] private TMP_Text goldText;

    private int _lastGoldAmount;
    private void Start()
    {
        if (playerGold == null)
        {
            Debug.LogError("PlayerGoldScriptableObject is not assigned in GoldUI.");
            return;
        }
    }
    
    //xdd
    private void Update()
    {
        // Check if the gold amount has changed
        if (playerGold.currentGold != _lastGoldAmount)
        {
            _lastGoldAmount = playerGold.currentGold;
            UpdateGoldUI(_lastGoldAmount);
        }
    }


    // Update the TMP_Text component when gold changes
    private void UpdateGoldUI(int goldAmount)
    {
        goldText.SetText("Gold: {0}", goldAmount);
    }

}
