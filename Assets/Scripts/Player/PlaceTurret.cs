using System;
using UnityEngine;
using Unity.Netcode;

public class PlaceObject : NetworkBehaviour
{
    [SerializeField] private GameObject TurretBasic;
    [SerializeField] private GameObject GhostPlacementTurret;
    [SerializeField] private GameObject goldMiner;

    public static int TurretBasicCost = 150;

    private bool GhostTurretHasBeenPlaced = false;

    //Gold
    public PlayerGoldScriptableObject playerGold;

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
            BindingOfIsaacShooting.disableShooting = false;
            SpawnBasicTurret();
        }
    }

    private void SpawnGhostTurret()
    {
        Instantiate(GhostPlacementTurret, transform.position, Quaternion.identity);
        GhostTurretHasBeenPlaced = true;
    }

    private void SpawnBasicTurret()
    {
        //_addGold.MinusGoldToDisplay(TurretBasicCost);

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
        /*
        if (PlayerGold.CURRENT_PLAYER_GOLD >= 100)
        {
            Instantiate(goldMiner, transform.position, Quaternion.identity);
            _addGold.MinusGoldToDisplay(100);
        }
        */
    }
}
