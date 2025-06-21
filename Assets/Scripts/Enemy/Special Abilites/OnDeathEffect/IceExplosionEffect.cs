using UnityEngine;

public class IceExplosionEffect : BaseOnDeathEffect
{
    [SerializeField] private GameObject iceExplosionPrefab;

    public override void TriggerEffect()
    {
        Instantiate(iceExplosionPrefab, transform.position, Quaternion.identity);
    }
}