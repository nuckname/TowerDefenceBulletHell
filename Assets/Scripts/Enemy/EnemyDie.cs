using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    [SerializeField] private EnemyDropItems _enemyDropItems;

    [SerializeField] private EnemyOnMapCounter enemyOnMapCounter;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        enemyOnMapCounter = GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<EnemyOnMapCounter>();
        
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void EnemyHasDied()
    {
        // Add a small random directional force
        if (_rigidbody != null)
        {
            Vector3 randomDirection = new Vector3(
                UnityEngine.Random.Range(-1f, 1f), 
                UnityEngine.Random.Range(-1f, 1f), 
                UnityEngine.Random.Range(-1f, 1f)
            ).normalized;

            float forceMagnitude = 5f; // Adjust the magnitude as needed
            _rigidbody.AddForce(randomDirection * forceMagnitude, ForceMode.Impulse);
        }
        
        enemyOnMapCounter.DecreaseEnemyCount();

        _enemyDropItems.DropItems();

        Destroy(gameObject);
    }
}