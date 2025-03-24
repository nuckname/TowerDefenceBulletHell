using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class UpgradeUiManager : MonoBehaviour
{
    public GameObject targetTurret;
    
    [SerializeField] private GameObject[] allUpgradeUis;
    
    [SerializeField] private TMP_Text[] allTextUis;

    private GameObject currentUpgradeUi;
    
    private int upgradeSwitchIndex = 0;

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

    [Header("Reroll")]
    public int currentRerollAmount = 30;
    public int defaultRerollAmount = 30;
    [SerializeField] private TMP_Text rerollText;
    
    [SerializeField] private ApplyUpgrade _applyUpgrade;

    //Gold
    private int upgradePrice;

    //Buy Upgrade Index
    private int buttonClicked = 0;

    private void Awake()
    {
        _upgradeGold = GetComponent<UpgradeGold>();
    }

    private void Start()
    {
        print("Start true");
        DisableActionsWhileOpen(true);

        if (selectDescription == null)
        {
            Debug.LogError("selectDescription is null");
        }

        if (generateRarity == null)
        {
            Debug.LogError("generateRarity is null");
        }

    }
    
    private void GenerateDecription(StoreTurretDescription storeTurretDescription, TurretStats turretStats, UpgradeDataOnTurret upgradeDataOnTurret)
    {
        selectedRarity = generateRarity.SelectRarity(selectedRarity, turretStats);
            
        //Needed as accessing selectedRarity out of the scope of this script was causing errors. 
        _applyUpgrade.raritySelected = selectedRarity;
        storeTurretDescription.storedTurretSelectedRarity = selectedRarity;
            
        //Pick Upgrades
        storeTurretDescription.storedTurretDescription = selectDescription.Get3Descriptions(selectedRarity, upgradeDataOnTurret);

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
            GenerateDecription(storeTurretDescription, _targetTurret.GetComponent<TurretStats>(), _targetTurret.GetComponent<UpgradeDataOnTurret>());
            upgradePrice = _upgradeGold.DisplayGold(storeTurretDescription.storedTurretSelectedRarity, _targetTurret.GetComponent<TurretStats>().totalAmountOfUpgrades);
            storeTurretDescription.storeTurretPrice = upgradePrice;
        }


        if (isDescriptionAlreadyGenerated)
        {
            //Update: not sure if I need this.
            //Fixes another bug: when user presses Q and then E and selects upgrade displayedThreeUpgrades was empty. 
            displayedThreeUpgrades = storeTurretDescription.storedTurretDescription;
            
            _upgradeGold.HardCodedUpdateGoldAmount(storeTurretDescription.storeTurretPrice);
            
            //Skip the generation step as we dont want to generate them again.
            SetTextToUi(storeTurretDescription.storedTurretDescription);
        }

        //Set Display Gold Text and return amount
        print("Number of upgrades on the turret: " + _targetTurret.GetComponent<TurretStats>().totalAmountOfUpgrades);
        
    }
    
    private void SetTextToUi(string[] Text)
    {
        for (int i = 0; i < Text.Length; i++)
        {
            allTextUis[i].text = Text[i].ToString();
        }
    }

    public void ExitSelection()
    {
        BindingOfIsaacShooting.disableShooting = false;
        Destroy(gameObject);
        
        DisableActionsWhileOpen(false);
    }

    private int amountOfRerolls = 1;
    public void Reroll()
    {
        if (playerGold.SpendGold(currentRerollAmount))
        {
            switch (amountOfRerolls)
            {
                case 1:
                    currentRerollAmount = 20;
                    break;
                case 2:
                    currentRerollAmount = 10;
                    break;
                case >=3:
                    currentRerollAmount = 0;
                    break;
            }
            amountOfRerolls++;
            rerollText.text = "Reroll: $" + currentRerollAmount.ToString();
            
            if (targetTurret == null)
            {
                Debug.LogWarning("targetTurret is null");
            }
            StoreTurretDescription storeTurretDescription = targetTurret.GetComponent<StoreTurretDescription>();
            GenerateDecription(storeTurretDescription, targetTurret.GetComponent<TurretStats>(), targetTurret.GetComponent<UpgradeDataOnTurret>());
        
            upgradePrice = _upgradeGold.DisplayGold(storeTurretDescription.storedTurretSelectedRarity, targetTurret.GetComponent<TurretStats>().totalAmountOfUpgrades);
        }

    }

    public void ClickedToppedButton()
    {
        buttonClicked = 0;
    }
    
    public void ClickedMiddleButton()
    {
        buttonClicked = 1;
    }
    
    public void ClickedBottomButton()
    {
        buttonClicked = 2;
    }

    public void BuyUpgrade()
    {
        if (playerGold != null)
        {
            if (playerGold.SpendGold(upgradePrice))
            {
                DisableActionsWhileOpen(false);

                amountOfRerolls = 1;
                currentRerollAmount = defaultRerollAmount;
                rerollText.text = "Reroll: $" + currentRerollAmount.ToString();
                print("Set text to: " + currentRerollAmount);
                
                _applyUpgrade.ChosenUpgrade(displayedThreeUpgrades[buttonClicked], targetTurret);
            }
        }
    }

    private void DisableActionsWhileOpen(bool isOpen)
    {
        if (isOpen)
        {
            Debug.LogWarning("cant, place turret or shoot");
            BindingOfIsaacShooting.disableShooting = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlaceObject>().allowTurretPlacement = false;
        }
        else
        {
            Debug.LogWarning("CAN, place turret or shoot");
            BindingOfIsaacShooting.disableShooting = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlaceObject>().allowTurretPlacement = true;
            
        }
    }
}
