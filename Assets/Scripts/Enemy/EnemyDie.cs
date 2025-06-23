using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public bool enemyHasOnDeathEffect;
    
    [SerializeField] private EnemyDropItems _enemyDropItems;

    [SerializeField] private GameObject onDeathEffect; 
    
    [SerializeField] private EnemyOnMapCounter enemyOnMapCounter;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        //I dont understand why I cant just get a refernfce it doesnt work.
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

        TriggerOnDeathEffect();
        
        enemyOnMapCounter.DecreaseEnemyCount();

        _enemyDropItems.DropItems(false);

        Destroy(gameObject);
    }

    private void TriggerOnDeathEffect()
    {
        foreach (BaseOnDeathEffect effect in GetComponents<BaseOnDeathEffect>())
        {
            effect.TriggerEffect();
        }
    }
}