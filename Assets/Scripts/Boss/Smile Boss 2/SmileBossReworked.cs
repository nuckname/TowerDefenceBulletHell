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
    RandomShooting,
    BeamAttack
}

public class SmileBossReworked : MonoBehaviour
{
    // --- Basic Stats ---
    [Header("Health")]
    private float maxHealth = 1000f;
    private float currentHealth;
    
    [Header("Basic States")]
    public float moveSpeed = 1f;
    public float attackCooldown = 5f;

    // --- Beam Attack ---
    [Header("Beam Attack")]
    public GameObject beamPrefab;
    public float beamDuration = 5f;
    public int beamCount = 4; 

    // --- Explosion Attack Stats ---
    [Header("Red Circle Attack")]
    public GameObject explosiveCirclePrefab;

    [Header("Circle Attack States")]
    public GameObject circleAttackPrefab;
    public float minFireRate = 0.2f;
    public float maxFireRate = 0.8f;
    public float minBulletSpeed = 3f;
    public float maxBulletSpeed = 6f;
    public int amountOfCirclePrefabs = 20;

    [Header("Random Shooting stats")]
    [SerializeField] private GameObject BulletPrefab;
    public float minRandomBulletSpeed = 2f;
    public float maxRandomBulletSpeed = 5f;
    public int randomProjectileCount = 10;

    [Header("Current State")]
    [SerializeField] private BossState currentState;
    private float attackTimer;
    private BossState lastAttackUsed = BossState.Normal;

    void Start()
    {
        currentHealth = maxHealth;
        currentState = BossState.Normal;
        attackTimer = attackCooldown;
    }
    
    void Update()
    {
        if (currentHealth <= maxHealth / 2 && currentState != BossState.Enraged)
        {
            //EnterEnragedState();
        }
        
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
        }
    }
    
    void HandleNormalState()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            BossState nextAttack = ChooseNextAttack();
            lastAttackUsed = nextAttack;
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
                
                /*
                case BossState.BeamAttack:
                    TriggerBeamAttack();
                    break;
                */
            }
            attackTimer = attackCooldown;
        }
    }

    void TriggerBeamAttack()
    {
        currentState = BossState.BeamAttack;
        StartCoroutine(BeamAttackRoutine());
    }

    private GameObject Beam;
    IEnumerator BeamAttackRoutine()
    {
        float angleStep = 360f / beamCount; // Spread beams evenly
        for (int i = 0; i < beamCount; i++)
        {
            float angle = i * angleStep;
            Beam = FireBeam(angle);
        }
        yield return new WaitForSeconds(beamDuration);
        currentState = (currentHealth <= maxHealth / 2) ? BossState.Enraged : BossState.Normal;
        
        Destroy(Beam, beamDuration);
    }

    GameObject FireBeam(float angle)
    {
        Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
    
         GameObject beamInstance = Instantiate(beamPrefab, transform.position, rotation);
         return beamInstance;
    
    }

    
    void HandleExplosiveAttackState()
    {
        Instantiate(explosiveCirclePrefab, transform.position, Quaternion.identity);
        currentState = (currentHealth <= maxHealth / 2) ? BossState.Enraged : BossState.Normal;
    }
    
    void HandleEnragedState()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            BossState nextAttack = ChooseNextAttack();
            lastAttackUsed = nextAttack;
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
                case BossState.BeamAttack:
                    TriggerBeamAttack();
                    break;
             
            }
            attackTimer = 3f;
        }
    }
    
    void TriggerExplosiveAttack()
    {
        currentState = BossState.ExplosiveAttack;
    }
    
    void TriggerCircleAttack()
    {
        currentState = BossState.CircleAttack;
        CircleAttackRoutine();
    }
    
    void CircleAttackRoutine()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); 
        Vector3 playerPosition = player.transform.position;
        float minDistanceFromPlayer = 10f;

        for (int i = 0; i <= amountOfCirclePrefabs; i++)
        {
            Vector3 spawnPosition;
            do
            {
                float randomXPosition = Random.Range(-20, 20);
                float randomYPosition = Random.Range(-10, 10);
                spawnPosition = new Vector3(randomXPosition, randomYPosition, 0);
            }
            while (Vector3.Distance(spawnPosition, playerPosition) < minDistanceFromPlayer);

            GameObject circleAttack = Instantiate(circleAttackPrefab, spawnPosition, Quaternion.identity);
            CircleAttackController controller = circleAttack.GetComponent<CircleAttackController>();
            if (controller != null)
            {
                controller.fireRate = Random.Range(minFireRate, maxFireRate);
                controller.bulletSpeed = Random.Range(minBulletSpeed, maxBulletSpeed);
            }
        }

        // Wait until the circle attack completes its full rotation.
        currentState = (currentHealth <= maxHealth / 2) ? BossState.Enraged : BossState.Normal;
    }

    
    void TriggerRandomShooting()
    {
        currentState = BossState.RandomShooting;
        StartCoroutine(RandomShootingRoutine());
    }
    
    IEnumerator RandomShootingRoutine()
    {
        for (int i = 0; i < randomProjectileCount; i++)
        {
            FireRandomProjectile();
        }
        yield return new WaitForSeconds(1.5f);
        currentState = (currentHealth <= maxHealth / 2) ? BossState.Enraged : BossState.Normal;
    }
    
    void FireRandomProjectile()
    {
        if (BulletPrefab != null)
        {
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
    
    BossState ChooseNextAttack()
    {
        List<BossState> possibleAttacks = new List<BossState>()
        {
            BossState.ExplosiveAttack,
            BossState.CircleAttack,
            BossState.RandomShooting,
            BossState.BeamAttack
        };
        
        if (possibleAttacks.Contains(lastAttackUsed))
        {
            possibleAttacks.Remove(lastAttackUsed);
        }
        
        return possibleAttacks[Random.Range(0, possibleAttacks.Count)];
    }
}
