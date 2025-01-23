using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    [SerializeField] private int playerStartingGold = 150;
    public static int CURRENT_PLAYER_GOLD = 150;
    
    
    [SerializeField] private AddGold _addGold;

    private void Start()
    {
        _addGold.AddGoldToDisplay(playerStartingGold);
    }

    public static void UpdateGold()
    {
        
    }

}
