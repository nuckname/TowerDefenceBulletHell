
using UnityEngine;

public class MultipleUpgradeChoicesAvailableState : GameBaseState
{
    private int multiUpgradeCounter = 0;

    public override void EnterState(UpgradeStateManager upgradeStateManager)
    {
        Debug.Log("Multiple Upgrade Choices Available State");
    }

    public override void UpdateState(UpgradeStateManager upgradeStateManager)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            multiUpgradeCounter++;

            if (multiUpgradeCounter == 3)
            {
                upgradeStateManager.SwitchState(upgradeStateManager._noUpgradeSelectedState);
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
