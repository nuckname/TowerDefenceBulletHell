using System;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class Testingnetworkui : MonoBehaviour
{
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button startClientButton;

    private void Awake()
    {
        startHostButton.onClick.AddListener(() =>
        {
            Debug.Log("Started host");
            NetworkManager.Singleton.StartHost();
            Hide();
        });
        
        startClientButton.onClick.AddListener(() =>
        {
            Debug.Log("Started client");
            NetworkManager.Singleton.StartClient();
            Hide();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
