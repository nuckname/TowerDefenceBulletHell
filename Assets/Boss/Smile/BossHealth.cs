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
    public List<SpriteRenderer> spriteRenderer;
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
    private void Start()
    {
        
        currentHealth = maxHealth;
        spawnTime = Time.time;

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas not found in the scene!");
            return;
        }


        if (!isSnake)
        {
            spawnedHealthBar = Instantiate(healthBarPrefab, canvas.transform);
            
            healthBarTexted = spawnedHealthBar.GetComponent<TextMeshProUGUI>();
        }
        
        RectTransform rectTransform = spawnedHealthBar.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 0); 

        UpdateHealthUI();
    }

    public void SpawnHealthBar(int healthAmount)
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        spawnedHealthBar = Instantiate(healthBarPrefab, canvas.transform);
        currentHealth = healthAmount;
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
            print("hacking snake not working on end eaching end");
            if (Time.time - spawnTime >= redBoxTriggerDelay && !isSnake)
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
        healthBarTexted.text = currentHealth + " / " + maxHealth;
        
        /*
         //No longer slider now Text
        if (healthBarPrefab != null)
            healthBarSlider.value = (float)currentHealth / maxHealth;
            */
    }

    private IEnumerator FlashEffect()
    {
        foreach (SpriteRenderer _spriteRenderer in spriteRenderer)
        {
            _spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(flashDuration);
            _spriteRenderer.color = Color.white; 
        }
    }

    private void Die()
    {
        _enemyDie.EnemyHasDied();
        Destroy(spawnedHealthBar);
        Debug.Log("Boss Defeated!");
    }
}
