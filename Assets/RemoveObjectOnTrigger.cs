using System;
using UnityEngine;

public class RemoveObjectOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            return;
        }
        
        if (other.tag == "Player")
        {
            return;
        }
        Destroy(other.gameObject);
    }

}
