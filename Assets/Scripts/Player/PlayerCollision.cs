using System;
using TMPro;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private HealthManager _healthManager;

    [SerializeField] private TextMeshPro playerHP;
    [SerializeField] private int currentHP = 10;

    private void Awake()
    {
        playerHP = GameObject.FindGameObjectWithTag("PlayerTextHP").GetComponent<TextMeshPro>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            print("-1 health");
            currentHP -= 1;
            playerHP.text = $"HP: {currentHP.ToString()}";
            
            //_healthManager.PlayerMinusHealth();
            //Destory Bullet
            Destroy(other);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

    }
}
