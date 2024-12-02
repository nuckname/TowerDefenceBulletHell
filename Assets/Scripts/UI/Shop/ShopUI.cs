using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject UpgradeUI;
    [SerializeField] private UpgradeSelected _upgradeSelected;
    
    [SerializeField] private GenerateShopText _generateShopText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpgradeUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //if not null round.
            //reset should be in state manager. 
            _generateShopText.GenerateUpgrades();

            _upgradeSelected.UpgradeUIStatus = true;
            UpgradeUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _upgradeSelected.UpgradeUIStatus = false;
            UpgradeUI.SetActive(false);
        }
    }

    // Update is called once per frame

}
