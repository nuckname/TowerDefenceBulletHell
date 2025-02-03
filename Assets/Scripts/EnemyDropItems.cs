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
            float displacementY = Random.Range(-1.0f, 1.0f);
            float displacementX = Random.Range(-1.0f, 1.0f);
            
            Instantiate(coin, new Vector3(gameObject.transform.position.x + displacementX,
                gameObject.transform.position.y + displacementY,
                gameObject.transform.position.z), Quaternion.identity);
        }
        
        
        int amountOfHeartsToDrop = GenerateAmount(minimumGoldCoins, maximumGoldCoins);
        for (int i = 0; i <= amountOfHeartsToDrop; i++)
        {
            //Only Spawn Hearts for boss fight.
            //Instantiate(heart, gameObject.transform.position, Quaternion.identity);
        }
        
    }

    private int GenerateAmount(int min, int max)
    {
        int randomAmount = Random.Range(min, max);
        return randomAmount;
    }
}
