using System;
using UnityEngine;

public class UpgradeSelected : MonoBehaviour
{
    public bool UpgradeUIStatus;
    [SerializeField] private ShopUI _shopUI;
    [SerializeField] private GenerateShopText _generateShopText;
    [SerializeField] private UpgradeManager _upgradeManager;
    
    //This upgrade system is sort of dumb. A lot of scripts maybe over copmlicated. 
    
    private void Awake()
    {
        _upgradeManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeManager>();
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
                _shopUI.UpgradeUI.SetActive(false);
            }
            
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                print("Select Upgrade 2");
                GiveUpgrade(_generateShopText.upgrade2Selected);
                _shopUI.UpgradeUI.SetActive(false);

            } 
            
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                print("Select Upgrade 3");
                GiveUpgrade(_generateShopText.upgrade3Selected);
                _shopUI.UpgradeUI.SetActive(false);

            } 
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
