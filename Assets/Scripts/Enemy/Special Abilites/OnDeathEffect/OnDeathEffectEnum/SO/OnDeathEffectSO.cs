using UnityEngine;

[CreateAssetMenu(menuName = "OnDeathEffect/Settings", fileName = "OnDeathEffectZoneSettings")]
public class OnDeathEffectSO : ScriptableObject
{
    public string zoneTag = "Explosion";
    public float duration = 0.5f;
    public float fadeDuration = 1f;
    [Range(0, 255)] public float startingAlpha = 139f;
    public Color color = Color.white;
}
