using UnityEngine;

public class EnemyOnMapCounter : MonoBehaviour
{
    public int CurrentEnemiesOnMap { get; private set; }

    [SerializeField] private RoundStateManager roundStateManager;

    public void IncreaseEnemyCount(int amount)
    {
        CurrentEnemiesOnMap += amount;
        print("CurrentEnemiesOnMap" + CurrentEnemiesOnMap);
    }

    public void DecreaseEnemyCount()
    {
        CurrentEnemiesOnMap--;
        if (CurrentEnemiesOnMap <= 0)
        {
            roundStateManager.SwitchState(roundStateManager.roundOverState);
        }
    }
}