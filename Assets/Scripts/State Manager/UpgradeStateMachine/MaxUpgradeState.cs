using UnityEngine;

public class MaxUpgradeState : GameBaseState
{
    public override void EnterState(UpgradeStateManager upgradeStateManager)
    {
        Debug.Log("No Upgrade Selected State");
    }

    public override void UpdateState(UpgradeStateManager upgradeStateManager)
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            upgradeStateManager.SwitchState(upgradeStateManager._singleUpgradeOptionState);
        }
    }

    public override void OnTriggerEnter2D(UpgradeStateManager upgradeStateManager, Collider2D collider)
    {
        // No specific collision logic for this state
    }
    
    public override void OnTriggerExit2D(UpgradeStateManager upgradeStateManager, Collider2D collider)
    {
        // No specific collision logic for this state
    }
}
