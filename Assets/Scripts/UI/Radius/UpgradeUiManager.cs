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

    private ChangeUiColourBackGround changeUiColourBackGround;
    private UpgradeGold _upgradeGold;

    private SetIconUpgrades setIconUpgrades;
    
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
        setIconUpgrades = GetComponent<SetIconUpgrades>();
        _upgradeGold = GetComponent<UpgradeGold>();
        changeUiColourBackGround = GetComponent<ChangeUiColourBackGround>();
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
    
    private void GenerateDecription(StoreTurretDescriptionAndRarity storeTurretDescriptionAndRarity, TurretStats turretStats, UpgradeDataOnTurret upgradeDataOnTurret, string oldRarity)
    {
        selectedRarity = generateRarity.SelectRarity(selectedRarity, turretStats);

        while (selectedRarity == oldRarity)
        {
            selectedRarity = generateRarity.SelectRarity(selectedRarity, turretStats);
        }
    
        //Needed as accessing selectedRarity out of the scope of this script was causing errors. 
        _applyUpgrade.raritySelected = selectedRarity;
        storeTurretDescriptionAndRarity.storedTurretSelectedRarity = selectedRarity;

        //Pick Upgrades
        storeTurretDescriptionAndRarity.storedTurretDescription = selectDescription.Get3Descriptions(selectedRarity, upgradeDataOnTurret);

        //Picks Icons. Two calls of this.
        setIconUpgrades.SetIcons(storeTurretDescriptionAndRarity.storedTurretDescription, storeTurretDescriptionAndRarity.storedTurretSelectedRarity);
        
        //Puts it in global variable
        displayedThreeUpgrades = storeTurretDescriptionAndRarity.storedTurretDescription;
        
        //Display Text
        SetTextToUi(storeTurretDescriptionAndRarity.storedTurretDescription);
        
        
    }
    
    public void SetDescriptionsForUpgrades(GameObject _targetTurret)
    {
        StoreTurretDescriptionAndRarity storeTurretDescriptionAndRarity = _targetTurret.GetComponent<StoreTurretDescriptionAndRarity>();

        bool isDescriptionAlreadyGenerated = storeTurretDescriptionAndRarity.CheckTurretDescription();

        if (!isDescriptionAlreadyGenerated)
        {
            GenerateDecription(storeTurretDescriptionAndRarity, _targetTurret.GetComponent<TurretStats>(), _targetTurret.GetComponent<UpgradeDataOnTurret>(), null);
            upgradePrice = _upgradeGold.DisplayGold(storeTurretDescriptionAndRarity.storedTurretSelectedRarity, _targetTurret.GetComponent<TurretStats>().totalAmountOfUpgrades);
            
            print("New Description: " + upgradePrice);

            storeTurretDescriptionAndRarity.storeTurretPrice = upgradePrice;
        }


        if (isDescriptionAlreadyGenerated)
        {
            //Update: not sure if I need this.
            //Fixes another bug: when user presses Q and then E and selects upgrade displayedThreeUpgrades was empty. 
            displayedThreeUpgrades = storeTurretDescriptionAndRarity.storedTurretDescription;
            
            _upgradeGold.HardCodedUpdateGoldAmount(storeTurretDescriptionAndRarity.storeTurretPrice);
            
            //Selects icons two calls.
            setIconUpgrades.SetIcons(storeTurretDescriptionAndRarity.storedTurretDescription, storeTurretDescriptionAndRarity.storedTurretSelectedRarity);

            //Skip the generation step as we dont want to generate them again.
            SetTextToUi(storeTurretDescriptionAndRarity.storedTurretDescription);
        }

        UpdateBackgroundColourUi(storeTurretDescriptionAndRarity);

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
        StoreTurretDescriptionAndRarity storeTurretDescriptionAndRarity = targetTurret.GetComponent<StoreTurretDescriptionAndRarity>();

        string oldRarity = storeTurretDescriptionAndRarity.storedTurretSelectedRarity;
        
        if (playerGold.SpendGold(storeTurretDescriptionAndRarity.storeTurretRerollPrice))
        {
            /*
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
            */
            
            //Store Rereoll price.
            storeTurretDescriptionAndRarity.storeTurretRerollPrice = currentRerollAmount;
            
            rerollText.text = "Reroll: $" + storeTurretDescriptionAndRarity.storeTurretRerollPrice;
            
            
            if (targetTurret == null)
            {
                Debug.LogWarning("targetTurret is null");
            }
            
            GenerateDecription(storeTurretDescriptionAndRarity, targetTurret.GetComponent<TurretStats>(), targetTurret.GetComponent<UpgradeDataOnTurret>(), oldRarity);
        
            upgradePrice = _upgradeGold.DisplayGold(storeTurretDescriptionAndRarity.storedTurretSelectedRarity, targetTurret.GetComponent<TurretStats>().totalAmountOfUpgrades);
            
            storeTurretDescriptionAndRarity.storeTurretPrice = upgradePrice;
            
            UpdateBackgroundColourUi(storeTurretDescriptionAndRarity);
            

        }
    }

    public void UpdateBackgroundColourUi(StoreTurretDescriptionAndRarity storeTurretDescriptionAndRarity)
    {
        changeUiColourBackGround.UpdateUiBackground(storeTurretDescriptionAndRarity.storedTurretSelectedRarity);

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
            StoreTurretDescriptionAndRarity storeTurretDescriptionAndRarity = targetTurret.GetComponent<StoreTurretDescriptionAndRarity>();
            
            int newUpgradePrice = storeTurretDescriptionAndRarity.storeTurretPrice;
            
            if (playerGold.SpendGold(newUpgradePrice))
            {
                if (newUpgradePrice == 0)
                {
                    Debug.LogError("Spent 0");
                }

                print("Player spent: " + newUpgradePrice);
                DisableActionsWhileOpen(false);

                amountOfRerolls = 1;
                currentRerollAmount = defaultRerollAmount;
                rerollText.text = "Reroll: $" + storeTurretDescriptionAndRarity.storeTurretRerollPrice;
                
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
            //GameObject.FindGameObjectWithTag("Player").GetComponent<OnClickEffect>().UiOpenCantUpgradeTurret = true;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<SelectTurret>().AllowSelectingTurret = false;
        }
        else
        {
            Debug.LogWarning("CAN, place turret or shoot");
            BindingOfIsaacShooting.disableShooting = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlaceObject>().allowTurretPlacement = true;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<OnClickEffect>().UiOpenCantUpgradeTurret = false;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<SelectTurret>().AllowSelectingTurret = true;


            
        }
    }
}
