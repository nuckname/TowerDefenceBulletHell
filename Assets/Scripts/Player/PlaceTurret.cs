using System;
using UnityEngine;
using Unity.Netcode;

public class PlaceObject : NetworkBehaviour
{
    [SerializeField] private GameObject TurretBasic;
    [SerializeField] private GameObject GhostPlacementTurret;
    [SerializeField] private GameObject goldMiner;
    [SerializeField] private AddGold _addGold;

    public static int TurretBasicCost = 150;

    private bool GhostTurretHasBeenPlaced = false;

    private void Awake()
    {
        _addGold = GetComponent<AddGold>();
    }

    void Update()
    {
        /*
        if (!IsOwner)
        {
            return;
        }
        */

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!GhostTurretHasBeenPlaced)
            {
                if (PlayerGold.CURRENT_PLAYER_GOLD >= TurretBasicCost)
                {
                    SpawnGhostTurret();
                }
                else
                {
                    print("Not enought money");
                }
            }
            else
            {
                BindingOfIsaacShooting.disableShooting = false;
                SpawnBasicTurret();
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            PlaceGoldMiner();
        }
    }

    private void SpawnGhostTurret()
    {
        Instantiate(GhostPlacementTurret, transform.position, Quaternion.identity);
        GhostTurretHasBeenPlaced = true;
    }

    private void SpawnBasicTurret()
    {
        _addGold.MinusGoldToDisplay(TurretBasicCost);

        GameObject ghostTurret = GameObject.FindWithTag("GhostTurret");
        if (ghostTurret != null)
        {
            Instantiate(TurretBasic, ghostTurret.transform.position, Quaternion.identity);
            Destroy(ghostTurret);
        }

        GhostTurretHasBeenPlaced = false;
 
    }

    private void PlaceGoldMiner()
    {
        if (PlayerGold.CURRENT_PLAYER_GOLD >= 100)
        {
            Instantiate(goldMiner, transform.position, Quaternion.identity);
            _addGold.MinusGoldToDisplay(100);
        }
    }
}
