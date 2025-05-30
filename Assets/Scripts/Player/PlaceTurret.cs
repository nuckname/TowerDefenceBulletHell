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

    public bool GhostTurretHasBeenPlaced = false;
    private GameObject currentGhost;

    // Gold system
    public PlayerGoldScriptableObject playerGold;
    public bool allowTurretPlacement;

    [SerializeField] private GameObject FloatingTextNotEnoughGold;

    [SerializeField] private TextMeshProUGUI displayTurretText;

    public bool tutorialCannotPlaced = false;
    public bool tutorialCannotBuyGhostTurret = false;
    public bool tutorialCannotPressQ = false;
    
    private void Awake()
    {
        displayTurretText = GameObject.FindGameObjectWithTag("TurretDisplayText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        allowTurretPlacement = true;
        TurretBasicCost = 25;
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

                if (tutorialCannotPlaced)
                {
                    AudioManager.instance.errorSFX();


                    Debug.Log("Cant place turret becuase of tutorial.");
                    Debug.Log("or Cant press Q on turret becuase of tutorial.");
                    return;
                }
                
                if (playerGold.SpendGold(TurretBasicCost))
                {
                    AudioManager.instance.PlaceTurretClip();
                    
                    //audioSource.PlayOneShot(placeTurretClip);

                    SpawnBasicTurret();
                    UpdateTurretDisplayText(TurretBasicCost);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (tutorialCannotPressQ)
            {
                AudioManager.instance.errorSFX();
                Debug.Log("Cant press Q on turret becuase of tutorial.");

                return;
            }
            
            if (GhostTurretHasBeenPlaced)
            {
                AudioManager.instance.CancelGhostTurretSFX();

                //audioSource.PlayOneShot(cancelGhostTurret);
                
                GameObject userGhostTurret = GameObject.FindGameObjectWithTag("GhostTurret");
                PlayerShooting.disableShooting = false;
                Destroy(userGhostTurret);
                GhostTurretHasBeenPlaced = !GhostTurretHasBeenPlaced;
            }
        }
    }

    private void UpdateTurretDisplayText(float turretCost)
    {
        displayTurretText.text = $"Turret Cost ${turretCost}";
    }

    private void SpawnFloatingText()
    {
        AudioManager.instance.errorSFX();
        AudioManager.instance.GibberishSFX();
        
        GameObject canvas = GameObject.Find("Canvas");
        GameObject floatingText = Instantiate(FloatingTextNotEnoughGold, canvas.transform);

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        floatingText.transform.position = Input.mousePosition;
    }

    public void BuyTurretGhost()
    {
        if (!GhostTurretHasBeenPlaced)
        {
            AudioManager.instance.PlaceGhostTurretSFX();
            
            SpawnGhostTurret();
            
        }
    }

    private void SpawnGhostTurret()
    {
        if (tutorialCannotBuyGhostTurret)
        {
            AudioManager.instance.errorSFX();
            return;
        }
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        GhostTurretHasBeenPlaced = true;

        currentGhost = Instantiate(GhostPlacementTurret, mousePos, transform.rotation);

        Transform priceLabelTransform = currentGhost.transform.Find("Cost");
        if (priceLabelTransform != null)
        {
            TMP_Text costText = priceLabelTransform.GetComponent<TMP_Text>();
            
            if (playerGold.currentGold <= TurretBasicCost)
            {
                costText.color = Color.red;
            }
            else
            {
                costText.color = Color.white;
            }
        }
        
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
            PlayerShooting.disableShooting = false;

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
}