using System;
using System.Collections.Generic;
using UnityEngine;

public class setEnemyShield : MonoBehaviour
{
    [Header("Shield N,E,S,W")]
    [SerializeField] private GameObject shieldNorth;
    [SerializeField] private GameObject shieldSouth;
    [SerializeField] private GameObject shieldEast;
    [SerializeField] private GameObject shieldWest;

    [Header("Shield Diagonal")]
    [SerializeField] private GameObject shieldNorthEast;
    [SerializeField] private GameObject shieldNorthWest;
    [SerializeField] private GameObject shieldSouthEast;
    [SerializeField] private GameObject shieldSouthWest;

    // Optional: If you want a lookup dictionary for easy mapping
    private Dictionary<ShieldDirectionType, GameObject> shieldMap;

    private void Awake()
    {
        shieldMap = new Dictionary<ShieldDirectionType, GameObject>()
        {
            { ShieldDirectionType.North, shieldNorth },
            { ShieldDirectionType.South, shieldSouth },
            { ShieldDirectionType.East, shieldEast },
            { ShieldDirectionType.West, shieldWest },
            { ShieldDirectionType.NorthEast, shieldNorthEast },
            { ShieldDirectionType.NorthWest, shieldNorthWest },
            { ShieldDirectionType.SouthEast, shieldSouthEast },
            { ShieldDirectionType.SouthWest, shieldSouthWest }
        };
    }

    public void ConfigureShields(List<ShieldDirectionType> activeDirections, int shieldHp)
    {
        //activate only the shields in the list
        foreach (var direction in activeDirections)
        {
            if (shieldMap.TryGetValue(direction, out GameObject shield))
            {
                shield.SetActive(true);
                if (shield.TryGetComponent(out EnemyShieldCollision shieldCollision))
                {
                    shieldCollision.shieldHealth = shieldHp;
                }
            }
            else
            {
                Debug.LogWarning($"No shield GameObject found for direction {direction}");
            }
        }
    }
}
