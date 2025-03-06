using System;
using UnityEngine;

public class RemoveObjectOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }

}
