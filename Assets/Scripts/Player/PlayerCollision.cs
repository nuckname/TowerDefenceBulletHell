using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerHealthScriptabeObject playerHealthScriptabeObject;
    [SerializeField] private PlayerGoldScriptableObject playerGoldScriptableObject;

    [SerializeField] private int CoinGiveGoldAmount = 5;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            playerHealthScriptabeObject.TakeDamage(1);
            Destroy(other);
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            playerGoldScriptableObject.AddGold(CoinGiveGoldAmount);
        }
    }


}
