using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceExplosionZone : MonoBehaviour
{
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float defaultStartingAlpha;

    [SerializeField] private float fadeDuration;
    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Set initial alpha to 139 / 255
            Color color = spriteRenderer.color;
            color.a = defaultStartingAlpha / 255f;
            spriteRenderer.color = color;
        }
    }

    private void Start()
    {
        StartCoroutine(FadeAndDestroyAfterDuration());
    }
    public void IceOnDeathEffect(GameObject gameObjectToApplyEffectToo, float effect)
    {
        ApplySpeedToTag(gameObjectToApplyEffectToo, effect);
    }

    private void ApplySpeedToTag(GameObject target, float multiplier)
    {
        string tag = target.tag;

        switch (tag)
        {
            case "Turret":
                TurretStats turret = target.GetComponent<TurretStats>();
                if (turret != null)
                {
                    turret.modifierFireRate *= multiplier;
                }
                break;

            case "Enemy":
                EnemyFollowPath enemyMovement = target.GetComponent<EnemyFollowPath>();
                if (enemyMovement != null)
                {
                    enemyMovement.moveSpeed *= multiplier;
                }
                break;

            case "PlayerCollision":
                PlayerMovement player = target.GetComponentInParent<PlayerMovement>();
                if (player != null)
                {
                    player.moveSpeed *= multiplier;
                }
                break;
            
            case "Bullet":
                BasicBullet basicBullet = target.GetComponent<BasicBullet>();
                if (basicBullet != null)
                {
                    basicBullet.speed *= multiplier;
                }
                break;
            
            case "PlayerBullet":
                PlayerBullet playerBullet = target.GetComponent<PlayerBullet>();
                if (playerBullet != null)
                {
                    playerBullet.speed *= multiplier;
                }
                break;

            default:
                Debug.Log("Ice effect applied to unknown tag: " + tag);
                break;
        }
    }

    
    private IEnumerator FadeAndDestroyAfterDuration()
    {
        yield return new WaitForSeconds(duration);

        if (spriteRenderer != null)
        {
            float startAlpha = 139f / 255f;
            float elapsed = 0f;
            Color color = spriteRenderer.color;

            while (elapsed < fadeDuration)
            {
                float alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeDuration);
                spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
                elapsed += Time.deltaTime;
                yield return null;
            }

            spriteRenderer.color = new Color(color.r, color.g, color.b, 0f);
        }

        Destroy(gameObject);
    }
}