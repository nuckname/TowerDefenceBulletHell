
using UnityEngine;

public abstract class GameBaseState
{
    public abstract void EnterState(UpgradeStateManager upgradeStateManager);
    public abstract void UpdateState(UpgradeStateManager upgradeStateManager);
    
    public abstract void OnTriggerEnter2D(UpgradeStateManager upgradeStateManager, Collider2D collider);

    public abstract void OnTriggerExit2D(UpgradeStateManager upgradeStateManager, Collider2D collider);
}
