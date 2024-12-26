using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeTower : MonoBehaviour
{
    //UpgradeTowerBasic
    //UpgradeTowerDartMonkey

    [SerializeField] private GameObject _spriteUpgradePath1;
    [SerializeField] private GameObject _spriteUpgradePath3;
    [SerializeField] private GameObject _spriteBlackTextBox;
    
    //? Update all UpgradePath Sprites to have a yellow arrow?
    //'j' or 'l' simply cycles thought them
    [SerializeField] private GameObject[] spriteDebugs;

    public bool allowSwappingBetweenUpgrade = false;
    private int spriteIndex = 1;

    public bool pickUpgrade = false;
    
    
    
    private int upgradeIndex = 1;

    private void DisplayUiTest()
    { 
        //for debugging
        
        
    }
    
    public void SelectedUpgrade()
    {
        print("Sleected upgrade called");
        if (upgradeIndex == 1)
        {
            //single upgrade
            Instantiate(_spriteUpgradePath1, gameObject.transform.position, Quaternion.identity);

            //places transparent box around the upgrade.
            //can select;
            //still need to spawn text;
            
            //spawn one upgrade sprite
            //Give Text info

            //cost

            upgradeIndex++;
        }
        
        if (upgradeIndex == 2)
        {
            //single upgrade

            
            
        }
        
        if (upgradeIndex == 3)
        {
            //double upgrade
            
        }
        
        if (upgradeIndex == 4)
        {
            
        }
        
        if (upgradeIndex == 5)
        {
            
        }
        
    }

    private void Update()
    {
        if (allowSwappingBetweenUpgrade)
        {
            //starts left
            if (Input.GetKey(KeyCode.J))
            {
                spriteIndex--;

                if (spriteIndex == 1)
                {
                    spriteIndex = 3;
                }

                Instantiate(spriteDebugs[spriteIndex], gameObject.transform.position, Quaternion.identity);
            }

            if (Input.GetKey(KeyCode.L))
            {
                spriteIndex++;
                
                if (spriteIndex == 3)
                {
                    spriteIndex = 1;
                }
                
                Instantiate(spriteDebugs[spriteIndex], gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
