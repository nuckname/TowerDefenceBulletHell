using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private HealthManager _healthManager;

    [SerializeField] private TextMeshProUGUI playerHP;
    [SerializeField] private TextMeshProUGUI deathText;
    
    [SerializeField] private int currentHP = 10;

    private void Awake()
    {
        playerHP = GameObject.FindGameObjectWithTag("PlayerTextHP").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            print("-1 health");
            currentHP -= 1;
            playerHP.text = $"HP: {currentHP.ToString()}";

            if (currentHP == 0)
            {
                //Restart Scene
                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentScene.name);
            }
            //_healthManager.PlayerMinusHealth();
            //Destory Bullet
            Destroy(other);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

    }
}
