using System;
using System.Collections;
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

    private float redBoxTriggerDelay = 5f;
    private float spawnTime;

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

        spawnedHealthBar = Instantiate(healthBarPrefab, canvas.transform);
        healthBarSlider = spawnedHealthBar.GetComponent<Slider>();

        if (healthBarSlider == null)
        {
            Debug.LogError("Health bar slider is missing from the instantiated object!");
            return;
        }

        RectTransform rectTransform = spawnedHealthBar.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 0); 

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
            if (Time.time - spawnTime >= redBoxTriggerDelay)
            {
                Destroy(spawnedHealthBar);
                Destroy(gameObject);
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
        if (healthBarPrefab != null)
            healthBarSlider.value = (float)currentHealth / maxHealth;
    }

    private IEnumerator FlashEffect()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = Color.white; // Reset color
    }

    private void Die()
    {
        _enemyDie.EnemyHasDied();
        Destroy(spawnedHealthBar);
        Debug.Log("Boss Defeated!");
    }
}
