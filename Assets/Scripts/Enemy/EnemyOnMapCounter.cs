using UnityEngine;

public class EnemyOnMapCounter : MonoBehaviour
{
    public int CurrentEnemiesOnMap { get; private set; }

    [SerializeField] private RoundStateManager roundStateManager;

    public void IncreaseEnemyCount(int amount)
    {
        CurrentEnemiesOnMap += amount;
        print("enemy CurrentEnemiesOnMap" + CurrentEnemiesOnMap);
    }

    public void DecreaseEnemyCount()
    {
        CurrentEnemiesOnMap--;
        print("enemy : -1 enemy");
        if (CurrentEnemiesOnMap <= 0)
        {
            roundStateManager.SwitchState(roundStateManager.roundOverState);
        }
    }
}