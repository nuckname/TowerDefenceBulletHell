using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Flash Effect")]
    public SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public float flashDuration = 0.2f;

    [Header("UI Elements")]
    public Slider healthBar;
    
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1); 
            Destroy(collision.gameObject);
        }
        
        if (collision.CompareTag("RedBox"))
        {
            print("Player should take damage.");
            TakeDamage(100);
            Destroy(collision.gameObject); 
        }
        
        if (collision.CompareTag("PlayerBullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject); 
        }
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FlashEffect());
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
            healthBar.value = (float)currentHealth / maxHealth;
    }

    private IEnumerator FlashEffect()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = Color.white; // Reset color
    }

    private void Die()
    {
        Debug.Log("Boss Defeated!");
        Destroy(gameObject); // Destroy the boss
    }
}