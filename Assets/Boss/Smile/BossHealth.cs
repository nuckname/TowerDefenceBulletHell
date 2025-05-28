using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private EnemyDie _enemyDie;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    [Header("Flash Effect")]
    public SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public float flashDuration = 0.2f;

    [Header("UI Elements")] 
    public GameObject healthBarPrefab;
    private Slider healthBarSlider;
    private GameObject spawnedHealthBar;
    private TextMeshProUGUI healthBarTexted;

    private float redBoxTriggerDelay = 5f;
    private float spawnTime;

    [SerializeField] private bool isSnake = false;
    [SerializeField] private SnakeBossController snakeBossController;

    private void Start()
    {
        currentHealth = maxHealth;
        spawnTime = Time.time;

        if (!isSnake)
        {
            SpawnHealthBar(70);
        }
    }

    public void SpawnHealthBar(int healthAmount)
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        spawnedHealthBar = Instantiate(healthBarPrefab, canvas.transform);
            
        healthBarTexted = spawnedHealthBar.GetComponent<TextMeshProUGUI>();
        RectTransform rectTransform = spawnedHealthBar.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 0); 
        currentHealth = healthAmount;
        maxHealth = healthAmount;
        
        UpdateHealthUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Basically this collision shouldnt work on the snake boss and only the same.
        //Demo rush
        
        if (collision.CompareTag("Bullet") && !isSnake)
        {
            TakeDamage(1); 
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("RedBox"))
        {
            //if (Time.time - spawnTime >= redBoxTriggerDelay && !isSnake)
            if (Time.time - spawnTime >= redBoxTriggerDelay)
            {
                Destroy(spawnedHealthBar);
                Destroy(gameObject);
                DeathScreenController.Instance.Show();
            }
            else
            {
                Debug.Log("RedBox trigger ignored (within first 5 seconds).");
            }
        }
        
        //We handle snake collisions in SnakeBossController.cs
        if (collision.CompareTag("PlayerBullet") && !isSnake)
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
        healthBarTexted.text = currentHealth + " / " + maxHealth;
        
        /*
         //No longer slider now Text
        if (healthBarPrefab != null)
            healthBarSlider.value = (float)currentHealth / maxHealth;
            */
    }

    private IEnumerator FlashEffect()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = Color.white; 
    }

    private void Die()
    {
        _enemyDie.EnemyHasDied();

        if (isSnake)
        {
            print("win screen");
            snakeBossController.DestroyBoss();
        }
        
        Destroy(spawnedHealthBar);
        
        Debug.Log("Boss Defeated!");
    }
}
