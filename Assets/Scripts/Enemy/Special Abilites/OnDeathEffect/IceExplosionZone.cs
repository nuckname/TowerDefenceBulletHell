using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To find usage go to ISpeedModifiable
public class IceExplosionZone : BaseExplosionZone
{
    public void IceOnDeathEffect(GameObject gameObjectToApplyEffectToo, float effect)
    {
        ApplySpeedEffect(gameObjectToApplyEffectToo, effect);
    }

    private void ApplySpeedEffect(GameObject target, float multiplier)
    {
        ISpeedModifiable speedModifiable = target.GetComponent<ISpeedModifiable>();
        if (speedModifiable != null)
        {
            speedModifiable.ModifySpeed(multiplier);
            return;
        }
        
        // Try getting from parent if not found on the object itself
        /*
        speedModifiable = target.GetComponentInParent<ISpeedModifiable>();
        if (speedModifiable != null)
        {
            speedModifiable.ModifySpeed(multiplier);
            return;
        }
        */
        
        Debug.Log($"Ice effect could not be applied to {target.name} - no ISpeedModifiable component found");
    }
}