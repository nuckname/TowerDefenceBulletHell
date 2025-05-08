using System;
using UnityEngine;

public class Maget : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Collectable>(out Collectable collectable))
        {
            print("Collecting");
            collectable.SetTarget(gameObject.transform.position);        
        }
        print("No collecting");
    }
}
