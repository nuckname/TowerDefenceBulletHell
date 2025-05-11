using System;
using UnityEngine;

public class RemoveObjectOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //other.tag == "EnemyBullet"||
        
        if (other.tag == "GhostBullet"|| 
            other.tag == "Bullet" ||
            other.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
        }
    }

}
