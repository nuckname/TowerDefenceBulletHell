using System;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles the reroll functionality for upgrade options including cost management and validation.
/// </summary>
public class UpgradeRerollHandler : MonoBehaviour
{
    [Header("Reroll Settings")]
    [SerializeField] private int defaultRerollCost = 30;
    [SerializeField] private TMP_Text rerollText;
    
    public event Action<GameObject> OnRerollCompleted;
    
    private GameObject targetTurret;
    private int currentRerollCost;
    private int rerollCount = 1;
    
    // Dependencies
    private GenerateRarity generateRarity;
    private SelectDescription selectDescription;
    private UpgradeGold upgradeGold;
    
    public int CurrentRerollCost => currentRerollCost;

    private void Awake()
    {
        generateRarity = GetComponentInParent<GenerateRarity>();
        selectDescription = GetComponentInParent<SelectDescription>();
        upgradeGold = GetComponentInParent<UpgradeGold>();
        
        ResetRerollCount();
    }

    public void SetTargetTurret(GameObject turret)
    {
        targetTurret = turret;
        UpdateRerollDisplay();
    }

    public void Reroll()
    {
        if (targetTurret == null)
        {
            Debug.LogWarning("Cannot reroll: no target turret set");
            return;
        }

        StoreTurretDescriptionAndRarity storage = targetTurret.GetComponent<StoreTurretDescriptionAndRarity>();
        
        if (PlayerGold.Instance.SpendGold(storage.storeTurretRerollPrice))
        {
            ProcessReroll(storage);
        }
        else
        {
            Debug.Log("Insufficient funds for reroll");
        }
    }

    private void ProcessReroll(StoreTurretDescriptionAndRarity storage)
    {
        string previousRarity = storage.storedTurretSelectedRarity;
        
        // Generate new upgrades with different rarity
        GenerateNewUpgrades(storage, previousRarity);
        
        // Update costs
        UpdateRerollCost(storage);
        UpdateUpgradePrice(storage);
        
        AudioManager.instance.RerollTurretSFX();
        OnRerollCompleted?.Invoke(targetTurret);
    }

    private void GenerateNewUpgrades(StoreTurretDescriptionAndRarity storage, string oldRarity)
    {
        var turretStats = targetTurret.GetComponent<TurretStats>();
        var upgradeData = targetTurret.GetComponent<UpgradeDataOnTurret>();
        
        string newRarity = generateRarity.SelectRarity("", turretStats);
        
        // Ensure we get a different rarity
        while (newRarity == oldRarity)
        {
            newRarity = generateRarity.SelectRarity("", turretStats);
        }
        
        storage.storedTurretSelectedRarity = newRarity;
        storage.storedTurretDescription = selectDescription.Get3Descriptions(newRarity, upgradeData);
    }

    private void UpdateRerollCost(StoreTurretDescriptionAndRarity storage)
    {
        // Store current reroll price for next reroll
        storage.storeTurretRerollPrice = currentRerollCost;
        UpdateRerollDisplay();
    }

    private void UpdateUpgradePrice(StoreTurretDescriptionAndRarity storage)
    {
        var turretStats = targetTurret.GetComponent<TurretStats>();
        int newUpgradePrice = upgradeGold.DisplayGold(storage.storedTurretSelectedRarity, turretStats.totalAmountOfUpgrades);
        storage.storeTurretPrice = newUpgradePrice;
    }

    private void UpdateRerollDisplay()
    {
        if (rerollText != null && targetTurret != null)
        {
            var storage = targetTurret.GetComponent<StoreTurretDescriptionAndRarity>();
            rerollText.text = $"Reroll: ${storage.storeTurretRerollPrice}";
        }
    }

    public void ResetRerollCount()
    {
        rerollCount = 1;
        currentRerollCost = defaultRerollCost;
        
        if (targetTurret != null)
        {
            var storage = targetTurret.GetComponent<StoreTurretDescriptionAndRarity>();
            storage.storeTurretRerollPrice = currentRerollCost;
            UpdateRerollDisplay();
        }
    }

    // Alternative reroll cost scaling system
    private void UpdateRerollCostProgressive()
    {
        switch (rerollCount)
        {
            case 1:
                currentRerollCost = 20;
                break;
            case 2:
                currentRerollCost = 10;
                break;
            case >= 3:
                currentRerollCost = 0;
                break;
        }
        rerollCount++;
    }
}