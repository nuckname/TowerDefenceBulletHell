using System;
using UnityEngine;

public class TurretCollision : MonoBehaviour
{
    private GameObject UpgradeUI;

    private void Awake()
    {
        UpgradeUI = GameObject.FindWithTag("UpgradeUI");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Trigger UI
        }
    }
}
