using System;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private Button spawnPlayerButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;
    private void Awake()
    {
        spawnPlayerButton.onClick.AddListener(() =>
        {
            Instantiate(player, spawnPoint.position, Quaternion.identity);
            Hide();
        });
        
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();

            Hide();
        });
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            spawnPlayerButton.onClick.Invoke();
        }
        
        if (Input.GetKey(KeyCode.Escape))
        {
            quitButton.onClick.Invoke();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
