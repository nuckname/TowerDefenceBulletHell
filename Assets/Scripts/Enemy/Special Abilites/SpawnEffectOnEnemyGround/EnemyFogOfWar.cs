using System.Collections.Generic;
using UnityEngine;

public class EnemyFogOfWar : SpawnEffectOnEnemyGroundPosition
{
    private static readonly List<GameObject> _fogEffects = new();
    protected override float ZOffset => -1f;
    protected override List<GameObject> SpawnedEffects => _fogEffects;

    public static void DestroyAllFog() => _fogEffects.ForEach(Destroy);
}
