using System.Collections.Generic;
using UnityEngine;

public class EnemyPaintTrail : SpawnEffectOnEnemyGroundPosition
{
    private static readonly List<GameObject> _paintEffects = new();
    protected override float ZOffset => 1f;
    protected override List<GameObject> SpawnedEffects => _paintEffects;
    public static void DestroyAllPaint() => _paintEffects.ForEach(Destroy);
}
