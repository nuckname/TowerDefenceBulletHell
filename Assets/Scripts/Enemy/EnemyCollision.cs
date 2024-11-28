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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
             _enemyHealth.EnemyHit();  
            
            Destroy(other.gameObject);
            
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            print("GET OFF ME");
            //Player hit?
        }
    }
}
