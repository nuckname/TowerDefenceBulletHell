using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceExplosionZone : MonoBehaviour
{
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float radius = 3f;
    [SerializeField] private float defaultStartingAlpha;

    
    [SerializeField] private float fadeDuration;
    private SpriteRenderer spriteRenderer;
    //Remove this?
    private List<TurretStats> affectedTurrets = new List<TurretStats>();
    
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
        if (gameObjectToApplyEffectToo.gameObject.CompareTag("Turret"))
        {
            print("enter Ondeath Trigger Enter");
            TurretStats turret = gameObjectToApplyEffectToo.GetComponent<TurretStats>();
            if (turret != null && !affectedTurrets.Contains(turret))
            {
                turret.modifierFireRate *= 0.5f;
                affectedTurrets.Add(turret);
            }
        }
        
        if (gameObjectToApplyEffectToo.CompareTag("PlayerCollision"))
        {
            print(" enter OnDeath Player");
            
            PlayerMovement player = gameObjectToApplyEffectToo.GetComponentInParent<PlayerMovement>();
            if (player != null)
            {
                player.moveSpeed *= effect;
            }
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