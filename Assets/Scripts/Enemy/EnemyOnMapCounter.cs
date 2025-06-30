using UnityEngine;

public class EnemyOnMapCounter : MonoBehaviour
{
    public int MaxEnemiesOnMap = 0;
    
    [SerializeField] private RoundStateManager roundStateManager;

    public void DecreaseEnemyCount()
    {
        //Update music volume
        roundStateManager.OnEnemyCountChanged(MaxEnemiesOnMap);

        MaxEnemiesOnMap--;
        if (MaxEnemiesOnMap <= 0)
        {
            roundStateManager.SwitchState(roundStateManager.roundOverState);
        }
    }

    public void AddEnemyCount(int amount)
    {
        MaxEnemiesOnMap += amount;
    }
}