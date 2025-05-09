using System;
using TMPro;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine.UI;

public class PlaceTurret : NetworkBehaviour
{
    [SerializeField] private GameObject TurretBasic;
    [SerializeField] private GameObject GhostPlacementTurret;
    [SerializeField] private GameObject goldMiner;
    
    [SerializeField] private int amountOfTurretsBrought = 0;
    
    public static int TurretBasicCost = 25;

    private bool GhostTurretHasBeenPlaced = false;
    private GameObject currentGhost;

    // Gold system
    public PlayerGoldScriptableObject playerGold;
    public bool allowTurretPlacement;

    [SerializeField] private GameObject FloatingTextNotEnoughGold;
    private void Start()
    {
        allowTurretPlacement = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && allowTurretPlacement)
        {
            BuyTurretGhost();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (GhostTurretHasBeenPlaced)
            {
                if (playerGold.currentGold <= TurretBasicCost)
                {
                    SpawnFloatingText();
                    return;
                }
                
                GhostBlockPathCollision ghostBlockPathCollision = GameObject.FindGameObjectWithTag("GhostTurret").GetComponent<GhostBlockPathCollision>();
                if (!ghostBlockPathCollision.canPlaceGhost)
                {
                    return;
                }
                
                if (playerGold.SpendGold(TurretBasicCost))
                {
                    SpawnBasicTurret();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GhostTurretHasBeenPlaced)
            {
                GameObject userGhostTurret = GameObject.FindGameObjectWithTag("GhostTurret");
                BindingOfIsaacShooting.disableShooting = false;
                Destroy(userGhostTurret);
                GhostTurretHasBeenPlaced = !GhostTurretHasBeenPlaced;
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            PlaceGoldMiner();
        }
    }

    private void SpawnFloatingText()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject floatingText = Instantiate(FloatingTextNotEnoughGold, canvas.transform);

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        floatingText.transform.position = Input.mousePosition;
    }

    public void BuyTurretGhost()
    {
        if (!GhostTurretHasBeenPlaced)
        {

            SpawnGhostTurret();
            
        }
    }

    private void SpawnGhostTurret()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        GhostTurretHasBeenPlaced = true;

        currentGhost = Instantiate(GhostPlacementTurret, mousePos, transform.rotation);

        SetNewTurretPrice(currentGhost);

    }
    private void SetNewTurretPrice(GameObject ghostTurret)
    {
        Transform transform = ghostTurret.transform.Find("Cost");
        TextMeshPro costText = transform.GetComponent<TextMeshPro>();

        costText.text = "$" + TurretBasicCost;
    }



    private void SpawnBasicTurret()
    {
        if (currentGhost.GetComponent<GhostBlockPathCollision>().canPlaceGhost)
        {
            BindingOfIsaacShooting.disableShooting = false;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Quaternion ghostTurretRotation = GameObject.FindGameObjectWithTag("GhostTurret").GetComponentInChildren<GhostTurretRotate>().savedRotation;

            GameObject _turretBasic = Instantiate(TurretBasic,  mousePos, ghostTurretRotation);
            
            //Gets the sprites rotation so I can rotate the Upgrades spirte for rare diagnoal upgrade
            Transform spriteTransform = currentGhost.transform.Find("TurretSprite_0");
            _turretBasic.GetComponent<StoreTurretDescriptionAndRarity>().storeTurretRotation = spriteTransform.rotation.eulerAngles.z;
            
            amountOfTurretsBrought++;
            
            switch (amountOfTurretsBrought)
            {
                case 1:
                    TurretBasicCost = 50;
                    break;
                case 2:
                    TurretBasicCost = 100;
                    break;
                case 3:
                    TurretBasicCost = 200;
                    break;
                case 4:
                    TurretBasicCost = 250;
                    break;
                case 5:
                    TurretBasicCost = 275;
                    break;
                default:
                    TurretBasicCost = 300;
                    break;
            }
            

            
            GhostTurretHasBeenPlaced = false;
            Destroy(currentGhost);
        }
        else
        {
            Debug.Log("Ghost on path - cannot place turret.");
        }
    }

    private void GetTurretSpireRotation(GameObject curentGhost)
    {

    }

    private void PlaceGoldMiner()
    {
        // Gold miner placement logic (commented out in original)
    }
}