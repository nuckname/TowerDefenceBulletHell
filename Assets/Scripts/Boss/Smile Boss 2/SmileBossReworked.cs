using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public enum BossState
{
    Normal,
    ExplosiveAttack,
    Enraged,
    CircleAttack,
    RandomShooting
}

public class SmileBossReworked : MonoBehaviour
{
    // --- Basic Stats ---
    //This should no longer work
    [Header("Health")]
    private float maxHealth = 1000f;
    private float currentHealth;
    
    [Header("Basic States")]
    public float moveSpeed = 1f;         // Base movement speed
    public float attackCooldown = 5f;    // Time between attacks in normal state

    // --- Explosion Attack Stats ---
    [Header("Red Circle Attack")]
    public GameObject explosiveCirclePrefab; // Prefab for the explosion circle

    [Header("Circle Attack States")]
    // --- Circle Attack Stats ---
    public GameObject circleAttackPrefab;  // Prefab that handles the rotating circle attack
    public float minFireRate = 0.2f;         // Minimum time between shots for circle attack
    public float maxFireRate = 0.8f;         // Maximum time between shots for circle attack
    public float minBulletSpeed = 3f;        // Minimum bullet speed for circle attack
    public float maxBulletSpeed = 6f;
    public int amountOfCirclePrefabs = 2;
    // Maximum bullet speed for circle attack

    [Header("Random Shooting stats")]
    // --- Random Shooting State Stats ---
    [SerializeField] private GameObject BulletPrefab; // Prefab for random projectile attack
    public float minRandomBulletSpeed = 2f;     // Minimum bullet speed for random projectiles
    public float maxRandomBulletSpeed = 5f;     // Maximum bullet speed for random projectiles
    public int randomProjectileCount = 10;      // Number of projectiles to fire in random shooting state

    // --- Internal state management ---

    [Header("Current State")]
    [SerializeField] private BossState currentState;
    private float attackTimer;
    
    // This variable tracks the last attack used so that we don't repeat it.
    // We only consider the attack abilities (ExplosiveAttack, CircleAttack, RandomShooting).
    private BossState lastAttackUsed = BossState.Normal;

    void Start()
    {
        currentHealth = maxHealth;
        currentState = BossState.Normal;
        attackTimer = attackCooldown;
    }
    
    void Update()
    {
        // Check if health has dropped to half to enter Enraged state.
        if (currentHealth <= maxHealth / 2 && currentState != BossState.Enraged)
        {
            EnterEnragedState();
        }
        
        // State machine for boss behavior.
        switch (currentState)
        {
            case BossState.Normal:
                HandleNormalState();
                break;
            case BossState.ExplosiveAttack:
                HandleExplosiveAttackState();
                break;
            case BossState.Enraged:
                HandleEnragedState();
                break;
            case BossState.CircleAttack:
                // CircleAttack state is handled via coroutine.
                break;
            case BossState.RandomShooting:
                // RandomShooting state is handled via coroutine.
                break;
        }
    }
    
