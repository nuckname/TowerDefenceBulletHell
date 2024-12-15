using System;
using Unity.VisualScripting;
using UnityEngine;

public class GoldOreCollision : MonoBehaviour
{
    [SerializeField] private GoldOreHealth _goldOreHealth; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _goldOreHealth.goldMinerHealth -= 10;
        }
    }
}
