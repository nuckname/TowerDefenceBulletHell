// TeleportPortal.cs
using UnityEngine;

public class TeleportPortal : MonoBehaviour
{
    private Transform destination;

    public bool isTeleportSender = false;
    public bool isTeleportReceiver = false;

    private void Reset()
    {
        // ensure trigger
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && isTeleportSender)
        {
            if (destination == null)
            {
                GameObject receiver = GameObject.FindGameObjectWithTag("teleportReceiver");

                if (receiver != null)
                {
                    //cache destination
                    destination = receiver.transform;
                    other.transform.position = destination.position;
                }
                else
                {
                    Debug.LogWarning("No object with tag 'teleportReceiver' found!");
                }
            }
            else
            {
                other.transform.position = destination.position;
            }
        }
    }
}