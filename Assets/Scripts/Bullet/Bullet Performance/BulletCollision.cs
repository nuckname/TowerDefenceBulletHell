using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private GameObject basicTurret;

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("OutOfZone"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            //Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            print("enemy");
            //Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            print("-1 hp?");
        }
    }
}
