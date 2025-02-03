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
        // Use a switch statement for better readability
        switch (roundCounter)
        {
            case 1:
                roundStateManager.SpawnBasicEnemies(1); // 1 basic enemy
                break;
            case 2:
                roundStateManager.SpawnBasicEnemies(2); // 2 basic enemies
                break;
            case 3:
                roundStateManager.SpawnBasicEnemies(3); // 3 basic enemies
                break;
            case 4:
                roundStateManager.SpawnBasicEnemies(4); // 4 basic enemies
                break;
            case 5:
                roundStateManager.SpawnBasicEnemies(5); // 5 basic enemies
                break;
            case 6:
                roundStateManager.SpawnBasicEnemies(6); // 6 basic enemies
                break;
            case 7:
                roundStateManager.SpawnBasicEnemies(7); // 7 basic enemies
                break;
            case 8:
                roundStateManager.SpawnBasicEnemies(8); // 8 basic enemies
                break;
            case 9:
                roundStateManager.SpawnBasicEnemies(9); // 9 basic enemies
                break;
            case 10:
                roundStateManager.SpawnBasicEnemies(10); // 10 basic enemies
                break;
            default:
                // Handle rounds beyond 10 (if needed)
                Debug.LogWarning($"Round {roundCounter} is not defined. Defaulting to 10 enemies.");
                roundStateManager.SpawnBasicEnemies(10);
                break;
        }

        Debug.Log($"Round {roundCounter} enemies spawned.");

        // Ensure the state is not switched too early
        // Uncomment and debug if necessary
        // roundStateManager.SwitchState(roundStateManager.roundOverState);
    }
    
}
