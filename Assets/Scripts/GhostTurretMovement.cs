using System;
using Unity.Netcode;
using UnityEngine;

public class GhostTurretMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float followSpeed = 10f; 

    private void Awake()
    {
        PlayerShooting.disableShooting = true;
    }

    void Update()
    {
        /*
        if (!IsOwner)
        {
            return;
        }
        */

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; 

        transform.position = Vector3.Lerp(transform.position, mousePos, followSpeed * Time.deltaTime);

    }
}