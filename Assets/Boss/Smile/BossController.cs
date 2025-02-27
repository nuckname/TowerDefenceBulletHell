using System.Collections;
using UnityEngine;

public enum BossState
{
    Idle,       // Normal movement and shooting
    JumpAttack, // Jumps and spawns projectiles on landing
    RandomShot, // Fires projectiles in random directions
    Stagger     // Takes extra damage at HP thresholds
}

public class BossController : MonoBehaviour
{
    public BossState currentState;
    
    [Header("Boss Attributes")]
    public float speed = 2f;
    public int maxHealth = 1000;
    private int currentHealth;
    private bool isStaggering = false;

    [Header("Jump Attack")]
    public float jumpHeight = 0.5f;
    public float jumpTime = 1f;
    public GameObject projectilePrefab;
    
    [Header("Shooting")]
    public float shootCooldown = 1.5f;
    public int projectileCount = 4;
    
    private float nextActionTime;
    
    private void Start()
    {
        currentHealth = maxHealth;
        ChangeState(BossState.Idle);
    }

    private void Update()
    {
        if (Time.time > nextActionTime && !isStaggering)
        {
            ChooseNextState();
            nextActionTime = Time.time + shootCooldown;
        }
    }

    private void ChooseNextState()
    {
        if (currentHealth <= maxHealth * 0.66f && currentHealth > maxHealth * 0.33f)
        {
            StartCoroutine(StaggerState());
            return;
        }
        else if (currentHealth <= maxHealth * 0.33f)
        {
            StartCoroutine(StaggerState());
            return;
        }

        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0: ChangeState(BossState.JumpAttack); break;
            case 1: ChangeState(BossState.RandomShot); break;
            case 2: ChangeState(BossState.Idle); break;
        }
    }

    private void ChangeState(BossState newState)
    {
        currentState = newState;
        StopAllCoroutines(); // Stop previous actions

        switch (currentState)
        {
            case BossState.Idle:
                StartCoroutine(IdleState());
                break;
            case BossState.JumpAttack:
                StartCoroutine(JumpAttackState());
                break;
            case BossState.RandomShot:
                StartCoroutine(RandomShotState());
                break;
        }
    }

    private IEnumerator IdleState()
    {
        while (currentState == BossState.Idle)
        {
            // Move in random directions
            Vector2 moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            transform.Translate(moveDir * speed * Time.deltaTime);

            yield return new WaitForSeconds(0.5f); // Change movement direction every 0.5 seconds
            ShootProjectiles(1);
        }
    }

    private IEnumerator JumpAttackState()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 jumpScale = originalScale * 0.5f;

        transform.localScale = jumpScale; // Shrink (Jump)
        yield return new WaitForSeconds(jumpTime);

        transform.localScale = originalScale; // Expand (Land)
        SpawnProjectiles(8);
        
        ChangeState(BossState.Idle);
    }

    private IEnumerator RandomShotState()
    {
        //ShootProjectiles(4);
        yield return new WaitForSeconds(1f);
        ChangeState(BossState.Idle);
    }

    private IEnumerator StaggerState()
    {
        isStaggering = true;
        currentState = BossState.Stagger;
        
        Debug.Log("Boss is staggering! Free damage window!");

        yield return new WaitForSeconds(3f); // Stay vulnerable for 2 seconds
        isStaggering = false;

        ChangeState(BossState.Idle);
    }

    private void ShootProjectiles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float angle = Random.Range(0f, 360f);
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            SpawnProjectile(direction);
        }
    }

    private void SpawnProjectiles(int count)
    {
        float angleStep = 360f / count;
        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            SpawnProjectile(direction);
        }
    }

    private void SpawnProjectile(Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * 3f; // Adjust speed
        //Destroy(projectile, 3f);
    }

    public void TakeDamage(int damage)
    {
        if (isStaggering) damage *= 2; // Double damage in stagger state

        currentHealth -= damage;
        if (currentHealth <= 0) Destroy(gameObject);
    }
}
