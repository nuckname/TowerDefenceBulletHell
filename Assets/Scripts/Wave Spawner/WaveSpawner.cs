using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WavePersonalityType
{
    Random,      // Default behavior
    Swarm,       // Many weak enemies
    Elite,       // Few strong enemies
    Fast         // Faster enemies (with speed cost multiplier)
}

[System.Serializable]
public class WavePersonality
{
    public WavePersonalityType type;
    public float speedMultiplier = 1f;
    public int maxEnemies = 50;
    public int costMultiplier = 1; // Cost multiplier for speed adjustments
    public string description;
}

public class WaveSpawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public List<WavePersonality> personalities = new List<WavePersonality>();
    public int currWave;
    private int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public Transform[] spawnLocation;
    public int spawnIndex;
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
    private WavePersonality currentPersonality;
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        InitializePersonalities();
        GenerateWave();
    }

    void InitializePersonalities()
    {
        // Define default personalities
        personalities = new List<WavePersonality>{
            new WavePersonality{
                type = WavePersonalityType.Swarm,
                speedMultiplier = 1.0f,
                maxEnemies = 60,
                costMultiplier = 1,
                description = "Massive numbers of weak enemies"
            },
            new WavePersonality{
                type = WavePersonalityType.Elite,
                speedMultiplier = 1.2f,
                maxEnemies = 15,
                costMultiplier = 2,
                description = "Small number of powerful enemies"
            },
            new WavePersonality{
                type = WavePersonalityType.Fast,
                speedMultiplier = 1.5f,
                maxEnemies = 30,
                costMultiplier = 3,
                description = "Fast-moving enemies with speed penalty"
            }
        };
    }

    void FixedUpdate()
    {
        // Existing FixedUpdate code remains the same
        // ... [Keep previous FixedUpdate implementation] ...
    }

    public void GenerateWave()
    {
        // Select random personality
        currentPersonality = personalities[Random.Range(0, personalities.Count)];
        waveValue = (currWave * 10) * currentPersonality.costMultiplier;
        
        Debug.Log($"Starting Wave {currWave} - {currentPersonality.description}");
        GenerateEnemies();
        
        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
    }

    public void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        int attempts = 0;
        int maxAttempts = 1000;

        while ((waveValue > 0 || generatedEnemies.Count < currentPersonality.maxEnemies) && attempts < maxAttempts)
        {
            attempts++;
            List<Enemy> filteredEnemies = FilterEnemiesByPersonality();
            if (filteredEnemies.Count == 0) break;

            int randEnemyId = Random.Range(0, filteredEnemies.Count);
            Enemy selectedEnemy = filteredEnemies[randEnemyId];
            
            int modifiedCost = selectedEnemy.cost * currentPersonality.costMultiplier;
            
            if (waveValue - modifiedCost >= 0)
            {
                generatedEnemies.Add(selectedEnemy.enemyPrefab);
                waveValue -= modifiedCost;
            }
        }

        enemiesToSpawn = generatedEnemies;
    }

    List<Enemy> FilterEnemiesByPersonality()
    {
        List<Enemy> filtered = new List<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            switch (currentPersonality.type)
            {
                case WavePersonalityType.Swarm:
                    if (enemy.cost <= 2) filtered.Add(enemy);
                    break;
                case WavePersonalityType.Elite:
                    if (enemy.cost >= 5) filtered.Add(enemy);
                    break;
                case WavePersonalityType.Fast:
                    // Assume enemies have a speed component we can modify
                    var movement = enemy.enemyPrefab.GetComponent<EnemyMovement>();
                    if (movement != null) movement.speed *= currentPersonality.speedMultiplier;
                    filtered.Add(enemy);
                    break;
                default:
                    filtered.Add(enemy);
                    break;
            }
        }

        return filtered.Count > 0 ? filtered : enemies; // Fallback to all enemies
    }

    // Modified spawner to apply speed modifications
    IEnumerator SpawnEnemy(GameObject prefab)
    {
        GameObject enemy = Instantiate(prefab, spawnLocation[spawnIndex].position, Quaternion.identity);
        
        // Apply speed modifications
        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        if (movement != null)
        {
            movement.speed *= currentPersonality.speedMultiplier;
        }

        spawnedEnemies.Add(enemy);
        yield return new WaitForSeconds(spawnInterval);
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
    [Tooltip("Base movement speed")]
    public float speed;
}

// Add this to your Enemy prefabs
public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = transform.right * speed;
    }
}