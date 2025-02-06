using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerHealthScriptabeObject playerHealthScriptabeObject;
    [SerializeField] private PlayerGoldScriptableObject playerGoldScriptableObject;

    [SerializeField] private int CoinGiveGoldAmount = 5;
    [SerializeField] private float iframeDuration = 1f; // Duration of invincibility frames
    [SerializeField] private float transparencyLevel = 0.5f; // How transparent the player becomes

    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>(); // Get SpriteRenderer from Player object
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found! Make sure the PlayerCollision is a child of Player.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet") && !isInvincible)
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage(1);
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            playerGoldScriptableObject.AddGold(CoinGiveGoldAmount);
        }
    }

    private void TakeDamage(int amount)
    {
        playerHealthScriptabeObject.TakeDamage(amount);
        StartCoroutine(ActivateIframes());
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
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = alpha; // Change alpha value for transparency
            spriteRenderer.color = color;
        }
    }
}
