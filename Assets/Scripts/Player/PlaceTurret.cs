using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] private GameObject TurretBasic;
    [SerializeField] private GameObject TurretCool;

    [SerializeField] private GameObject GhostPlacementTurret;
    
    [SerializeField] private GameObject goldMiner;
    
    [SerializeField] private AddGold _addGold;

    [SerializeField] private GameObject ghostTurret;
    private bool GhostTurretHasBeenPlaced = false;

    private void Awake()
    {
        _addGold = GetComponent<AddGold>();
    }
    
    void Update()
    {
        //Turret Basic
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerGold.CURRENT_PLAYER_GOLD <= 20)
            {
                print("not enough gold");
                return;
            }

            if (!GhostTurretHasBeenPlaced)
            {
                //try place turret
                //enable radius
                Instantiate(GhostPlacementTurret, gameObject.transform.position, quaternion.identity);
                GhostTurretHasBeenPlaced = true;
            }
            else
            {
                //disable radius
                //Turret has been placed
                _addGold.MinusGoldToDisplay(20);
                ghostTurret = GameObject.FindWithTag("GhostTurret"); 
                Instantiate(TurretBasic, ghostTurret.transform.position, Quaternion.identity);
                Destroy(ghostTurret);
                GhostTurretHasBeenPlaced = false;
            }
        }
        
        //Place miner
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (PlayerGold.CURRENT_PLAYER_GOLD >= 00)
            {
                Instantiate(goldMiner, gameObject.transform.position, Quaternion.identity);
                _addGold.MinusGoldToDisplay(100);
            }


        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
           //Instantiate(TurretCool, gameObject.transform.position, Quaternion.identity);
        }
    }

}
