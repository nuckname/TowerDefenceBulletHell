using UnityEngine;

public class BossDie : MonoBehaviour
{
    [SerializeField] private EnemyDropItems _enemyDropItems;
    private EnemyOnMapCounter enemyOnMapCounter;

    private void Awake()
    {
        //I dont understand why I cant just get a refernfce it doesnt work.
        enemyOnMapCounter = GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<EnemyOnMapCounter>();
    }
    
    public void EnemyHasDied()
    {
        print("Boss DecreaseEnemyCount - 1");
        enemyOnMapCounter.DecreaseEnemyCount();

        _enemyDropItems.DropItems(true);

        Destroy(gameObject);
    }
}
