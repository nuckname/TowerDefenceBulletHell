
using UnityEngine;

public class SingleUpgradeOptionSelectedState : GameBaseState
{
    private int singleUpgradeCounter = 0;
    public override void EnterState(UpgradeStateManager upgradeStateManager)
    {
        Debug.Log("Single Upgrade Option Selected State");
        // Highlight the selected turret and show upgrade details
    }

    public override void UpdateState(UpgradeStateManager upgradeStateManager)
    {
        //switch state when e has been pressed two times.
        
        if (Input.GetKeyDown(KeyCode.E)) // Example key for showing multiple options
        {
            singleUpgradeCounter++;

            if (singleUpgradeCounter == 2)
            {
                upgradeStateManager.SwitchState(upgradeStateManager._multipleUpgradeChoicesState);
            }
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
