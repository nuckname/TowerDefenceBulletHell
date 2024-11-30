using Unity.VisualScripting;
using UnityEngine;

public class EnemyDropItems : MonoBehaviour
{
    private int minimumGoldCoins = 1;
    private int maximumGoldCoins = 4;

    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject heart;
    
    public void DropItems()
    {
        int amountOfGoldToDrop = GenerateAmount(minimumGoldCoins, maximumGoldCoins);
        for (int i = 0; i <= amountOfGoldToDrop; i++)
        {
            Instantiate(coin, gameObject.transform.position, Quaternion.identity);
        }
        
        int amountOfHeartsToDrop = GenerateAmount(minimumGoldCoins, maximumGoldCoins);
        for (int i = 0; i <= amountOfHeartsToDrop; i++)
        {
            Instantiate(heart, gameObject.transform.position, Quaternion.identity);
        }
        
    }

    private int GenerateAmount(int min, int max)
    {
        int randomAmount = Random.Range(min, max);
        return randomAmount;
    }
}
