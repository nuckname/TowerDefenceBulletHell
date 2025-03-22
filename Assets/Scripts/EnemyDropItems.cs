using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDropItems : MonoBehaviour
{
    
    //Updated from SpawnEnemies.cs
    public int amountOfGoldCoinsToDrop;
    public int amountOfHeartsToDrop;

    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject heart;

    public GameObject roundManagerPrefab;


    public void DropItems()
    {
        for (int i = 0; i < amountOfGoldCoinsToDrop; i++)
        {
            SpawnItem(coin);
        }
        
        for (int i = 0; i < amountOfHeartsToDrop; i++)
        {
            SpawnItem(heart);
        }
    }

    private void SpawnItem(GameObject item)
    {
        float displacementY = Random.Range(-1.0f, 1.0f);
        float displacementX = Random.Range(-1.0f, 1.0f);
            
        Instantiate(item, new Vector3(gameObject.transform.position.x + displacementX,
            gameObject.transform.position.y + displacementY,
            gameObject.transform.position.z), Quaternion.identity);
    }
}
