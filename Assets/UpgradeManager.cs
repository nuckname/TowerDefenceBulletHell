using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    [SerializeField] private TurretConfig basicTurret;

    [SerializeField] private GenerateShopText _generateShopText;

    private void Awake()
    {
        _generateShopText = GameObject.FindGameObjectWithTag("Shop").GetComponent<GenerateShopText>();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
        
    }

    private void BulletLifeUpgrade()
    {
        basicTurret.bulletLifeTime += 1;
    }

    private void Projectile()
    {
        basicTurret.bulletSpeed += 2;
    }
    
    //Bullet Size
    private void BulletSize()
    {
        basicTurret.bulletSize += 0.5f;
    }
    
    //Fire rate.
    private void IncreasedTurretFireRate()
    {
        basicTurret.fireRate += 0.5f;
    }
    
}
