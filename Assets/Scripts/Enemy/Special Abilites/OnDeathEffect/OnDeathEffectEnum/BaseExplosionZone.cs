using System.Collections;
using UnityEngine;

public abstract class BaseExplosionZone : MonoBehaviour
{
    [Header("Zone Fade Settings")]
    [SerializeField] protected float duration = 0.5f;
    [SerializeField] protected float defaultStartingAlpha = 139f;
    [SerializeField] protected float fadeDuration = 1f;
    
    protected SpriteRenderer spriteRenderer;
    
    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            SetInitialAlpha();
        }
    }

    protected virtual void Start()
    {
        StartCoroutine(FadeAndDestroyAfterDuration());
    }
    
    protected virtual void SetInitialAlpha()
    {
        Color color = spriteRenderer.color;
        color.a = defaultStartingAlpha / 255f;
        spriteRenderer.color = color;
    }
    
    protected virtual IEnumerator FadeAndDestroyAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        
        if (spriteRenderer != null)
        {
            yield return StartCoroutine(FadeOut());
        }
        
        Destroy(gameObject);
    }
    
    protected virtual IEnumerator FadeOut()
    {
        float startAlpha = defaultStartingAlpha / 255f;
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
}