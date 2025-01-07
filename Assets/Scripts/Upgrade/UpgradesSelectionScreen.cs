using System;
using UnityEngine;

public class UpgradesSelectionScreen : MonoBehaviour
{
    [SerializeField] private GameObject UiTurretBox;
    
    public bool UpgradeUIStatus = false;
    private ShopCollisionTrigger _shopCollisionTrigger;
    
    private GenerateShopText _generateShopText;
    private UpgradeManager _upgradeManager;
    
    //This upgrade system is sort of dumb. A lot of scripts maybe over copmlicated. 
    
    private void Awake()
    {
        _upgradeManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeManager>();
        _generateShopText = GetComponent<GenerateShopText>();
        _shopCollisionTrigger = GetComponent<ShopCollisionTrigger>();
    }

    void Update()
    {
        if (UpgradeUIStatus)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                print("Select Upgrade 1");
                GiveUpgrade(_generateShopText.upgrade1Selected);
                
                //use state machine. cant access sh0ip again.
                // maybe you get like 5 upgrades before it turns off?
                _shopCollisionTrigger.UpgradeUI.SetActive(false);
            }
            
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                print("Select Upgrade 2");
                GiveUpgrade(_generateShopText.upgrade2Selected);
                _shopCollisionTrigger.UpgradeUI.SetActive(false);

            } 
            
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                print("Select Upgrade 3");
                GiveUpgrade(_generateShopText.upgrade3Selected);
                _shopCollisionTrigger.UpgradeUI.SetActive(false);

            } 
        }
    }

    public void ShowUpgradeUI(bool showUi)
    {
        if (showUi)
        {
            //instantiate a Turret manager.
            
            Instantiate(UiTurretBox, gameObject.transform.position, Quaternion.identity);
        }
    }

    void GiveUpgrade(string upgradeSelected)
    {
        foreach (string upgrade in _generateShopText.upgrades)
        {
            if (upgrade == "BulletLife")
            {
                _upgradeManager.BulletLifeUpgrade();
            }
        }
    }
    
}
