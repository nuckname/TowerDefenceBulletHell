using System;
using System.Collections;
using UnityEngine;

public class BaseExplosionZone : MonoBehaviour
{
    private OnDeathEffectSO _onDeathEffectSo;

    private SpriteRenderer spriteRenderer;

    public void Initialise(OnDeathEffectSO onDeathEffectSO)
    {
        if (onDeathEffectSO == null)
        {
            Debug.LogWarning($"[{name}] no OnDeathEffectSO assigned; skipping initialization.");
            return;
        }
        
        _onDeathEffectSo = onDeathEffectSO;
        
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && onDeathEffectSO != null)
        {
            SetInitialColorAndAlpha();
        }
        
        gameObject.tag = onDeathEffectSO.zoneTag;
        
        StartCoroutine(FadeAndDestroyAfterDuration());

    }

    private void SetInitialColorAndAlpha()
    {
        Color baseColor = _onDeathEffectSo.color;
        baseColor.a = _onDeathEffectSo.startingAlpha / 255f;
        spriteRenderer.color = baseColor;
    }

    private IEnumerator FadeAndDestroyAfterDuration()
    {
        yield return new WaitForSeconds(_onDeathEffectSo.duration);

        if (spriteRenderer != null)
        {
            yield return StartCoroutine(FadeOut());
        }

        Destroy(gameObject);
    }

    private IEnumerator FadeOut()
    {
        float startAlpha = _onDeathEffectSo.startingAlpha / 255f;
        float elapsed = 0f;
        Color color = spriteRenderer.color;

        while (elapsed < _onDeathEffectSo.fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, 0f, elapsed / _onDeathEffectSo.fadeDuration);
            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = new Color(color.r, color.g, color.b, 0f);
    }
}