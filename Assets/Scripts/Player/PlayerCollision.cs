using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private RoundsScriptableObject roundsScriptableObject;
    [SerializeField] private PlayerHealthScriptabeObject playerHealthScriptabeObject;
    [SerializeField] private PlayerGoldScriptableObject playerGoldScriptableObject;

    [SerializeField] private int CoinGiveGoldAmount = 5;
    [SerializeField] private float iframeDuration = 1f; // Duration of invincibility frames
    [SerializeField] private float transparencyLevel = 0.5f;

    private PlayerHealthSpriteSheet playerHealthSpriteSheet;
    private int _currentRoundIndex;
    private List<RoundsScriptableObject> rounds;
    [SerializeField] private SpawnEnemies spawnEnemies;
    
    [SerializeField] private GameModeManager gameModeManager;
    
    private bool isInvincible = false;
    
    
    [SerializeField] private GameObject floatingTextPrefab;

    
    private ScreenFlashOnDamage screenFlashOnDamage;
    
    [Header("Player Sprites")]
    [SerializeField] private List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    private void Awake()
    {
        screenFlashOnDamage = GetComponent<ScreenFlashOnDamage>(); 
        
        playerHealthSpriteSheet = GetComponent<PlayerHealthSpriteSheet>();
        
        gameModeManager = GameObject.FindGameObjectWithTag("GameModeManager").GetComponent<GameModeManager>();
        
        //So we can get the amount of gold for the enemies to drop. 
        rounds = GameObject.FindGameObjectWithTag("StateManager").GetComponent<SpawnEnemies>().roundsScriptableObject;
        //_currentRoundIndex = GameObject.FindGameObjectWithTag("StateManager").GetComponent<RoundStateManager>().currentRound;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if(isInvic) return;
        if (other.gameObject.CompareTag("Bullet") && !isInvincible)
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage(1);
        }
        
        if (other.gameObject.CompareTag("EnemyBullet") && !isInvincible)
        {
            TakeDamage(1);
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            int amount = spawnEnemies.roundsScriptableObject[spawnEnemies.currentRound].amountOfGoldGainedForEachCoin;
            
            
            AudioManager.instance.PlayerCollectCoinSFX();
            
            if (gameModeManager.CurrentMode == GameMode.HalfCash)
            {
                playerGoldScriptableObject.AddGold(Mathf.RoundToInt(amount / 2));
            }
            else
            {
                playerGoldScriptableObject.AddGold(amount);
            }
        }
        
        if (other.gameObject.CompareTag("Heart"))
        {
            if (playerHealthScriptabeObject.currentHealth < 10)
            {
                playerHealthScriptabeObject.currentHealth += 1;
                playerHealthSpriteSheet.ChangePlayerSprite();
            }
        }
    }

    public void TakeDamage(int amount)
    {
        AudioManager.instance.PlayerHurtSFX();
        
        screenFlashOnDamage.TakeDamage();
        
        playerHealthScriptabeObject.TakeDamage(amount);
        
        playerHealthSpriteSheet.ChangePlayerSprite();
        
        //ShowFloatingText(floatingTextPrefab);
        
        StartCoroutine(ActivateIframes());
    }
    
    void ShowFloatingText(GameObject textObject)
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject floatingText = Instantiate(textObject, canvas.transform);

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        floatingText.transform.position = screenPosition;
    }


    private IEnumerator ActivateIframes()
    {
        isInvincible = true;
        float elapsedTime = 0f;

        while (elapsedTime < iframeDuration)
        {
            SetTransparency(transparencyLevel); // Make player semi-transparent
            yield return new WaitForSeconds(0.2f);
            SetTransparency(1f); // Restore full visibility
            yield return new WaitForSeconds(0.2f);
            elapsedTime += 0.4f;
        }

        SetTransparency(1f); // Ensure full visibility at the end
        isInvincible = false;
    }

    private void SetTransparency(float alpha)
    {
        foreach (var spriteRenderer in sprites)
        {
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = alpha;
                spriteRenderer.color = color;
            }
        }
    }

}
