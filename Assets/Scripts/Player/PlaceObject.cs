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
    private GameObject curentGhost;

    // Gold system
    public PlayerGoldScriptableObject playerGold;
    public bool allowTurretPlacement;

    private void Start()
    {
        print("placeobject true");
        allowTurretPlacement = true;
    }

    void Update()
    {
        print(allowTurretPlacement);
        if (Input.GetKeyDown(KeyCode.Space) && allowTurretPlacement)
        {
            BuyTurretGhost();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (GhostTurretHasBeenPlaced)
            {
                SpawnBasicTurret(curentGhost);
            }
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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        GhostTurretHasBeenPlaced = true;
        return curentGhost = Instantiate(GhostPlacementTurret, mousePos, transform.rotation);
    }

    private void SpawnBasicTurret(GameObject currentGhost)
    {
        if (currentGhost.GetComponent<GhostBlockPathCollision>().canPlaceGhost)
        {
            BindingOfIsaacShooting.disableShooting = false;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            // Spawn the turret at the ghost's position with the ghost's rotation
            Instantiate(TurretBasic, mousePos, currentGhost.transform.rotation);
            
            GhostTurretHasBeenPlaced = false;
            Destroy(currentGhost);
        }
        else
        {
            Debug.Log("Ghost on path - cannot place turret.");
        }
    }

    private void PlaceGoldMiner()
    {
        // Gold miner placement logic (commented out in original)
    }
}