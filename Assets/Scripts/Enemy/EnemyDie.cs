using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    [SerializeField] private AddGold _addGold;
    [SerializeField] private EnemyDropItems _enemyDropItems;

    private RoundManager _roundManager;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _roundManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RoundManager>();
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

        // Counting enemy deaths
        _roundManager.totalEnemiesSpawnAmount -= 1;

        // Starts new round
        if (_roundManager.totalEnemiesSpawnAmount == 0)
        {
            _roundManager.CreateRound();
        }

        _addGold.AddGoldToDisplay(20);

        _enemyDropItems.DropItems();

        Destroy(gameObject);
    }
}