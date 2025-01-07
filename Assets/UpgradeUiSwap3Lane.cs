using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UpgradeUiSwap3Lane : MonoBehaviour
{
    [SerializeField] private GameObject[] allUpgradeUis;

    private GameObject currentUpgradeUi;
    
    [SerializeField] private int upgradeSwitchIndex = 0;

    private bool allowUiSwapping = false;

    private UpgradeRadius upgradeRadius;

    private void Awake()
    {
        upgradeRadius = GameObject.FindGameObjectWithTag("UpgradeRange").GetComponent<UpgradeRadius>();
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
        }
        
        //Select with E
        
        //Q cancels everything
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
            allowUiSwapping = false;

            TurnOffAllUi(false);

            upgradeRadius.UpgradeRadiusOn = true;
            upgradeRadius.allowTurretSwapping = true;
            
            Destroy(gameObject);
            
            
        }
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
                }
                else
                {
                    allUpgradeUis[i].gameObject.SetActive(false);
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
