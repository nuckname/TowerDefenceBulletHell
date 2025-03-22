using UnityEngine;

public class PlayerHealthSpriteSheet : MonoBehaviour
{
    [SerializeField] private HealthColourConfigScriptableObject playerHealthSpriteSheet; 
    [SerializeField] private PlayerHealthScriptabeObject playerHealth; 
    [SerializeField] private SpriteRenderer playerSpriteRenderer;

    public void ChangePlayerSprite()
    {
        int hp = playerHealth.currentHealth;
        if (hp > 0 && hp <= playerHealthSpriteSheet.healthColorPairs.Count)
        {
            playerSpriteRenderer.sprite = playerHealthSpriteSheet.healthColorPairs[hp - 1].sprite;
        }
        else
        {
            Debug.LogWarning("Invalid health value passed to ChangePlayerSprite.");
        }
    }
}
