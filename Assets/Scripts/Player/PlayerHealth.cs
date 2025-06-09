using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }

    [Header("Health")] 
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int startingHealth = 5;
    private int currentHealth;

    [Header("Tutorial")] 
    [SerializeField] private TutorialStateSO tutorialStateSO;

    [Header("UI")]
    private TextMeshProUGUI healthText;

    [Header("Other")]
    private GameModeManager gameModeManager;

    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject player;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip playerDieSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (healthText == null)
            healthText = GameObject.FindGameObjectWithTag("PlayerTextHP")?.GetComponent<TextMeshProUGUI>();

        if (gameModeManager == null)
            gameModeManager = GameObject.FindGameObjectWithTag("GameModeManager")?.GetComponent<GameModeManager>();
    }

    private void Start()
    {
        SetInitialHealth();
        UpdateHealthUI();
    }

    private void SetInitialHealth()
    {
        if (gameModeManager != null && gameModeManager.CurrentMode == GameMode.OneHp)
        {
            Debug.Log("Game mode is One HP — setting health to 1");
            currentHealth = 1;
            maxHealth = 1;
        }
        else
        {
            currentHealth = startingHealth;
        }
    }
    private void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = currentHealth.ToString();
    }

    public void TakeDamage(int amount)
    {
        AudioManager.instance.PlayerHurtSFX();

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            UpdateHealthUI();
            PlayerHasDied();
            return;
        }

        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHealthUI();
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void PlayerHasDied()
    {
        audioSource.PlayOneShot(playerDieSound);

        SetInitialHealth();
        
        //playerGoldScriptableObject.ResetGold();
        tutorialStateSO.playerTutorial = false;

        DeathScreenController.Instance.Show();

        if (player != null)
            Destroy(player);

        GameObject bossHealthBar = GameObject.FindGameObjectWithTag("BossHealthBar");
        if (bossHealthBar != null)
            Destroy(bossHealthBar);

        Debug.Log("Player has died.");
    }
    
    


}
