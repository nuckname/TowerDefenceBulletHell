using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    public TurretConfig turretConfig;
    public Transform shootPoint;
    private float fireCooldown;

    //Might have to make these public later. 
    [SerializeField] private bool upShootDirction; 
    [SerializeField] private bool downShootDirction; 
    [SerializeField] private bool leftShootDirction; 
    [SerializeField] private bool rightShootDirction;

    [SerializeField] private Transform ShootPointUp, ShootPointDown, ShootPointLeft, ShootPointRight;
    [SerializeField] private Transform[] ShootPoints;
    
    //Use Scriptable GameObject Prefab to increase or decrease firerate. 
    //public float increaseFireCooldown = 1;
    
    public int numberOfProjectiles = 1; 
    
    private List<Vector2> directions = new List<Vector2>();

    private int shootPointIndex;
    private void Awake()
    {
        ShootPoints = new Transform[] { ShootPointUp, ShootPointDown, ShootPointLeft, ShootPointRight };
    }

    private void Start()
    {
        AddDirectionsToTurret();
    }

    private void AddDirectionsToTurret()
    {
        if (upShootDirction == true)
        {
            directions.Add(Vector2.up);
        }
    
        if (downShootDirction == true)
        {
            directions.Add(Vector2.down);
        }

        if (leftShootDirction == true)
        {
            directions.Add(Vector2.left);
        }

        if (rightShootDirction == true)
        {
            directions.Add(Vector2.right);
        }
    }


    void Update()
    {
        //rb.velocity = direction * turretConfig.bulletSpeed;
        if (turretConfig == null || shootPoint == null)
        {
            Debug.LogWarning("TurretConfig or Shoot Point is missing.");
            return;
        }

        // Handle shooting based on fire rate
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / turretConfig.fireRate;
        }
    }
/*
    private IEnumerator FadeOut(float duration)
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime; 
            yield return null;
        }
    }
  */  
    private void Shoot()
    {
        if (turretConfig.bulletPrefab == null)
        {
            Debug.LogWarning("Bullet Prefab is missing in TurretConfig.");
            return;
        }

        // Loop through each direction and shoot a bullet
        foreach (Vector2 direction in directions)
        {
            
            GameObject bullet = Instantiate(
                turretConfig.bulletPrefab, 
                new Vector3(ShootPoints[shootPointIndex].position.x, ShootPoints[shootPointIndex].position.y, -1), 
                Quaternion.identity
            );
            
            for (int i = 0; i <= numberOfProjectiles; i++)
            {
                // Create the bullet and set its properties

                //StartCoroutine()
            }

            //-1 so we dont get an error
            if (shootPointIndex >= directions.Count - 1)
            {
                shootPointIndex = 0;
            }
            else
            {
                shootPointIndex++;
            }
            
            Destroy(bullet, 2.4f);
        }
    }
}