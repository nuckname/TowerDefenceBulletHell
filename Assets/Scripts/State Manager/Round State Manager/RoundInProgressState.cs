using System.Linq.Expressions;
using UnityEngine;

public class RoundInProgressState : RoundBaseState
{
    private int roundCounter = 1;
    public override void EnterState(RoundStateManager roundStateManager)
    {
        Debug.Log("Round In Progress State");

        roundStateManager.AllowTurretsToShoot(false);
        
        SpawnRoundEnemies(roundCounter, roundStateManager);
    }

    public override void UpdateState(RoundStateManager roundStateManager)
    {
        
    }

    public override void OnCollisionEnter2D(RoundStateManager roundStateManager, Collision2D other)
    {
        
    }

    private void SpawnRoundEnemies(int roundCounter, RoundStateManager roundStateManager)
    {
        if (roundCounter == 1)
        {
            roundStateManager.SpawnBasicEnemies(3,0,0,0,0,0);
        }
        
        if (roundCounter == 2)
        {
            roundStateManager.SpawnBasicEnemies(3,0,0,0,0,0);
        }
        
        roundCounter++;
        roundStateManager.SwitchState(roundStateManager.roundOverState);
    }
    
}
