using System;
using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles the purchase logic for upgrades including validation, payment, and feedback.
/// </summary>
public class UpgradePurchaseHandler : MonoBehaviour
{
    [Header("Purchase Feedback")]
    [SerializeField] private Color insufficientFundsColor = Color.red;
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeMagnitude = 5f;
    
    public event Action OnUpgradePurchased;
    
    private GameObject targetTurret;
    private UpgradeDisplayController displayController;
    private ApplyUpgrade applyUpgrade;
    private int selectedUpgradeIndex = 0;

    private void Awake()
    {
        displayController = GetComponent<UpgradeDisplayController>();
        applyUpgrade = GetComponentInParent<ApplyUpgrade>();
    }

    public void SetTargetTurret(GameObject turret)
    {
        targetTurret = turret;
    }

    public void SelectUpgrade(int index)
    {
        selectedUpgradeIndex = index;
    }

    public void AttemptPurchase()
    {
        if (targetTurret == null)
        {
            Debug.LogError("No target turret set for purchase!");
            return;
        }

        var storage = targetTurret.GetComponent<StoreTurretDescriptionAndRarity>();
        int upgradePrice = storage.storeTurretPrice;
        
        if (PlayerGold.Instance.SpendGold(upgradePrice))
        {
            ProcessSuccessfulPurchase(storage, upgradePrice);
        }
        else
        {
            ProcessFailedPurchase();
        }
    }

    private void ProcessSuccessfulPurchase(StoreTurretDescriptionAndRarity storage, int price)
    {
        string chosenUpgrade = displayController.DisplayedUpgrades[selectedUpgradeIndex];
        applyUpgrade.ChosenUpgrade(chosenUpgrade, targetTurret);
        
        AudioManager.instance.BuyTurretUpgradeSFX();
        OnUpgradePurchased?.Invoke();
    }

    private void ProcessFailedPurchase()
    {
        StopAllCoroutines();
        TMP_Text upgradeText = displayController.GetUpgradeText(selectedUpgradeIndex);
        
        if (upgradeText != null)
        {
            StartCoroutine(ShowInsufficientFundsFeedback(upgradeText));
        }
        
        AudioManager.instance.GibberishSFX();
    }

    private IEnumerator ShowInsufficientFundsFeedback(TMP_Text textComponent)
    {
        // Store original values
        var originalColor = textComponent.color;
        var rectTransform = textComponent.rectTransform;
        Vector3 originalPosition = rectTransform.localPosition;
        
        // Change color to indicate insufficient funds
        textComponent.color = insufficientFundsColor;
        
        // Shake animation
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            float offsetX = (UnityEngine.Random.value * 2f - 1f) * shakeMagnitude;
            rectTransform.localPosition = originalPosition + new Vector3(offsetX, 0f, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // Restore original state
        rectTransform.localPosition = originalPosition;
        textComponent.color = originalColor;
    }
}