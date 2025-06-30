using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To find usage go to ISpeedModifiable
public class IceExplosionZone : BaseExplosionZone
{
    protected override string ZoneTag
    {
        get
        {
            return "IceOnDeathEffect";
        }
    }
    
    //Overrides the Base values
    protected override void Awake()
    {
        duration = 0.5f;
        fadeDuration = 1f;
        defaultStartingAlpha = 139f;

        base.Awake();
    }
    
    protected override void SetInitialAlpha()
    {
        if (spriteRenderer != null)
        {
            Color iceBlue = new Color(0.5f, 0.8f, 1f); // Light icy blue
            iceBlue.a = defaultStartingAlpha / 255f;
            spriteRenderer.color = iceBlue;
        }
    }

}