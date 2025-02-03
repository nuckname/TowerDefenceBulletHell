using System;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class RoundStateManager : MonoBehaviour
{
    private RoundBaseState currentState;
    
    public RoundOverState roundOverState = new RoundOverState();
    public RoundInProgressState roundInProgressState = new RoundInProgressState();

    [SerializeField] private GameObject[] allTurrets;

    private SpawnEnemies spawnEnemies;
    private EnemyOnMapCounter enemyOnMapCounter;

    private void Awake()
    {
        spawnEnemies = GetComponent<SpawnEnemies>();
        enemyOnMapCounter = GetComponent<EnemyOnMapCounter>();
    }

    void Start()
    {
        currentState = roundOverState;
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
        allTurrets = GameObject.FindGameObjectsWithTag("Turret");
        
        foreach (GameObject turret in allTurrets)
        {
            turret.GetComponent<TurretShoot>().AllowTurretToShoot = shouldTurretShoot;
        }
    }
    

    public void SpawnBasicEnemies(int currentRoundIndex)
    {
        enemyOnMapCounter.MaxEnemiesOnMap = spawnEnemies.SpawnEnemiesPerRound(currentRoundIndex);
        print("Total enemies" + enemyOnMapCounter.MaxEnemiesOnMap);
    }

}
