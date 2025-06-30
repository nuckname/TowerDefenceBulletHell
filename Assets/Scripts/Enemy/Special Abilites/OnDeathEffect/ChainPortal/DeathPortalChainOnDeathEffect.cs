using UnityEngine;

public class DeathPortalChainOnDeathEffect : BaseOnDeathEffect
{
    [Header("Prefabs & Settings")]
    [Tooltip("Portal prefab must have a TeleportPortal component on its root")]
    public GameObject portalPrefab;

    [Tooltip("Enemy prefab to respawn (usually same as original)")]
    public GameObject enemyPrefab;

    [Range(0f, 1f)]
    [Tooltip("Alpha for the respawned 'ghost'")]
    public float respawnAlpha = 0.5f;

    public int enemyRespawnHp;

    [HideInInspector]
    public bool firstTime = true;

    private SpawnEnemies spawnEnemies;
    
    public int AmountOfRoundsBeforeDestroy; 

    private void Awake()
    {
        GameObject stateManager = GameObject.FindGameObjectWithTag("StateManager");
        if (stateManager != null)
        {
            spawnEnemies = stateManager.GetComponent<SpawnEnemies>();
            if (spawnEnemies == null)
            {
                Debug.LogError("SpawnEnemies component not found on StateManager.");
            }
        }
        else
        {
            Debug.LogError("No GameObject with tag 'StateManager' found in the scene.");
        }
    }

    public override void TriggerEffect()
    {
        Vector3 deathPosition = transform.position;

        print("death pos: " + deathPosition);
        if (firstTime)
        {
            CreatePortal(deathPosition, true);

            GameObject newEnemy = Instantiate(enemyPrefab, deathPosition, Quaternion.identity);
            ConfigureRespawnedEnemy(newEnemy, deathPosition);

            firstTime = false;
        }
        else
        {
            CreatePortal(deathPosition, false);
        }
    }

    private void CreatePortal(Vector3 position, bool isSender)
    {
        GameObject portal = Instantiate(portalPrefab, position, Quaternion.identity);
        TeleporterHealthCounter teleporterHealthCounter = portal.GetComponent<TeleporterHealthCounter>();
        
        teleporterHealthCounter.maxTpLength = AmountOfRoundsBeforeDestroy;
        
        if (teleporterHealthCounter == null)
        {
            Debug.LogError("TeleportPortal component missing on portalPrefab.");
            return;
        }

        if (isSender)
        {
            portal.tag = "teleportSender";
        }
        else
        {
            portal.tag = "teleportReceiver";
        }
    }

    private void ConfigureRespawnedEnemy(GameObject enemy, Vector3 deathPosition)
    {
        // Setup portal chain flag on new enemy
        DeathPortalChainOnDeathEffect portalEffect = enemy.GetComponentInChildren<DeathPortalChainOnDeathEffect>();

        print("Setting firstTime portal is this correct?");
        if (portalEffect != null)
        {
            portalEffect.firstTime = false;
        }
        else
        {
            Debug.LogWarning("Respawned enemy is missing DeathPortalChainOnDeathEffect.");
        }

        SetUpEnemyPath(enemy, deathPosition);

        SetUpEnemyHp(enemy);

        FadeOutSprite(enemy);
    }

    private void SetUpEnemyPath(GameObject enemy, Vector3 deathPosition)
    {
        EnemyFollowPath enemyPath = enemy.GetComponent<EnemyFollowPath>();
        if (enemyPath != null)
        {
            // Skip initial positioning in Start() method
            enemyPath.skipInitialPositioning = true;
            
            // Set the waypoint index based on the death position
            enemyPath.SetWaypointIndexFromPosition(deathPosition);
            
            // Override the position to ensure it spawns exactly at death position
            enemy.transform.position = deathPosition;
        }
        else
        {
            Debug.LogWarning("Respawned enemy is missing EnemyFollowPath component.");
        }
    }

    private void SetUpEnemyHp(GameObject enemy)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.EnemyStartingHealth = enemyRespawnHp;
            enemyHealth.SetEnemyHealthToSprite();
        }
    }

    private void FadeOutSprite(GameObject enemy)
    {
        SpriteRenderer[] spriteRenderers = enemy.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            Color color = spriteRenderers[i].color;
            color.a = respawnAlpha;
            spriteRenderers[i].color = color;
        }
    }
}
