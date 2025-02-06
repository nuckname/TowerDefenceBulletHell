using System;
using UnityEngine;
using Unity.Netcode;

public class PlaceObject : NetworkBehaviour
{
    [SerializeField] private GameObject TurretBasic;
    [SerializeField] private GameObject GhostPlacementTurret;
    [SerializeField] private GameObject goldMiner;

    public static int TurretBasicCost = 25;

    private bool GhostTurretHasBeenPlaced = false;

    //[SerializeField] private GhostBlockPathCollision ghostBlockPathCollision;
    [SerializeField] private bool canPlaceGhost;
    //Gold
    public PlayerGoldScriptableObject playerGold;

    private GameObject curentGhost;

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
            BuyTurretGhost();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            PlaceGoldMiner();
        }
    }

    public void BuyTurretGhost()
    {
        if (!GhostTurretHasBeenPlaced)
        {
            if (playerGold.SpendGold(TurretBasicCost))
            {
                SpawnGhostTurret();
            }
        }
        else
        {
            SpawnBasicTurret(curentGhost);
        }
    }

    private GameObject SpawnGhostTurret()
    {
        GhostTurretHasBeenPlaced = true;
        return curentGhost = Instantiate(GhostPlacementTurret, transform.position, Quaternion.identity);
    }

    private void SpawnBasicTurret(GameObject currentGhost)
    {
        if (currentGhost.GetComponent<GhostBlockPathCollision>().canPlaceGhost)
        {
            BindingOfIsaacShooting.disableShooting = false;
            
            GameObject ghostTurret = GameObject.FindWithTag("GhostTurret");
            if (ghostTurret != null)
            {
                Instantiate(TurretBasic, ghostTurret.transform.position, Quaternion.identity);
                GhostTurretHasBeenPlaced = false;
                Destroy(ghostTurret);
            }

        }
        else
        {
            print("Ghost on path");
        }
    }

    private void PlaceGoldMiner()
    {
        /*
        if (PlayerGold.CURRENT_PLAYER_GOLD >= 100)
        {
            Instantiate(goldMiner, transform.position, Quaternion.identity);
            _addGold.MinusGoldToDisplay(100);
        }
        */
    }
}
