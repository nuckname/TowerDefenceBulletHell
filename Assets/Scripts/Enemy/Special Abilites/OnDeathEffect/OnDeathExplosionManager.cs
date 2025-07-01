using System;
using UnityEngine;

public class OnDeathExplosionManager : BaseOnDeathEffect
{
    [HideInInspector] public bool hasIceExplosion = false;
    [HideInInspector] public bool hasZombieExplosion = false;

    [SerializeField] private OnDeathEffectSO iceSO;
    [SerializeField] private OnDeathEffectSO zombieSO;

    [SerializeField] private GameObject onDeathEffectPrefab;
    
    private BaseExplosionZone _baseExplosionZone;

    public override void TriggerEffect()
    {
        if (hasIceExplosion)
        {
            SetOnDeathEffect(iceSO);
        }

        if (hasZombieExplosion)
        {
            SetOnDeathEffect(zombieSO);
        }
    }

    private void SetOnDeathEffect(OnDeathEffectSO onDeathEffect)
    {
        GameObject onDeathEffectGO = Instantiate(onDeathEffectPrefab, transform.position, Quaternion.identity);
        onDeathEffectGO.GetComponent<BaseExplosionZone>().Initialise(onDeathEffect);
    }
}
