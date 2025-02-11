using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class UpgradeUiManager : MonoBehaviour
{
    public GameObject targetTurret;
    
    [SerializeField] private GameObject[] allUpgradeUis;
    
    [SerializeField] private TMP_Text[] allTextUis;

    private GameObject currentUpgradeUi;
    
    private int upgradeSwitchIndex = 0;

    private bool allowUiSwapping = false;

    private UpgradeRadius upgradeRadius;
    
    [SerializeField] private string[] displayedThreeUpgrades;

    
    [SerializeField] private GenerateRarity generateRarity;
    
    [SerializeField] private SelectDescription selectDescription;

    private UpgradeGold _upgradeGold;

    private string chosenUpgrade = "";

    //if the description has already been generated and the player runs away from the turret
    //and reaccesses it the turret should no generate new upgrades or roll rarity again.
    [SerializeField] private bool ifDescriptionAlreadyGenerated = false;
    
    //Not used?
    //[SerializeField] bool noUpgradeSelected = true;

    private string selectedRarity = "Error";
    private int rarityIndex = 0;
    
    //Gold
    public PlayerGoldScriptableObject playerGold;
    private int rerollGoldAmount = 50;

    [SerializeField] private ApplyUpgrade _applyUpgrade;

    //Gold
    private int upgradePrice;
    
    private void Awake()
    {
        upgradeRadius = GameObject.FindGameObjectWithTag("UpgradeRange").GetComponent<UpgradeRadius>();
        _upgradeGold = GetComponent<UpgradeGold>();
    }

    private void Start()
    {
        if (selectDescription == null)
        {
            Debug.LogError("selectDescription is null");
        }

        if (generateRarity == null)
        {
            Debug.LogError("generateRarity is null");
        }

    }
    
    private void GenerateDecription(StoreTurretDescription storeTurretDescription, TurretStats turretStats)
    {
        selectedRarity = generateRarity.SelectRarity(selectedRarity, turretStats);
            
        //Needed as accessing selectedRarity out of the scope of this script was causing errors. 
        _applyUpgrade.raritySelected = selectedRarity;
        storeTurretDescription.storedTurretSelectedRarity = selectedRarity;
            
        //Pick Upgrades
        storeTurretDescription.storedTurretDescription = selectDescription.Get3Descriptions(selectedRarity, turretStats.ReduceTurretBlankChance);

        //Puts it in global variable
        displayedThreeUpgrades = storeTurretDescription.storedTurretDescription;
            
        //Display Text
        SetTextToUi(storeTurretDescription.storedTurretDescription);
    }
    
    public void SetDescriptionsForUpgrades(GameObject _targetTurret)
    {
        StoreTurretDescription storeTurretDescription = _targetTurret.GetComponent<StoreTurretDescription>();

        bool isDescriptionAlreadyGenerated = storeTurretDescription.CheckTurretDescription();

        if (!isDescriptionAlreadyGenerated)
        {
            GenerateDecription(storeTurretDescription, _targetTurret.GetComponent<TurretStats>());
        }

        if (isDescriptionAlreadyGenerated)
        {
            //Fixes another bug: when user presses Q and then E and selects upgrade displayedThreeUpgrades was empty. 
            displayedThreeUpgrades = storeTurretDescription.storedTurretDescription;
            
            //Skip the generation step as we dont want to generate them again.
            SetTextToUi(storeTurretDescription.storedTurretDescription);
        }
        
        //Set Display Gold Text and return amount
        upgradePrice = _upgradeGold.DisplayGold(storeTurretDescription.storedTurretSelectedRarity);
    }
    
    private void SetTextToUi(string[] Text)
    {
        for (int i = 0; i < Text.Length; i++)
        {
            allTextUis[i].text = Text[i].ToString();
            print("Set text to " + allTextUis[i].text);
        }
    }

    private void Update()
    {
        if (allowUiSwapping)
        {
            //Swaps between upgrade screens
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SwitchSelection(1); 
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SwitchSelection(-1);
            }
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ExitSelection();
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                BuyUpgrade();
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReRoll();
            }
        }
    }

    private void ReRoll()
    {
        if (playerGold.SpendGold(50))
        {
            if (targetTurret == null)
            {
                Debug.LogWarning("deez");
            }
            StoreTurretDescription storeTurretDescription = targetTurret.GetComponent<StoreTurretDescription>();
            GenerateDecription(storeTurretDescription, targetTurret.GetComponent<TurretStats>());
            
            upgradePrice = _upgradeGold.DisplayGold(storeTurretDescription.storedTurretSelectedRarity);
        }
    }

    public void BuyUpgrade()
    {
        if (playerGold != null)
        {
            //Minus gold in if statement
            if (playerGold.SpendGold(upgradePrice))
            {
                _applyUpgrade.ChosenUpgrade(displayedThreeUpgrades[upgradeSwitchIndex], targetTurret);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("UpgradeRange"))
        {
            allowUiSwapping = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("UpgradeRange"))
        {
            ExitSelection();
        }
    }

    private void ExitSelection()
    {
        UpgradeRadius upgradeRadius = GameObject.FindGameObjectWithTag("UpgradeRange").GetComponent<UpgradeRadius>();
        if (upgradeRadius != null)
        {
            //Actually we need to get what has been selected. And use that as a highlight. New function.
            upgradeRadius.HighlightFurthestTurret();
        }
        else
        {
            Debug.Log("Error. UpgradeRadius null");
        }        
        
        allowUiSwapping = false;

        TurnOffAllUi(false);

        BindingOfIsaacShooting.disableShooting = false;
        upgradeRadius.UpgradeRadiusOn = true;
        upgradeRadius.allowTurretSwapping = true;
            
        Destroy(gameObject);
    }

    public void TurnOffAllUi(bool showUi)
    {
        if (!showUi)
        {
            //for (int i = 0; i >= allUpgradeUis.Length; i++)
            for (int i = 0; i < allUpgradeUis.Length; i++)
            {
                if (i == 0)
                {
                    //so we can initiate UIManager Correctly.
                    allUpgradeUis[0].gameObject.SetActive(true);
                    allTextUis[0].gameObject.SetActive(true);
                }
                else
                {
                    allUpgradeUis[i].gameObject.SetActive(false);
                    allTextUis[i].gameObject.SetActive(true);
                }
                
            }
        }
    }
    
    private void SwitchSelection(int direction)
    {
        if (allUpgradeUis.Length > 0)
        {
            if (currentUpgradeUi != null)
            {
                //currentUpgradeUi.SetActive(false);
            }

            allUpgradeUis[upgradeSwitchIndex].SetActive(false);
            
            upgradeSwitchIndex = (upgradeSwitchIndex + direction + allUpgradeUis.Length) % allUpgradeUis.Length;

            allUpgradeUis[upgradeSwitchIndex].SetActive(true);
            //currentUpgradeUi = Instantiate(allUpgradeUis[upgradeSwitchIndex], transform.position, Quaternion.identity);

        }
        else
        {
            Debug.Log("No Ui to swap");
        }
    }
}
