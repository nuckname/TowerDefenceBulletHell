using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public HealthColourConfigScriptableObject HealthColourConfigScriptableObject;
    
    [SerializeField] private EnemyDie _enemyDie;
    [SerializeField] private EnemyCollision _enemyCollision;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public int EnemyStartingHealth;
    
    public Dictionary<int, Color> colorDictionary;

    void Awake()
    {
        // Initialize the color dictionary
        colorDictionary = new Dictionary<int, Color>();

        //Convert HealthColourConfigScriptableObject to dictionary
        if (HealthColourConfigScriptableObject != null)
        {
            foreach (var pair in HealthColourConfigScriptableObject.healthColorPairs)
            {
                colorDictionary[pair.healthThreshold] = pair.color;
            }
        }

        // Set initial color
        if (colorDictionary.TryGetValue(EnemyStartingHealth, out Color initialColor))
        {
            spriteRenderer.color = initialColor;
        }
        else
        {
            // Use default color if no matching threshold is found
            spriteRenderer.color = HealthColourConfigScriptableObject.defaultColor;
        }
    }
    
    public void InitializeEnemy(int enemyHp)
    {
        EnemyStartingHealth = enemyHp;

        // Set the initial color based on the enemyHp value
        if (colorDictionary.TryGetValue(enemyHp, out Color initialColor))
        {
            spriteRenderer.color = initialColor;
        }
        else
        {
            // Use default color if no matching threshold is found
            spriteRenderer.color = HealthColourConfigScriptableObject.defaultColor;
        }
    }

    
    public void EnemyHit()
    {
        EnemyStartingHealth--;
        
        if (EnemyStartingHealth <= 0)
        {
            _enemyDie.EnemyHasDied(); 
        }
        
        if (colorDictionary.TryGetValue(EnemyStartingHealth, out Color newColor))
        {
            spriteRenderer.color = newColor;
        }
        else
        {
            // Use default color if no matching threshold is found
            spriteRenderer.color = HealthColourConfigScriptableObject.defaultColor;
        }
    }
        
    void Update()
    {
        // Update logic if needed
    }
}