using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBuilderMovement : MonoBehaviour
{
    
    [SerializeField] private GameObject player;
    
    private GameObject _ghostBuilding; 
    
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the horizontal and vertical axes
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.S))
        {
            _ghostBuilding.transform.position = new Vector3(player.transform.position.x - 4, player.transform.position.y - 4, player.transform.position.z);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }
}
