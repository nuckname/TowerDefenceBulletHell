using System;
using UnityEngine;
using TMPro;

public class PlayerGold : MonoBehaviour
{
    public static PlayerGold Instance { get; private set; }

    [Header("Gold Settings")]
    [SerializeField] private int startingAmountOfGold = 0;
    public int CurrentGold { get; private set; }

    [Header("UI Reference")]
    [SerializeField] private TMP_Text goldText;

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
        ResetGold();
    }

    public void ResetGold()
    {
        CurrentGold = startingAmountOfGold;
        UpdateGoldUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AddGold(100);
        }
    }

    public void AddGold(int amount)
    {
        CurrentGold += amount;
        UpdateGoldUI();
    }

    public int currentGoldAmount()
    {
        return CurrentGold;
    }

    public bool SpendGold(int amount)
    {
        if (CurrentGold >= amount)
        {
            CurrentGold -= amount;
            UpdateGoldUI();
            return true;
        }

        Debug.Log("Not enough gold!");
        return false;
    }

    private void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.SetText(CurrentGold.ToString());
        }
        else
        {
            Debug.LogWarning("Gold Text UI is not assigned.");
        }
    }
}