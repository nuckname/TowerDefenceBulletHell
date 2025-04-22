using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretStats : MonoBehaviour
{
    [Header("Total Upgrade Count")] 
    public int totalAmountOfUpgrades = 0;
    
    [Header("Basic")]
    public float modifierFireRate = 0;
    public float modifierBulletLifeTime = 1;
    public float modifierBulletSpeed = 0;
    public float modifierSlowerBulletSpeed = 0;
    
    //This is only for the stats sheet and does nothing to the turret.
    [Header("is homing")]
    public bool isTurretHoming = false;
    
    [Header("Pierce")]
    public int pierceCount = 0;

    [Header("Extra Projectiles")]
    public float angleSpread = 30f;
    public int extraProjectiles = 1;

    public bool Shoot4Projectiles = false;
    
    [Header("Bullet Split")]
    public bool enableBulletSplit = false;
    public int splitAmount = 0;
    
    [Header("Multi Shot")]
    public int multiShotCount = 1;
    public float multiShotDelay = 0.25f;

    [Header("Extra Shoot Points")]
    [Header("North, South, East, West")]
    public bool allow4ShootPoints = false;
    public bool TurretShootsBackwards = false;
    public bool TurretShootsUpAndDown = false;
    public int activeDirections = 0;

    [Header("Figure‑8 Upgrade")]
    public bool  enableFigure8      = false;
    public float figure8RadiusX     = 2f;
    public float figure8RadiusY     = 1f;
    public float figure8LoopsPerSec = 0.5f;
    
    [Header("Slow On Hit")]
    public bool slowOnHitEnabled = false;
    public float slowAmount = 0f;
    public float slowDuration = 0f;
    
    [Header("Diagonal")]
    public bool allowDiagonalShooting = false;

    
    [Header("Bouncing")] 
    public int amountOfBounces = 1;
    public bool AllowBulletsToBounce = false;
    
    [Header("Chain")] 
    public int chainRange = 5;
    public bool AllowBulletsToBouncesToChain = false;

    [Header("Return Projectiles")] 
    //When player has 3 pierce? enable 
    public bool projectilesReturn = false;
    
    [Header("TrackShooter")] 
    public bool enableOrbit = false;
    public float orbitRadius = 180;
    public float orbitSpeed = 90;

    [Header("Gold On hit")] 
    public bool GoldOnHit = false;

    [Header("Spiral")] 
    public bool spiralBullets = false;
}
