// TeleportPortal.cs
using UnityEngine;

public class TeleportPortal : MonoBehaviour
{
    [Tooltip("Where things entering this collider should be sent")]
    public Transform destination;

    private void Reset()
    {
        // ensure trigger
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // only teleport enemies (or whatever tag you like)
        if (destination != null && other.CompareTag("Enemy"))
        {
            other.transform.position = destination.position;
        }
    }
}