using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    [SerializeField] private AddGold _addGold;

    private RoundManager _roundManager;
    
    private void Awake()
    {
        _roundManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RoundManager>();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyHasDied()
    {
        //Counting enemy deaths
        _roundManager.totalEnemiesSpawnAmount -= 1;
    
        //Starts new round
        if (_roundManager.totalEnemiesSpawnAmount == 0)
        {
            _roundManager.CreateRound();
        }
        
        _addGold.AddGoldToDisplay(20);
        
        Destroy(gameObject);
    }
    
    
}
