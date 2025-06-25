using UnityEngine;

public class IceExplosionEffectOnDeathEffect : BaseOnDeathEffect
{
    [SerializeField] private GameObject iceExplosionPrefab;

    public override void TriggerEffect()
    {
        Instantiate(iceExplosionPrefab, transform.position, Quaternion.identity);
    }
    
    //Have sprite stuff here?
}