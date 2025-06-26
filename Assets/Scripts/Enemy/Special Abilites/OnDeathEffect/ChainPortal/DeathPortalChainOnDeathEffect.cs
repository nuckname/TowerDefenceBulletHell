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

        if (firstTime)
        {
            CreatePortal(deathPosition, true);

            GameObject newEnemy = Instantiate(enemyPrefab, deathPosition, Quaternion.identity);
            ConfigureRespawnedEnemy(newEnemy);

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
        TeleportPortal teleportPortal = portal.GetComponent<TeleportPortal>();

        if (teleportPortal == null)
        {
            Debug.LogError("TeleportPortal component missing on portalPrefab.");
            return;
        }

        if (isSender)
        {
            teleportPortal.isTeleportSender = true;
            portal.tag = "teleportSender";
        }
        else
        {
            teleportPortal.isTeleportReceiver = true;
            portal.tag = "teleportReceiver";
        }
    }

    private void ConfigureRespawnedEnemy(GameObject enemy)
    {
        // Setup portal chain flag on new enemy
        DeathPortalChainOnDeathEffect portalEffect = enemy.GetComponentInChildren<DeathPortalChainOnDeathEffect>();
        if (portalEffect != null)
        {
            portalEffect.firstTime = false;
        }
        else
        {
            Debug.LogWarning("Respawned enemy is missing DeathPortalChainOnDeathEffect.");
        }

        // Setup waypoint index from the previous enemy
        if (spawnEnemies != null && spawnEnemies.enemyHasPortal != null)
        {
            EnemyFollowPath previousPath = spawnEnemies.enemyHasPortal.GetComponent<EnemyFollowPath>();
            EnemyFollowPath newPath = enemy.GetComponent<EnemyFollowPath>();

            if (previousPath != null && newPath != null)
            {
                newPath.waypointIndex = previousPath.waypointIndex + 1;
            }
        }
        else
        {
            Debug.LogWarning("Failed to assign waypoint index. enemyHasPortal or SpawnEnemies missing.");
        }

        // Set new enemy HP
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.EnemyStartingHealth = enemyRespawnHp;
            enemyHealth.SetEnemyHealthToSprite();
        }

        // Fade out new enemy sprites
        SpriteRenderer[] spriteRenderers = enemy.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            Color color = spriteRenderers[i].color;
            color.a = respawnAlpha;
            spriteRenderers[i].color = color;
        }
    }
}
