using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginGameState : GameBaseState
{
    public override void EnterState(GameStateManager gameStateManager)
    {
        
    }

    public override void UpdateState(GameStateManager gameStateManager)
    {
        //Change States from Non-MonoBehaviour
        //gameStateManager.SwitchState(gameStateManager._roundOverGameState);

    }

    public override void OnCollisionEnter(GameStateManager gameStateManager, Collision collision)
    {
        
    }
}
