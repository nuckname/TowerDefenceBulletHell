using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class UpgradeUiSwap3Lane : MonoBehaviour
{
    [SerializeField] private GameObject[] allUpgradeUis;
    
    [SerializeField] private TMP_Text[] allTextUis;

    private GameObject currentUpgradeUi;
    
    [SerializeField] private int upgradeSwitchIndex = 0;

    private bool allowUiSwapping = false;

    private UpgradeRadius upgradeRadius;
    
    [SerializeField] private string[] displayedThreeUpgrades;

    [SerializeField] private GameObject targetTurret;

    private GenerateRarity generateRarity;
    
    private SelectDescription selectDescription;

    private string chosenUpgrade = "";

    //if the description has already been generated and the player runs away from the turret
    //and reaccesses it the turret should no generate new upgrades or roll rarity again.
    [SerializeField] private bool ifDescriptionAlreadyGenerated = false;
    [SerializeField] bool noUpgradeSelected = true;

    private string selectedRarity;


    private void Awake()
    {
        upgradeRadius = GameObject.FindGameObjectWithTag("UpgradeRange").GetComponent<UpgradeRadius>();

        generateRarity = GetComponent<GenerateRarity>();
        selectDescription = GetComponent<SelectDescription>();
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

        //Sets blank Text to 'Error'
        foreach (TMP_Text singleDescription in allTextUis)
        {
            singleDescription.text = "Error";
        }
    }

    public void SetDescriptionsForUpgrades(GameObject _targetTurret)
    {
        StoreTurretDescription storeTurretDescription = _targetTurret.GetComponent<StoreTurretDescription>();
        
        bool isDescriptionAlreadyGenerated = storeTurretDescription.CheckTurretDescription();
        
        if (!isDescriptionAlreadyGenerated)
        {
            //Generate rarity 
            selectedRarity = generateRarity.SelectRarity();
            
            //Pick Upgrades
            storeTurretDescription.storedTurretDescription = selectDescription.Get3Descriptions(selectedRarity);
             
            //Display Text
            SetTextToUi(storeTurretDescription.storedTurretDescription);
        }

        if (isDescriptionAlreadyGenerated)
        {
            //Skip the generation step as we dont want to generate them again.
            //displayedThreeUpgrades = storeTurretDescription.storedTurretDescription;
            SetTextToUi(storeTurretDescription.storedTurretDescription);
        }
    }

    private void SetTextToUi(string[] Text)
    {
        for (int i = 0; i < Text.Length; i++)
        {
            allTextUis[i].text = Text[i].ToString();
            print(" set text to " + allTextUis[i].text);
        }
    }

    private void Update()
    {
        if (allowUiSwapping)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                SwitchSelection(1); 
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                SwitchSelection(-1);
            }
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ExitSelection();
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                SelectUpgrade();
            }
        }
        
        //Select with E
        
        //Q cancels everything
    }

    private void SelectUpgrade()
    {
        //Need to rework.
        //cant compare everything
        //use scriptable object or something?
        
        if (upgradeSwitchIndex == 0)
        {
            if (displayedThreeUpgrades != null)
            {
                print("Upgrade: " + displayedThreeUpgrades[0]);
                chosenUpgrade = displayedThreeUpgrades[0];

                if (displayedThreeUpgrades[0] == "Fires an additional projectile")
                {
                    //GetComponent<Additional Projectile>()
                }

                if(chosenUpgrade == "")
                //scripts based off rarity?
                //each upgrade has its own script???
                
                targetTurret.GetComponent<BasicTurretUpgrades>().GetTurretInfomation(displayedThreeUpgrades[0]);
                Destroy(gameObject);
                
                
                //Get what turret
                //targetTurret.ApplyUpgrade
            }
            else
            {
                print("0 is null");
            }
        }
                
        else if (upgradeSwitchIndex == 1)
        {
            if (displayedThreeUpgrades != null)
            {
                print("Upgrade: " + displayedThreeUpgrades[1]);
            }
            else
            {
                print("1 is null");
            }
        }
                
        else if (upgradeSwitchIndex == 2)
        {
            if (displayedThreeUpgrades != null)
            {
                print("Upgrade: " + displayedThreeUpgrades[2]);
            }
            else
            {
                print("2 is null");
            }
        }
    }

    private void GetUpgradeScript(string selectedUpgrade)
    {
        
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

        upgradeRadius.UpgradeRadiusOn = true;
        upgradeRadius.allowTurretSwapping = true;
            
        Destroy(gameObject);
    }

    private void TurnOffAllUi(bool showUi)
    {
        if (!showUi)
        {
            for (int i = 0; i >= allUpgradeUis.Length; i++)
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
