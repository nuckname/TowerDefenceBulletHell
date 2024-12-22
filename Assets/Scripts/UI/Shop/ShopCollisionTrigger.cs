using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShopCollisionTrigger : MonoBehaviour
{
    public GameObject UpgradeUI;
    
    private UpgradesSelectionScreen _upgradesSelectionScreen;
    private GenerateShopText _generateShopText;

    private void Awake()
    {
        _upgradesSelectionScreen = GetComponent<UpgradesSelectionScreen>();
        _generateShopText = GetComponent<GenerateShopText>();

        UpgradeUI = GameObject.FindWithTag("UpgradeUI");
    }

    private void Start()
    {
        //UpgradeUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //if not null round.
            //reset should be in state manager. 
            _generateShopText.GenerateUpgrades();

            _upgradesSelectionScreen.UpgradeUIStatus = true;
            UpgradeUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _upgradesSelectionScreen.UpgradeUIStatus = false;
            UpgradeUI.SetActive(false);
        }
    }

    // Update is called once per frame

}
