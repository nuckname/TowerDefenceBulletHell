using System;
using Unity.VisualScripting;
using UnityEngine;

public class CircleAttackCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerMagnet")
        {
            print("spawned in player's magnet circle collider");
            Destroy(gameObject);
        }
    }
}
