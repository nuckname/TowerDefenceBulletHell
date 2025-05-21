using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private TutorialStateSO tutorialStateSO;
    public PlayerHealthScriptabeObject playerHealthScriptabeObject;
    private TextMeshProUGUI healthText;

    [SerializeField] private PlayerGoldScriptableObject playerGoldScriptableObject;

    private GameModeManager gameModeManager;

    [SerializeField] private GameObject deathScreen;

    [SerializeField] private GameObject player;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip playerDieSound;
    //public GameObject floatingTextPrefab;
    private void Awake()
    {
        healthText = GameObject.FindGameObjectWithTag("PlayerTextHP").GetComponent<TextMeshProUGUI>();

        gameModeManager = GameObject.FindGameObjectWithTag("GameModeManager").GetComponent<GameModeManager>();
    }

    private void Start()
    {
        SetGameModeOneHp();
    }

    private void SetGameModeOneHp()
    {
        if (gameModeManager.CurrentMode == GameMode.OneHp)
        {
            print("Give player 1 hp");
            playerHealthScriptabeObject.currentHealth = 1;
        }
        else
        {
            //Also setting thing in PlayerHealthScriptabeObject
            playerHealthScriptabeObject.currentHealth = playerHealthScriptabeObject.startingHealth;
        }
    }

//xdd
    private void Update()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthText.text = playerHealthScriptabeObject.currentHealth.ToString();
    }

    public void PlayerHasDied()
    {
        audioSource.PlayOneShot(playerDieSound);
        
        SetGameModeOneHp();
        playerGoldScriptableObject.ResetGold();

        DeathScreenController.Instance.Show();

        tutorialStateSO.playerTutorial = false;
        
        Destroy(player);
    }
    
    


}
