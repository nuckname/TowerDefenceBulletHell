
using UnityEngine;

public class NoUpgradeSelectedState : GameBaseState
{
    //uses Upgrade Radius script
    // in player prefab
    private Collision2D[] collider;
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
        //add to collider?
        //I think i need to pass collider between scripts
        
        
        // No specific collision logic for this state
    }
    
    public override void OnTriggerExit2D(UpgradeStateManager upgradeStateManager, Collider2D collider)
    {
        //remove from collider
        
        
        // No specific collision logic for this state
    }
}
