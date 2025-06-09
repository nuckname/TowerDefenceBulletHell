using UnityEngine;

/// <summary>
/// Controls player action states when the upgrade UI is open, preventing shooting and turret placement.
/// </summary>
public class PlayerActionController : MonoBehaviour
{
    private GameObject player;
    private PlaceTurret placeTurretComponent;
    
    private void Awake()
    {
        CachePlayerComponents();
    }

    private void CachePlayerComponents()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            placeTurretComponent = player.GetComponent<PlaceTurret>();
        }
        else
        {
            Debug.LogWarning("Player GameObject not found!");
        }
    }

    /// <summary>
    /// Disables or enables player actions based on UI state.
    /// </summary>
    /// <param name="disable">True to disable actions, false to enable them</param>
    public void DisableActions(bool disable)
    {
        if (disable)
        {
            Debug.Log("Disabling player actions: shooting and turret placement");
            DisablePlayerActions();
        }
        else
        {
            Debug.Log("Enabling player actions: shooting and turret placement");
            EnablePlayerActions();
        }
    }

    private void DisablePlayerActions()
    {
        // Disable shooting
        PlayerShooting.disableShooting = true;
        
        // Disable turret placement
        if (placeTurretComponent != null)
        {
            placeTurretComponent.allowTurretPlacement = false;
        }
        
        // Additional UI state management could go here
        // OnClickEffect.UiOpenCantUpgradeTurret = true;
    }

    private void EnablePlayerActions()
    {
        // Enable shooting
        PlayerShooting.disableShooting = false;
        
        // Enable turret placement
        if (placeTurretComponent != null)
        {
            placeTurretComponent.allowTurretPlacement = true;
        }
        
        // Additional UI state management could go here
        // OnClickEffect.UiOpenCantUpgradeTurret = false;
    }

    /// <summary>
    /// Emergency method to ensure all actions are re-enabled (e.g., on component destruction)
    /// </summary>
    public void ForceEnableAllActions()
    {
        EnablePlayerActions();
    }

    private void OnDestroy()
    {
        // Ensure actions are re-enabled when this component is destroyed
        ForceEnableAllActions();
    }
}