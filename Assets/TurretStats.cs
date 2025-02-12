using System;
using UnityEngine;

public class TurretStats : MonoBehaviour
{
    [Header("Basic")]
    public float modifierFireRate = 0;
    public float modifierBulletLifeTime = 1;
    public float modifierBulletSpeed = 0;
    
    [Header("Pierce")]
    public int pierceCount = 0;

    [Header("Extra Projectiles")]
    public float angleSpread = 30f;
    public int extraProjectiles = 1;

    public bool Shoot4Projectiles = false;
    
    [Header("Multi Shot")]
    public int multiShotCount = 1;
    public float multiShotDelay = 0.25f;

    [Header("Extra Shoot Points")] 
    public bool allow4ShootPoints = false;
    public int activeDirections = 1;

    [Header("Bouncing")] 
    public int amountOfBounces = 0;

    [Header("Bouncing")] 
    //When player has 3 pierce? enable 
    public bool projectilesReturn = false;
    
    [Header("TrackShooter")] 
    public bool enableOrbit = false;
    public float orbitRadius = 180;
    public float orbitSpeed = 90;

    [Header("Gold On hit")] 
    public bool GoldOnHit = false;
    
    [Header("Turret Luck")] 
    public int ReduceTurretBlankChance = 0;
    
    public int NormalIncreaseChanceForRarity = 0;
    public int RareIncreaseChanceForRarity = 0;
    public int LegendaryIncreaseChanceForRarity = 0;

    [Header("Spiral")] 
    public bool spiralBullets = false;
}
