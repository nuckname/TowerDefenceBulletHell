using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    
    [SerializeField] private Transform spawnPoint; // Where enemies will spawn
    public List<RoundsScriptableObject> roundsScriptableObject; // List of scriptable objects for each round

    private bool isDoubleHP = false;

    [SerializeField] private GameObject _onDeathEffectVisual;
    
    private GameModeManager gameModeManager;
    
    public int currentRound;

    public List<OnDeathEffectType> onDeathEffects;
    private void Start()
    {
        //Call this only once somehow. 
        gameModeManager = GameObject.FindGameObjectWithTag("GameModeManager").GetComponent<GameModeManager>();
    }

    private int _freePlayTotalEnemies = 0;
    public int SpawnEnemiesPerRound(int currentRoundIndex)
    {
        if (gameModeManager.CurrentMode == GameMode.Tutorial)
        {
            currentRound++;
            //_freePlayTotalEnemies = FreePlay();
            

            return _freePlayTotalEnemies;
        }
        //used in enemy prefab
        currentRound = currentRoundIndex;
        
        if (gameModeManager.CurrentMode == GameMode.DoubleHP)
        {
            isDoubleHP = true;
        }
        else
        {
            isDoubleHP = false;
        }
        
        if (roundsScriptableObject[currentRoundIndex].boss != null)
        {
            Instantiate(roundsScriptableObject[currentRoundIndex].boss, spawnPoint.position, Quaternion.identity);
        }

        // Check if there are rounds remaining
        if (currentRoundIndex < roundsScriptableObject.Count)
        {
            // Get the current round's scriptable object
            RoundsScriptableObject currentRound = roundsScriptableObject[currentRoundIndex];
            int totalEnemies = currentRound.GetTotalEnemies();
            
            // Start spawning enemies for the current round
            StartCoroutine(SpawnEnemiesWithDelay(currentRound));

            currentRoundIndex++;
            print("totalEnemies: " + totalEnemies);
            
            return totalEnemies;
        }
        Debug.LogError("Returned 0 Enemies ERROR");
        return 0;
    }

    private int FreePlay(int currentRoundIndex)
    {
        int totalAmountOfEnemies = 0;
        
        
        return totalAmountOfEnemies;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SkipRound();
        }
    }

    private void SkipRound()
    {
        if (currentRound < roundsScriptableObject.Count - 1)
        {
            //Get State MAchine?
            currentRound++;
            DeleteAllEnemies();
            StopAllCoroutines(); // Stop any ongoing enemy spawns
            SpawnEnemiesPerRound(currentRound);
            Debug.Log("Skipped to Round: " + currentRound);
        }
        else
        {
            Debug.Log("No more rounds to skip!");
        }
    }
    
    private void DeleteAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }


    private IEnumerator SpawnEnemiesWithDelay(RoundsScriptableObject round)
    {
        // Iterate through each enemy group in the round
        foreach (EnemyGroup group in round.enemyGroups)
        {
            yield return new WaitForSeconds(group.delayBeforeGroup);

            for (int i = 0; i < group.count; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

                EnemyDropItems enemyDropItems = enemy.GetComponent<EnemyDropItems>();

                enemy.GetComponent<EnemyDropItems>().amountOfGoldCoinsToDrop = round.amountOfGoldToDrop;
                
                
                //enemyDropItems.amountOfGoldCoinsToDrop = round.amountOfGoldToDrop;
                //enemyDropItems.amountOfHeartsToDrop = round.amountOfHeartToDrop;
                
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    if (isDoubleHP)
                    {
                        enemyHealth.InitializeEnemy(group.enemyHp * 2); 
                    }
                    else
                    {
                        enemyHealth.InitializeEnemy(group.enemyHp); 
                    }
                }
                
                // Apply speed modifier from the round
                EnemyFollowPath enemyMovement = enemy.GetComponent<EnemyFollowPath>();
                if (enemyMovement != null)
                {
                    enemyMovement.moveSpeed += round.speedModifer;
                }

                //ENEMIES 2.0                
                if (group.shieldDirections.Count >= 1)
                {
                    AddShieldDirection(group, enemy);
                }

                if (group.groundEffects.Count >= 1)
                {
                    AddGroundEffects(group, enemy);
                }
                
                
                if (group.onDeathEffects.Count >= 1)
                {
                    AddOnDeathEffect(group, enemy);
                }
                
                yield return new WaitForSeconds(group.spawnInterval);
            }
        }

        // Wait for the wave's delay after completion
        yield return new WaitForSeconds(round.delayAfterWave);
    }
    
    private void AddShieldDirection(EnemyGroup group, GameObject enemy)
    {
        setEnemyShield setEnemyShield = enemy.GetComponent<setEnemyShield>();   
        if (setEnemyShield != null)
        {
            setEnemyShield.enabled = true;
            setEnemyShield.ConfigureShields(group.shieldDirections, group.shieldHp);
        }
    }

    private void AddGroundEffects(EnemyGroup group, GameObject enemy)
    {
        foreach (var effectType in group.groundEffects)
        {
            switch (effectType)
            {
                case GroundEffectType.FogOfWar:
                    EnemyFogOfWar enemyFogOfWar = enemy.GetComponent<EnemyFogOfWar>();   
                    enemyFogOfWar.enabled = true; 
                    break;
                case GroundEffectType.PaintSpeedEffect:
                    EnemyPaintTrail enemyPaintTrail = enemy.GetComponent<EnemyPaintTrail>();   
                    enemyPaintTrail.enabled = true; 
                    break;
            }
        }
    }

    private void AddOnDeathEffect(EnemyGroup group, GameObject enemy)
    {
        //_onDeathEffectVisual.SetActive(true);

        foreach (var effectType in group.onDeathEffects)
        {
            switch (effectType)
            {
                case OnDeathEffectType.HealNearby:
                    //enemy.AddComponent<HealNearbyOnDeathEffect>();
                    break;
                case OnDeathEffectType.Duplicate:
                    //var dup = enemy.AddComponent<DuplicateOnDeathEffect>();
                    //dup.enemyPrefab = enemyPrefab;
                    break;
                case OnDeathEffectType.StunTurrets:
                    //enemy.AddComponent<StunTurretsOnDeathEffect>();
                    break;
                case OnDeathEffectType.CreateFog:
                    //enemy.AddComponent<FogOnDeathEffect>();
                    break;
                case OnDeathEffectType.SpeedBoost:
                    //enemy.AddComponent<SpeedBoostOnDeathEffect>();
                    break;
                case OnDeathEffectType.ShadowPortal:
                    //enemy.AddComponent<ShadowPortalOnDeathEffect>();
                    break;
                case OnDeathEffectType.DeathPortalChain:
                    //enemy.AddComponent<DeathPortalChainOnDeathEffect>();
                    break;
                case OnDeathEffectType.ZombieHoming:
                    //enemy.AddComponent<ZombieHomingOnDeathEffect>();
                    break;
                case OnDeathEffectType.IceExplosion:
                    
                    IceExplosionEffect iceExplosionEffect = enemy.GetComponentInChildren<IceExplosionEffect>();
                    iceExplosionEffect.enabled = true;
                    
                    //Make blue or change sprite or something?
                    //Need a sprite reference
                    break;
                // etc…
            }
        }
    }
    
}