using UnityEngine;

public class RoundOverGameState : GameBaseState
{
    public override void EnterState(GameStateManager gameStateManager)
    {
        Debug.Log("Round Over State");
    }

    public override void UpdateState(GameStateManager gameStateManager)
    {

    }

    public override void OnCollisionEnter(GameStateManager gameStateManager, Collision collision)
    {
        
    }
}
