using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private EnemyHealth _enemyHealth;
    // Start is called before the first frame update
    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _enemyHealth.EnemyHit();  
            print("bullet coll");
            
            Destroy(other.gameObject);
            
        }
        
        if (other.gameObject.CompareTag("PlayerCollision"))
        {
            print("GET OFF ME");
            //Player hit?
        }
        
        if (other.gameObject.CompareTag("RedBox"))
        {
            Destroy(gameObject);
            print("-1 hp");
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _enemyHealth.EnemyHit();  
            print("stay bullet coll");
            
            Destroy(other.gameObject);
            
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            print("GET OFF ME");
            //Player hit?
        }
    }
}
