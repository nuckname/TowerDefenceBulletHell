using UnityEngine;

public class RoundOverGameState : GameBaseState
{
    public override void EnterState(GameStateManager gameStateManager)
    {
        Debug.Log("Round Over State");
    }

    public override void UpdateState(GameStateManager gameStateManager)
    {
        //Turrets stop firing.
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameStateManager.SwitchState(gameStateManager._beginGameState);
        }
    }

    public override void OnCollisionEnter(GameStateManager gameStateManager, Collision collision)
    {
        
    }
}