    // --- Normal state behavior ---
    void HandleNormalState()
    {
        // Handle attack cooldown.
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            BossState nextAttack = ChooseNextAttack();
            lastAttackUsed = nextAttack; // Save the chosen attack to avoid repeating it.
            switch (nextAttack)
            {
                case BossState.ExplosiveAttack:
                    TriggerExplosiveAttack();
                    break;
                case BossState.CircleAttack:
                    TriggerCircleAttack();
                    break;
                case BossState.RandomShooting:
                    TriggerRandomShooting();
                    break;
            }
            attackTimer = attackCooldown;
        }
    }
    
    // --- Explosive attack state behavior ---
    void HandleExplosiveAttackState()
    {
        // Spawn the explosive circle prefab at the boss's location.
        Instantiate(explosiveCirclePrefab, transform.position, Quaternion.identity);
        currentState = (currentHealth <= maxHealth / 2) ? BossState.Enraged : BossState.Normal;
    }
    
    // --- Enraged state behavior ---
    void HandleEnragedState()
    {
        float enragedMoveSpeed = 1.5f;       // Faster movement when enraged.
        float enragedAttackCooldown = 3f;    // Faster attack rate when enraged.
        
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            BossState nextAttack = ChooseNextAttack();
            lastAttackUsed = nextAttack; // Save the chosen attack to avoid repetition.
            switch (nextAttack)
            {
                case BossState.ExplosiveAttack:
                    TriggerExplosiveAttack();
                    break;
                case BossState.CircleAttack:
                    TriggerCircleAttack();
                    break;
                case BossState.RandomShooting:
                    TriggerRandomShooting();
                    break;
            }
            attackTimer = enragedAttackCooldown;
        }
    }
    
    // --- Trigger the circle attack state ---
    void TriggerCircleAttack()
    {
        currentState = BossState.CircleAttack;
        //StartCoroutine(CircleAttackRoutine());
        CircleAttackRoutine();
    }
    
    // Coroutine to manage the circle attack duration.
    void CircleAttackRoutine()
    {
        for (int i = 0; i <= amountOfCirclePrefabs; i++)
        {
            //.x -20 to 20 and .y
            float randomXPosition = Random.Range(-20, 20);
            float randomYPosition = Random.Range(-10, 10);
            GameObject circleAttack = Instantiate(circleAttackPrefab, new Vector3(randomXPosition, randomYPosition, 0), Quaternion.identity);
            CircleAttackController controller = circleAttack.GetComponent<CircleAttackController>();
            if (controller != null)
            {
                controller.fireRate = Random.Range(minFireRate, maxFireRate);
                controller.bulletSpeed = Random.Range(minBulletSpeed, maxBulletSpeed);
            }
            //yield return new WaitForSeconds(controller.rotationDuration);
        }

        
        // Wait until the circle attack completes its full rotation.
        currentState = (currentHealth <= maxHealth / 2) ? BossState.Enraged : BossState.Normal;
    }
    
    // --- Trigger the random shooting state ---
    void TriggerRandomShooting()
    {
        currentState = BossState.RandomShooting;
        StartCoroutine(RandomShootingRoutine());
    }
    
    // Coroutine for the random shooting state.
    IEnumerator RandomShootingRoutine()
    {
        // Fire the configured number of projectiles in random directions.
        for (int i = 0; i < randomProjectileCount; i++)
        {
            FireRandomProjectile();
            yield return new WaitForSeconds(0.1f);
        }
        // Optionally, wait a short period for effect.
        yield return new WaitForSeconds(1.5f);
        currentState = (currentHealth <= maxHealth / 2) ? BossState.Enraged : BossState.Normal;
    }
    
    // Spawns a single projectile in a random direction.
    void FireRandomProjectile()
    {
        if (BulletPrefab != null)
        {
            // Determine a random angle in degrees.
            float angle = Random.Range(0f, 360f);
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            
            GameObject projectile = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float speed = Random.Range(minRandomBulletSpeed, maxRandomBulletSpeed);
                rb.linearVelocity = direction * speed;
            }
        }
    }
    
    // --- State Transitions ---
    void EnterEnragedState()
    {
        Debug.Log("Boss is now enraged!");
        currentState = BossState.Enraged;
    }
    
    void TriggerExplosiveAttack()
    {
        currentState = BossState.ExplosiveAttack;
    }
    
    // --- Damage Handling ---


    
    // --- Helper Method ---
    // Chooses a random attack (from ExplosiveAttack, CircleAttack, RandomShooting) that is not the same as the last used attack.
    BossState ChooseNextAttack()
    {
        List<BossState> possibleAttacks = new List<BossState>()
        {
            BossState.ExplosiveAttack,
            BossState.CircleAttack,
            BossState.RandomShooting
        };
        
        // Remove the last used attack if it is one of the attack types.
        if (possibleAttacks.Contains(lastAttackUsed))
        {
            possibleAttacks.Remove(lastAttackUsed);
        }
        
        int index = Random.Range(0, possibleAttacks.Count);
        return possibleAttacks[index];
    }
}
