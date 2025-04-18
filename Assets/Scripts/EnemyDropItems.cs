using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDropItems : MonoBehaviour
{
    [SerializeField] private Vector3 bossDropItemsLocation;

    //Updated from SpawnEnemies.cs
    public int amountOfGoldCoinsToDrop;
    public int amountOfHeartsToDrop;

    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject heart;

    public void DropItems(bool isBoss)
    {
        for (int i = 0; i < amountOfGoldCoinsToDrop; i++)
        {
            SpawnItem(coin, isBoss);
        }
        
        for (int i = 0; i < amountOfHeartsToDrop; i++)
        {
            SpawnItem(heart, isBoss);
        }
    }

    private void SpawnItem(GameObject item, bool isBoss)
    {
        float displacementY = Random.Range(-1.0f, 1.0f);
        float displacementX = Random.Range(-1.0f, 1.0f);

        if (!isBoss)
        {
            Instantiate(item, new Vector3(gameObject.transform.position.x + displacementX,
                gameObject.transform.position.y + displacementY,
                gameObject.transform.position.z), Quaternion.identity);
        }

        if (isBoss)
        {
            Instantiate(item, new Vector3(bossDropItemsLocation.x + displacementX,
                bossDropItemsLocation.y + displacementY,
                bossDropItemsLocation.z), Quaternion.identity);
        }
    }

    private void SpawnBossGold()
    {
        
    }
}
