using System;
using TMPro;
using UnityEngine;

public class AddGold : MonoBehaviour
{
    private TMP_Text goldText;

    void Start()
    {
        goldText = GameObject.Find("Display Gold").GetComponent<TMP_Text>();
    }

    public void AddGoldToDisplay(int amount)
    {
        PlayerGold.CURRENT_PLAYER_GOLD += amount;
        goldText.text = PlayerGold.CURRENT_PLAYER_GOLD.ToString();
    }
}