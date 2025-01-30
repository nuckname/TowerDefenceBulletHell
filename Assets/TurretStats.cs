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
    
    [Header("Multi Shot")]
    public int multiShotCount = 1;
    public float multiShotDelay = 0.25f;
}
