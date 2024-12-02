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
    public void BulletLifeUpgrade()
    {
        print("selected upgrade: bullet life time");

        basicTurret.bulletLifeTime += 1;
    }

    public void Projectile()
    {
        print("selected upgrade: proj speed");
        basicTurret.bulletSpeed += 2;
    }
    
    //Bullet Size
    public void BulletSize()
    {
        print("selected upgrade: bullet size");
        basicTurret.bulletSize += 0.5f;
    }
    
    //Fire rate.
    private void IncreasedTurretFireRate()
    {
        basicTurret.fireRate += 0.5f;
    }
    
}
