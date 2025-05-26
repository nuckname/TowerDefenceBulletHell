using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerHealth", menuName = "Game/Player Health")]
public class PlayerHealthScriptabeObject : ScriptableObject
{
    public int maxHealth;
    public int startingHealth;
    public int currentHealth;
    
    public GameObject floatingTextPrefab;

    [SerializeField] private PlayerHealth playerHealth;

    private void OnEnable()
    {
        maxHealth = 10;
        currentHealth = startingHealth;
    }

    public void ResetHealth()
    { 
        currentHealth = maxHealth;
    }

    //How does this work for multiplayer
    //There's another Takedamge in PlayerCollision
    public void TakeDamage(int amount)
    {
        AudioManager.instance.PlayerHurtSFX();

        
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //:(
            playerHealth = GameObject.FindGameObjectWithTag("PlayerCollision").GetComponent<PlayerHealth>();
            
            playerHealth.PlayerHasDied();
            
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
