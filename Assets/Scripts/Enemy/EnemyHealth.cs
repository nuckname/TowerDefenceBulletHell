using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public HealthColourConfigScriptableObject HealthColourConfigScriptableObject;
    
    [SerializeField] private EnemyDie _enemyDie;
    [SerializeField] private EnemyCollision _enemyCollision;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public int EnemyStartingHealth;

    public Dictionary<int, Sprite> spriteDictionary;
    
    private bool isDead = false;

    void Awake()
    {
        // Initialize the sprite dictionary
        spriteDictionary = new Dictionary<int, Sprite>();

        // Convert HealthColourConfigScriptableObject to sprite dictionary
        if (HealthColourConfigScriptableObject != null)
        {
            foreach (var pair in HealthColourConfigScriptableObject.healthColorPairs)
            {
                spriteDictionary[pair.healthThreshold] = pair.sprite;
            }
        }

        // Set initial sprite
        if (spriteDictionary.TryGetValue(EnemyStartingHealth, out Sprite initialSprite))
        {
            spriteRenderer.sprite = initialSprite;
        }
    }

    public void InitializeEnemy(int enemyHp)
    {
        EnemyStartingHealth = enemyHp;

        if (spriteDictionary.TryGetValue(enemyHp, out Sprite sprite))
        {
            spriteRenderer.sprite = sprite;
        }
    }

    public void EnemyHit()
    {
        if (isDead) return;

        //Different sound of dying and getting hit?????????
        
        AudioManager.instance.enemyHitSFX();
        
        EnemyStartingHealth--;

        if (EnemyStartingHealth <= 0)
        {
            
            
            isDead = true;
            _enemyDie.EnemyHasDied();
        }

        if (spriteDictionary.TryGetValue(EnemyStartingHealth, out Sprite newSprite))
        {
            spriteRenderer.sprite = newSprite;
        }
    }

    void Update()
    {
        // Optional update logic
    }
}