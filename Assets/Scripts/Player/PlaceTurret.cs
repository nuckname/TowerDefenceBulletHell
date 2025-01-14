using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
public class PlaceObject : NetworkBehaviour
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
        //refactor too messy
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsOwner)
            {
                return;
            }
            
            if (PlayerGold.CURRENT_PLAYER_GOLD <= 20)
            {
                print("not enough gold");
                return;
            }

            if (!GhostTurretHasBeenPlaced)
            {
                SpawnGhostTurret();
            }
        }

        //Spawn Basic Turret
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (GhostTurretHasBeenPlaced)
            {
                SpawnBasicTurret();    
            }
        }


        
        //Place miner
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (PlayerGold.CURRENT_PLAYER_GOLD >= 0)
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

    private void SpawnGhostTurret()
    {
        Instantiate(GhostPlacementTurret, this.gameObject.transform.position, Quaternion.identity);
        GhostTurretHasBeenPlaced = true;
    }

    private void SpawnBasicTurret()
    {
        if (GhostTurretHasBeenPlaced && PlayerGold.CURRENT_PLAYER_GOLD >= 20)
        {
            _addGold.MinusGoldToDisplay(20);
            
            ghostTurret = GameObject.FindWithTag("GhostTurret");
            Instantiate(TurretBasic, ghostTurret.transform.position, Quaternion.identity);
                                    
            Destroy(ghostTurret);
            GhostTurretHasBeenPlaced = false;
        }
    }
}
