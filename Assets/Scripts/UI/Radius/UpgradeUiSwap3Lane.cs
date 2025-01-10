using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class UpgradeUiSwap3Lane : MonoBehaviour
{
    [SerializeField] private GameObject[] allUpgradeUis;
    
    [SerializeField] private TMP_Text[] allTextUis;

    private GameObject currentUpgradeUi;
    
    [SerializeField] private int upgradeSwitchIndex = 0;

    private bool allowUiSwapping = false;

    private UpgradeRadius upgradeRadius;
    
    [SerializeField] private string[] selectedUpgrades;

    [SerializeField] private GameObject targetTurret;

    private void Awake()
    {
        upgradeRadius = GameObject.FindGameObjectWithTag("UpgradeRange").GetComponent<UpgradeRadius>();
    }
    
    public void SetDescriptionsForUpgrades(string[] selectedUpgrades, GameObject _targetTurret)   
    {
        
        this.selectedUpgrades = selectedUpgrades; // Assign the parameter to the global field
        this.targetTurret = _targetTurret;
        
        for (int i = 0; i < selectedUpgrades.Length; i++)
        {
            allTextUis[i].text = selectedUpgrades[i].ToString();
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
        print("selected upgrade");

        if (upgradeSwitchIndex == 0)
        {
            if (selectedUpgrades != null)
            {
                print(selectedUpgrades[0]);
                print(targetTurret);

                string tag = targetTurret.tag;

                //would need this for every single turret in the game. How to fix?
                if (tag == "Turret")
                {
                    targetTurret.GetComponent<BasicTurretUpgrades>().GetTurretInfomation(selectedUpgrades[0]);
                }
                
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
            if (selectedUpgrades != null)
            {
                print(selectedUpgrades[1]);
            }
            else
            {
                print("1 is null");
            }
        }
                
        else if (upgradeSwitchIndex == 2)
        {
            if (selectedUpgrades != null)
            {
                print(selectedUpgrades[2]);
            }
            else
            {
                print("2 is null");
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
