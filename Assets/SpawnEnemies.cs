using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float enemySpeed;
    
    public int amountOfEnemiesSpawned = 0;

    public void SpawnEnemiesPerRound(int redEnemies, int orangeEnemies, int yellowEnemies, int greenEnemies, int blueEnemies, int purpleEnemies)
    {
        amountOfEnemiesSpawned = 0;
        StartCoroutine(SpawnEnemiesWithDelay(redEnemies, Color.red, 1));
        StartCoroutine(SpawnEnemiesWithDelay(orangeEnemies, new Color(1f, 0.5f, 0f), 2));
        StartCoroutine(SpawnEnemiesWithDelay(yellowEnemies, Color.yellow, 3));
        StartCoroutine(SpawnEnemiesWithDelay(greenEnemies, Color.green, 4));
        StartCoroutine(SpawnEnemiesWithDelay(blueEnemies, Color.blue, 5));
        StartCoroutine(SpawnEnemiesWithDelay(purpleEnemies, new Color(0.5f, 0f, 0.5f), 6));
        
        Debug.Log("Total Enemies Spawned");
        //pass in EnemyOnMapCouter
        
    }

    private IEnumerator SpawnEnemiesWithDelay(int numberOfEnemies, Color color, int health)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject enemy = Instantiate(this.enemy, spawnPoint.position, Quaternion.identity);
            enemy.GetComponent<SpriteRenderer>().color = color;
            enemy.GetComponent<EnemyHealth>().EnemyStartingHealth = health;

            amountOfEnemiesSpawned++;
            
            yield return new WaitForSeconds(0.5f); 
        }
    }
}
