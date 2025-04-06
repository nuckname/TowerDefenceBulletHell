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
            playerHealthScriptabeObject.currentHealth = 10;
        }
    }

//xdd
    private void Update()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthText.text = "Player Health: " + playerHealthScriptabeObject.currentHealth.ToString();
    }

    public void PlayerHasDied()
    {
        SetGameModeOneHp();
        playerGoldScriptableObject.ResetGold();
        
        deathScreen.SetActive(true);
        
        tutorialStateSO.playerTutorial = false;
        
        Destroy(player);
    }
    


}
