using System;
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

    private EnemyShieldCollision _enemyShieldCollision;

    private void Awake()
    {
        _enemyShieldCollision = GetComponentInChildren<EnemyShieldCollision>();
    }

    public void ConfigureShields(EnemyGroup group)
    {
        ConfigureShield(group.north, shieldNorth, group.shieldHp);
        ConfigureShield(group.south, shieldSouth, group.shieldHp);
        ConfigureShield(group.east, shieldEast, group.shieldHp);
        ConfigureShield(group.west, shieldWest, group.shieldHp);
        ConfigureShield(group.northEast, shieldNorthEast, group.shieldHp);
        ConfigureShield(group.northWest, shieldNorthWest, group.shieldHp);
        ConfigureShield(group.southEast, shieldSouthEast, group.shieldHp);
        ConfigureShield(group.southWest, shieldSouthWest, group.shieldHp);
    }

    private void ConfigureShield(bool isActive, GameObject shield, int shieldHp)
    {
        shield.SetActive(isActive);
    
        if (isActive && shield.TryGetComponent(out EnemyShieldCollision shieldCollision))
        {
            shieldCollision.shieldHealth = shieldHp;
        }
    }
}