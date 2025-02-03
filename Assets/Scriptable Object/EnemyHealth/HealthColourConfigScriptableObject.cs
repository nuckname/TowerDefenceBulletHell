using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthColorConfig", menuName = "Enemy/Health Color Config")]
public class HealthColourConfigScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class HealthColorPair
    {
        public Color color;
        [ReadOnly] public int healthThreshold; // Auto-set in OnValidate
    }

    [Tooltip("Colors ordered from lowest (1) to highest health threshold")]
    public List<HealthColorPair> healthColorPairs = new List<HealthColorPair>();
    public Color defaultColor = Color.grey;

    // Auto-assign sequential thresholds whenever edited in Inspector
    private void OnValidate()
    {
        for (int i = 0; i < healthColorPairs.Count; i++)
        {
            healthColorPairs[i].healthThreshold = i + 1;
        }
    }
}
