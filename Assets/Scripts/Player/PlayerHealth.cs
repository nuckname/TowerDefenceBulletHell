using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerHealthScriptabeObject playerHealthScriptabeObject; 
    private TextMeshProUGUI healthText; 
    
    //public GameObject floatingTextPrefab;
    private void Awake()
    {
        healthText = GameObject.FindGameObjectWithTag("PlayerTextHP").GetComponent<TextMeshProUGUI>();
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
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    


}
