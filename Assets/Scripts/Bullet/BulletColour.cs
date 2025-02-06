using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletColour : MonoBehaviour
{
    public TurretConfig turretConfig; // Reference to the turret configuration for bullet lifetime
    private SpriteRenderer spriteRenderer; // Reference to the bullet's SpriteRenderer
    private float lifeTime; // Tracks the bullet's total lifetime
    private float fadeDuration; // Duration for fading

    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing on the bullet!");
        }
    }

    void Start()
    {
        lifeTime = turretConfig.bulletLifeTime;
        fadeDuration = lifeTime;
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;

        float alpha = Mathf.Clamp01(lifeTime / fadeDuration);

        if (spriteRenderer != null)
        {
            Color currentColor = spriteRenderer.color;
            currentColor.a = alpha; // Adjust alpha channel
            spriteRenderer.color = currentColor;
        }

        // Destroy bullet when lifetime is over
        if (lifeTime <= 0)
        {
            SafeDestroy();
        }
        if (lifeTime <= 1.6f)
        {
            //Turn off box colider?
        }
    }
    
    private void SafeDestroy()
    {
        if (this != null && gameObject != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Attempted to destroy a GameObject that is already null or destroyed!");
        }
    }

}