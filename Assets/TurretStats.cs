using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretStats : MonoBehaviour
{
    public List<ItemList> items = new List<ItemList>();

    public GameObject turretsPersonalBullet;
    private void Start()
    {
        print("Created new item");
        IncreaseAttackSpeed item = new IncreaseAttackSpeed();
        items.Add(new ItemList(item, item.GiveName(), 1));
    }

    [Header("Total Upgrade Count")] 
    public int totalAmountOfUpgrades = 0;
    
    [Header("Basic")]
    public float modifierFireRate = 0;
    public float modifierBulletLifeTime = 1;
    public float modifierBulletSpeed = 0;
    
    //This is only for the stats sheet and does nothing to the turret.
    [Header("is homing")]
    public bool isTurretHoming = false;
    
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
    [Header("North, South, East, West")]
    public bool allow4ShootPoints = false;
    public int activeDirections = 0;

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
    
    [Header("Turret Luck")] 
    public int ReduceTurretBlankChance = 0;
    
    public int NormalIncreaseChanceForRarity = 0;
    public int RareIncreaseChanceForRarity = 0;
    public int LegendaryIncreaseChanceForRarity = 0;

    [Header("Spiral")] 
    public bool spiralBullets = false;
}
