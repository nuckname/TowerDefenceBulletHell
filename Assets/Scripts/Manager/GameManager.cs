using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;

    void Start()
    {
        Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
    }
    
}
