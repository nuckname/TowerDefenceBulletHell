using System;
using UnityEngine;

public class ZombieLogic : MonoBehaviour
{
    EnemyFollowPath _enemyFollowPath;
    private Transform _player;
    private bool followPlayer = false;
    
    public float speed = 1.75f;
    private void Awake()
    {
        _enemyFollowPath = GetComponentInParent<EnemyFollowPath>();
    }

    public void SetPathToPlayer()
    {
        _enemyFollowPath.enabled = false;
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            _player = playerObj.transform;
            followPlayer = true;
        }
        else
        {
            Debug.LogWarning("Player not found!");
        }
        
    }
    
    private void Update()
    {
        if (followPlayer && _player != null)
        {
            print("Follow player");
            
            Vector3 direction = (_player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
