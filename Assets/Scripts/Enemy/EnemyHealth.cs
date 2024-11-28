using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyDie _enemyDie;
    [SerializeField] private EnemyCollision _enemyCollision;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private AddGold _addGold;
    
    public int EnemyStartingHealth;
    
    public Dictionary<int, Color> colorDictionary;

    void Start()
    {
        colorDictionary = new Dictionary<int, Color>
        {
            { 1, Color.red },
            { 2, Color.green },
            { 3, Color.blue },
            { 4, Color.yellow },
            { 5, Color.black }
        };
        
        //Sets inital colour
        if (colorDictionary.TryGetValue(EnemyStartingHealth, out Color initialColor))
        {
            spriteRenderer.color = initialColor;
        }
    }
    
    public void EnemyHit()
    {
        EnemyStartingHealth--;
        
        _addGold.AddGoldToDisplay(5);
        
        if (EnemyStartingHealth <= 0)
        {
            _enemyDie.EnemyHasDied(); 
        }
        
        if (colorDictionary.TryGetValue(EnemyStartingHealth, out Color newColor))
        {
            spriteRenderer.color = newColor;
        }
    }
        
    void Update()
    {

    }
}
