using UnityEngine;

public class ResetScriptableObjects : MonoBehaviour
{
    [SerializeField] private TurretConfig basicTurret;
    void Start()
    {
        basicTurretReset();
    }

    private void basicTurretReset()
    {
        basicTurret.bulletSize = 1;
        basicTurret.bulletSpeed = 3;
        basicTurret.bulletLifeTime = 2;
        basicTurret.numberOfProjectiles = 1;
        basicTurret.fireRate = 0.75f;
    }
}
