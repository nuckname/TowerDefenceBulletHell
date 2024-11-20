using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] private EnemyDie enemyDie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gameObject.CompareTag("Bullet"))
        {
            print("COLL");
            //Goes down a level like bloons 
            //destory enemy
            Destroy(gameObject);
            //destory bullet
            Destroy(other.gameObject);
            enemyDie.EnemyHasDied();
        }
        
        if (gameObject.CompareTag("Player"))
        {
            print("GET OFF ME");
        }
    }
}
