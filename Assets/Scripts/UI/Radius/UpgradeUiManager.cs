using System;
using UnityEngine;

/// <summary>
/// Main coordinator for the upgrade UI system. Handles high-level flow and delegates to specialized components.
/// </summary>
public class UpgradeUiManager : MonoBehaviour
{
    [Header("Target")]
    public GameObject targetTurret;
    
    [Header("Components")]
    [SerializeField] private UpgradeDisplayController displayController;
    [SerializeField] private UpgradeInputHandler inputHandler;
    [SerializeField] private UpgradePurchaseHandler purchaseHandler;
    [SerializeField] private UpgradeRerollHandler rerollHandler;
    [SerializeField] private PlayerActionController actionController;
    
    private void Awake()
    {
        InitializeComponents();
    }

    private void Start()
    {
        actionController.DisableActions(true);
        ValidateComponents();
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            inputHandler.HandleInput();
        }
    }

    private void InitializeComponents()
    {
        if (displayController == null) displayController = GetComponent<UpgradeDisplayController>();
        if (inputHandler == null) inputHandler = GetComponent<UpgradeInputHandler>();
        if (purchaseHandler == null) purchaseHandler = GetComponent<UpgradePurchaseHandler>();
        if (rerollHandler == null) rerollHandler = GetComponent<UpgradeRerollHandler>();
        if (actionController == null) actionController = GetComponent<PlayerActionController>();
        
        // Set up event subscriptions
        inputHandler.OnExitRequested += ExitSelection;
        inputHandler.OnRerollRequested += rerollHandler.Reroll;
        inputHandler.OnUpgradeSelected += purchaseHandler.SelectUpgrade;
        
        purchaseHandler.OnUpgradePurchased += OnUpgradePurchased;
        rerollHandler.OnRerollCompleted += displayController.RefreshDisplay;
    }

    private void ValidateComponents()
    {
        if (displayController == null) Debug.LogError("UpgradeDisplayController is missing!");
        if (inputHandler == null) Debug.LogError("UpgradeInputHandler is missing!");
        if (purchaseHandler == null) Debug.LogError("UpgradePurchaseHandler is missing!");
    }

    public void SetupUpgradesForTurret(GameObject turret)
    {
        targetTurret = turret;
        displayController.SetupUpgrades(turret);
        purchaseHandler.SetTargetTurret(turret);
        rerollHandler.SetTargetTurret(turret);
    }

    public void ExitSelection()
    {
        OnClickEffect.UiOpenCantUpgradeTurret = false;
        AudioManager.instance.backSFX();
        
        actionController.DisableActions(false);
        Destroy(gameObject);
    }

    private void OnUpgradePurchased()
    {
        actionController.DisableActions(false);
        rerollHandler.ResetRerollCount();
    }

    private void OnDestroy()
    {
        // Clean up event subscriptions
        if (inputHandler != null)
        {
            inputHandler.OnExitRequested -= ExitSelection;
            inputHandler.OnRerollRequested -= rerollHandler.Reroll;
            inputHandler.OnUpgradeSelected -= purchaseHandler.SelectUpgrade;
        }
        
        if (purchaseHandler != null)
        {
            purchaseHandler.OnUpgradePurchased -= OnUpgradePurchased;
        }
        
        if (rerollHandler != null)
        {
            rerollHandler.OnRerollCompleted -= displayController.RefreshDisplay;
        }
    }
}