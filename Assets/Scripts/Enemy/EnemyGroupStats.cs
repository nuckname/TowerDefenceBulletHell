using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyGroupStats
{
    [Tooltip("Determines what kind of enemy to spawn based off int to colour")]
    public int enemyHp;

    [Tooltip("Number of enemies to spawn in this group")]
    public int count;

    [Tooltip("Delay before this group starts spawning")]
    public float delayBeforeGroup = 0f;

    [Tooltip("Time between spawning each enemy in this group")]
    public float spawnInterval = 0.5f;

    [Tooltip("Does boss spawn")]
    public bool bossSpawn = false;

    [Header("Ground Effects")]
    public List<GroundEffectType> groundEffects = new List<GroundEffectType>();
    public float paintMoveSpeedModifer = 0;
    
    [Header("Shield Directions")]
    [Tooltip("List of directions for shields")]
    
    public List<ShieldDirectionType> shieldDirections = new List<ShieldDirectionType>();
    
    [Header("Shield HP")]
    public int shieldHp;
    
    [Header("Rotation")]
    public bool isRotating = false;
    public bool clockWise = false;
    public bool counterClockWise = false;
     
    [Header("On-Death Effects")]
    [Tooltip("Which effects to apply when this enemy dies")]
    public List<OnDeathEffectType> onDeathEffects = new List<OnDeathEffectType>();
    public int roundsBeforePortalIsDestroied = 0;
}
