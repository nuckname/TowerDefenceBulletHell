using UnityEngine;

public class EnemyOnMapCounter : MonoBehaviour
{
    public int CurrentEnemiesOnMap { get; private set; }

    [SerializeField] private RoundStateManager roundStateManager;

    public void IncreaseEnemyCount(int amount)
    {
        print(CurrentEnemiesOnMap);
        CurrentEnemiesOnMap += amount;
    }

    public void DecreaseEnemyCount()
    {
        CurrentEnemiesOnMap--;
        if (CurrentEnemiesOnMap <= 0)
        {
            Debug.Log("No Enemies Left");
            roundStateManager.SwitchState(roundStateManager.roundOverState);
        }
    }
}