using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTurret : MonoBehaviour
{
    [SerializeField] private GameObject TurretBasic;
    [SerializeField] private GameObject TurretCool;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Instantiate(TurretBasic, gameObject.transform.position, Quaternion.identity);
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(TurretCool, gameObject.transform.position, Quaternion.identity);
        }
    }

}
