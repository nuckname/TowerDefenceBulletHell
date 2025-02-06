using UnityEngine;

public class EnemyOnMapCounter : MonoBehaviour
{
    public int MaxEnemiesOnMap = 0;
    
    [SerializeField] private RoundStateManager roundStateManager;

    public void DecreaseEnemyCount()
    {
        MaxEnemiesOnMap--;
        if (MaxEnemiesOnMap <= 0)
        {
            roundStateManager.SwitchState(roundStateManager.roundOverState);
        }
    }
}