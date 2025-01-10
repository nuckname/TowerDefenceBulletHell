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

    private void SpawnPlaceHolderTurret(int turretType)
    {
        GameObject turretToSpawn = null;
        switch (turretType)
        {
            case 1:
                turretToSpawn = GhostPlacementTurret;
                break;
            case 2:
                turretToSpawn = TurretBasic;
                break;
            default:
                Debug.LogError("Unknown turret type received in SpawnPlaceHolderTurretClientRpc.");
                return;
        }

        Instantiate(turretToSpawn, gameObject.transform.position, Quaternion.identity);
        
    }

    
    void Update()
    {
        //Turret Basic
        //refactor too messy
        if (Input.GetKeyDown(KeyCode.U))
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
                //try place turret
                //enable radius
                //Instantiate(GhostPlacementTurret, gameObject.transform.position, quaternion.identity);
                SpawnPlaceHolderTurret(1); // For GhostPlacementTurret
                
                GhostTurretHasBeenPlaced = true;
            }
            else
            {
                //disable radius
                //Turret has been placed
                _addGold.MinusGoldToDisplay(20);
                ghostTurret = GameObject.FindWithTag("GhostTurret");
                
                //Instantiate(TurretBasic, ghostTurret.transform.position, Quaternion.identity);
                SpawnPlaceHolderTurret(2); // For TurretBasic
                
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
