using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

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
    
    [Header("Cant buy upgrade")]
    [SerializeField] private Color insufficientColor = Color.red;
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeMagnitude = 5f;
    
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
        setIconUpgrades.SetIcons(storeTurretDescriptionAndRarity.storedTurretDescription, storeTurretDescriptionAndRarity.storedTurretSelectedRarity, storeTurretDescriptionAndRarity.storeTurretRotation);
        
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
            setIconUpgrades.SetIcons(storeTurretDescriptionAndRarity.storedTurretDescription, storeTurretDescriptionAndRarity.storedTurretSelectedRarity, storeTurretDescriptionAndRarity.storeTurretRotation);

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
        OnClickEffect.UiOpenCantUpgradeTurret = false;
        
        AudioManager.instance.backSFX();
        
        PlayerShooting.disableShooting = false;
        Destroy(gameObject);
        
        DisableActionsWhileOpen(false);
    }

    private int amountOfRerolls = 1;
    public void Reroll()
    {
        StoreTurretDescriptionAndRarity storeTurretDescriptionAndRarity = targetTurret.GetComponent<StoreTurretDescriptionAndRarity>();

        string oldRarity = storeTurretDescriptionAndRarity.storedTurretSelectedRarity;
        
        if (PlayerGold.Instance.SpendGold(storeTurretDescriptionAndRarity.storeTurretRerollPrice))
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
            
            AudioManager.instance.RerollTurretSFX();

        }
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reroll();
            }
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ExitSelection();
            }
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitSelection();
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ClickedToppedButton();
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ClickedMiddleButton();
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ClickedBottomButton();
            }
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
        StoreTurretDescriptionAndRarity storeTurretDescriptionAndRarity = targetTurret.GetComponent<StoreTurretDescriptionAndRarity>();
        
        int newUpgradePrice = storeTurretDescriptionAndRarity.storeTurretPrice;
        
        if (PlayerGold.Instance.SpendGold(newUpgradePrice))
        {
            if (newUpgradePrice == 0)
            {
                Debug.LogWarning("Spent 0");
            }

            print("Player spent: " + newUpgradePrice);
            DisableActionsWhileOpen(false);

            amountOfRerolls = 1;
            currentRerollAmount = defaultRerollAmount;
            rerollText.text = "Reroll: $" + storeTurretDescriptionAndRarity.storeTurretRerollPrice;
            
            
            _applyUpgrade.ChosenUpgrade(displayedThreeUpgrades[buttonClicked], targetTurret);
            
            AudioManager.instance.BuyTurretUpgradeSFX();
        }
        else
        {
            //not enough gold
            StopAllCoroutines();
            TMP_Text textToShake = GetTextButtonClicked(buttonClicked);
            StartCoroutine(ShowCannotBuyFeedback(textToShake));
            
            AudioManager.instance.GibberishSFX();

        }
      
    }

    private TMP_Text GetTextButtonClicked(int clickNumber)
    {
        switch (clickNumber)
        {
            case 0:
                return allTextUis[0];
            case 1:
                return allTextUis[1];
            case 2:
                return allTextUis[2];
            default:
                Debug.LogWarning($"GetTextButtonClicked: unknown clickNumber {clickNumber}");
                return null;
        }

    }

    private IEnumerator ShowCannotBuyFeedback(TMP_Text priceText)
    {
        // 1) turn text red
        var originalColor = priceText.color;
        priceText.color = insufficientColor;

        // 2) shake
        var rt = priceText.rectTransform;
        Vector3 originalPos = rt.localPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = (Random.value * 2f - 1f) * shakeMagnitude;
            rt.localPosition = originalPos + new Vector3(offsetX, 0f, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 3) restore
        rt.localPosition = originalPos;
        priceText.color = originalColor;
    }
    
    private void DisableActionsWhileOpen(bool isOpen)
    {
        if (isOpen)
        {
            Debug.LogWarning("cant, place turret or shoot");
            PlayerShooting.disableShooting = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlaceTurret>().allowTurretPlacement = false;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<OnClickEffect>().UiOpenCantUpgradeTurret = true;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<SelectTurret>().AllowSelectingTurret = false;
        }
        else
        {
            Debug.LogWarning("CAN, place turret or shoot");
            PlayerShooting.disableShooting = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlaceTurret>().allowTurretPlacement = true;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<OnClickEffect>().UiOpenCantUpgradeTurret = false;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<SelectTurret>().AllowSelectingTurret = true;


            
        }
    }
}
