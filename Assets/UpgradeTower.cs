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
    
    private int upgradeIndex = 1;
    
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
