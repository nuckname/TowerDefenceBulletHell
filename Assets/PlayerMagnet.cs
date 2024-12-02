using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        print(other);
        if (other.gameObject.TryGetComponent<Collectable>(out Collectable collectable))
        {
            print("called target");
            collectable.SetTarget(transform.parent.position);
        }
    }
}
