using System;
using UnityEngine;

/// <summary>
/// Handles all input for the upgrade UI including keyboard shortcuts and button selections.
/// </summary>
public class UpgradeInputHandler : MonoBehaviour
{
    public event Action OnExitRequested;
    public event Action OnRerollRequested;
    public event Action<int> OnUpgradeSelected;
    
    public int SelectedUpgradeIndex { get; private set; } = 0;

    public void HandleInput()
    {
        HandleExitInput();
        HandleRerollInput();
        HandleUpgradeSelectionInput();
    }

    private void HandleExitInput()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
        {
            OnExitRequested?.Invoke();
        }
    }

    private void HandleRerollInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnRerollRequested?.Invoke();
        }
    }

    private void HandleUpgradeSelectionInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectUpgrade(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectUpgrade(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectUpgrade(2);
        }
    }

    public void SelectUpgrade(int index)
    {
        if (index >= 0 && index <= 2)
        {
            SelectedUpgradeIndex = index;
            OnUpgradeSelected?.Invoke(index);
        }
        else
        {
            Debug.LogWarning($"Invalid upgrade index: {index}");
        }
    }

    // Called by UI buttons
    public void OnTopButtonClicked() => SelectUpgrade(0);
    public void OnMiddleButtonClicked() => SelectUpgrade(1);
    public void OnBottomButtonClicked() => SelectUpgrade(2);
}