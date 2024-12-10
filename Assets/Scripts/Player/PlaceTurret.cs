using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] private GameObject TurretBasic;
    [SerializeField] private GameObject TurretCool;
    
    [SerializeField] private GameObject goldMiner;
    
    private AddGold _addGold;

    private void Awake()
    {
        _addGold = GetComponent<AddGold>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerGold.CURRENT_PLAYER_GOLD >= 20)
            {
                Instantiate(TurretBasic, gameObject.transform.position, Quaternion.identity);
               _addGold.MinusGoldToDisplay(20);
            }
            else
            {
                print("cant buy turret");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            //Place miner
            Instantiate(goldMiner, gameObject.transform.position, Quaternion.identity);
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
           //Instantiate(TurretCool, gameObject.transform.position, Quaternion.identity);
        }
    }

}
