using UnityEngine;

public class SpawnEnemyReversePortal : BaseOnDeathEffect
{
    [SerializeField] private GameObject portal;
    public override void TriggerEffect()
    {
        Instantiate(portal, transform.position, Quaternion.identity);
    }
}
