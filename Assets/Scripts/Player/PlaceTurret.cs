using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTurret : MonoBehaviour
{
    [SerializeField] private GameObject TurretBasic;
    [SerializeField] private GameObject TurretCool;
    
    [SerializeField] private AddGold _addGold;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerGold.CURRENT_PLAYER_GOLD >= 20)
            {
                Instantiate(TurretBasic, gameObject.transform.position, Quaternion.identity);
                _addGold.MinusGoldToDisplay(20);
            }
            else
            {
                print("cant buy turret");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
           //Instantiate(TurretCool, gameObject.transform.position, Quaternion.identity);
        }
    }

}
