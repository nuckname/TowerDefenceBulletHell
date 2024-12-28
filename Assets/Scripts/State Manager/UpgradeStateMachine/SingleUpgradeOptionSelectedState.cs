using UnityEngine;

public class SingleUpgradeOptionSelectedState : GameBaseState
{
    private int singleUpgradeCounter = 0;
    private GameObject uiCirclePlusBoxCach;
    private Vector3 playerPosition;
    
    public override void EnterState(UpgradeStateManager upgradeStateManager)
    {
        Debug.Log("Single Upgrade Option Selected State");
        // Highlight the selected turret and show upgrade details
    }

    public override void UpdateState(UpgradeStateManager upgradeStateManager)
    {
        //switch state when e has been pressed two times.
        //and colliding

        if (Input.GetKeyDown(KeyCode.E))
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
        GameObject other = collider.gameObject;

        Debug.Log("TriggerEnter");
        if (other.gameObject.CompareTag("UiSquare"))
        {
            uiCirclePlusBoxCach = Object.Instantiate(upgradeStateManager.uiCirclePlusBoxPrefab, playerPosition + new Vector3(0, 2, 0), Quaternion.identity);
            
        }
    }
    
    public override void OnTriggerExit2D(UpgradeStateManager upgradeStateManager, Collider2D collider)
    {
        Debug.Log("TriggerExit");
        GameObject other = collider.gameObject;

        if (other.gameObject.CompareTag("UiSquare"))
        {
            if (uiCirclePlusBoxCach != null)
            {
                Object.Destroy(uiCirclePlusBoxCach);
                //reset box
                uiCirclePlusBoxCach = null;
            }
            else
            {
                Debug.LogError("Single State: something wrong with Upgrade Box ui");
            }
        }
    }
}
