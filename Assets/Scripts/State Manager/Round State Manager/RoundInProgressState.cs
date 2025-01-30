using System.Linq.Expressions;
using UnityEngine;

public class RoundInProgressState : RoundBaseState
{
    private int roundCounter = 0;
    public override void EnterState(RoundStateManager roundStateManager)
    {
        Debug.Log("Round In Progress State");

        Debug.Log("true");

        roundStateManager.AllowTurretsToShoot(true);
        
        roundCounter++;
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
            roundStateManager.SpawnBasicEnemies(3,3,1,0,0,0);
        }
        
        else if (roundCounter == 2)
        {
            roundStateManager.SpawnBasicEnemies(5,5,2,1,0,0);
        }
        
        else if (roundCounter == 3)
        {
            roundStateManager.SpawnBasicEnemies(10,1,1,0,3,0);
        }
        
        else if (roundCounter == 4)
        {
            roundStateManager.SpawnBasicEnemies(5,3,3,2,5,1);
        }
        
        else if (roundCounter == 5)
        {
            roundStateManager.SpawnBasicEnemies(3,3,7,2,0,3);
        }
        
        else if (roundCounter == 6)
        {
            roundStateManager.SpawnBasicEnemies(10,10,10,10,10,10);
        }
        
        else if (roundCounter == 7)
        {
            roundStateManager.SpawnBasicEnemies(10,10,0,10,20,12);
        }
        
        else if (roundCounter == 8)
        {
            roundStateManager.SpawnBasicEnemies(10,0,0,25,25,25);
        }
        
        else if (roundCounter == 9)
        {
            roundStateManager.SpawnBasicEnemies(0,0,0,0,0,50);
        }
        
        else if (roundCounter == 10)
        {
            roundStateManager.SpawnBasicEnemies(0,0,0,10,25,20);
        }
        
        Debug.Log(roundCounter);
        
        //Causingbug where turrets dont shoot 
        //roundStateManager.SwitchState(roundStateManager.roundOverState);
    }
    
}
