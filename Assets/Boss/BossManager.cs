using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class BossManager : MonoBehaviour
{
    [SerializeField] private Vector2 BossSpawnPoint;
    [SerializeField] private GameObject SnakeBoss;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(SnakeBoss, BossSpawnPoint, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
