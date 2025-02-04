using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerHealth", menuName = "Game/Player Health")]
public class PlayerHealthScriptabeObject : ScriptableObject
{
    public int maxHealth;
    public int currentHealth;
    
    public GameObject floatingTextPrefab;

    [SerializeField] private PlayerHealth playerHealth;

    private void OnEnable()
    {
        maxHealth = 10;
        currentHealth = 10;
    }

    public void ResetHealth()
    { 
        currentHealth = maxHealth;
    }

    //How does this work for multiplayer
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //:(
            playerHealth = GameObject.FindGameObjectWithTag("PlayerCollision").GetComponent<PlayerHealth>();
            playerHealth.PlayerHasDied();
            currentHealth = 10;

            Debug.Log(name + " has died!");
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

}
