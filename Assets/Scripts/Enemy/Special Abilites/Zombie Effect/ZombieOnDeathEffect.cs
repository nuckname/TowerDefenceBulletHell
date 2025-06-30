using UnityEngine;

public class ZombieOnDeathEffect : BaseOnDeathEffect
{
    [SerializeField] private GameObject zombieExplosionPrefab;

    public override void TriggerEffect()
    {
        Instantiate(zombieExplosionPrefab, transform.position, Quaternion.identity);
    }
}
