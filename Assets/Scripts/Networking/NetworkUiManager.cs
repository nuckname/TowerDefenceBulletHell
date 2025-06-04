using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUiManager : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button cilentButton;

    private void Awake()
    {
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();

        });
        
        cilentButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();

        });
    }
}
