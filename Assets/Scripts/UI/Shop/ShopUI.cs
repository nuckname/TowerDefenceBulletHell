using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject UpgradeUI;

    private bool UpgradeUIStatus;

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
            
            UpgradeUIStatus = true;
            UpgradeUI.SetActive(UpgradeUIStatus);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UpgradeUIStatus = false;
            UpgradeUI.SetActive(UpgradeUIStatus);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UpgradeUIStatus)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                print("Select Upgrade 1");
            }
            
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                print("Select Upgrade 2");
            } 
            
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                print("Select Upgrade 3");
            } 
        }
    }
}
