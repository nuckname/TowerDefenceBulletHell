using System;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class RoundStateManager : MonoBehaviour
{
    private RoundBaseState currentState;

    [SerializeField] private Transform spawnPoint;
    public GameObject Enemy;
    
    public RoundOverState roundOverState = new RoundOverState();
    public RoundInProgressState roundInProgressState = new RoundInProgressState();

    public int amountOfEnemiesSpawned = 0;

    [SerializeField] private GameObject[] allTurrets;
    
    void Start()
    {
        currentState = roundOverState;
        allTurrets = GameObject.FindGameObjectsWithTag("Turret");
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentState.OnCollisionEnter2D(this, other);
    }

    public void SwitchState(RoundBaseState roundBaseState)
    {
        currentState = roundBaseState;
        roundBaseState.EnterState(this);
    }

    public void AllowTurretsToShoot(bool shouldTurretShoot)
    {
        Debug.Log($"Setting turrets to shoot: {shouldTurretShoot}");
        
        //cache some how?
        //allTurrets = GameObject.FindGameObjectsWithTag("Turret");

        foreach (GameObject turret in allTurrets)
        {
            turret.GetComponent<TurretShoot>().AllowTurretToShoot = shouldTurretShoot;
        }

    }

    public int SpawnBasicEnemies(int redEnemies, int orangeEnemies, int yellowEnemies, int greenEnemies, int blueEnemies, int purpleEnemies)
    {
        StartCoroutine(SpawnEnemiesWithDelay(redEnemies, Color.red, 1));
        StartCoroutine(SpawnEnemiesWithDelay(orangeEnemies, new Color(1f, 0.5f, 0f), 2));
        StartCoroutine(SpawnEnemiesWithDelay(yellowEnemies, Color.yellow, 3));
        StartCoroutine(SpawnEnemiesWithDelay(greenEnemies, Color.green, 4));
        StartCoroutine(SpawnEnemiesWithDelay(blueEnemies, Color.blue, 5));
        StartCoroutine(SpawnEnemiesWithDelay(purpleEnemies, new Color(0.5f, 0f, 0.5f), 6));

        return amountOfEnemiesSpawned;
    }

    private IEnumerator SpawnEnemiesWithDelay(int numberOfEnemies, Color color, int health)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject enemy = Instantiate(Enemy, spawnPoint.position, Quaternion.identity);
            enemy.GetComponent<SpriteRenderer>().color = color;
            enemy.GetComponent<EnemyHealth>().EnemyStartingHealth = health;
            amountOfEnemiesSpawned++;
        
            yield return new WaitForSeconds(0.5f); 
        }
    }
}
