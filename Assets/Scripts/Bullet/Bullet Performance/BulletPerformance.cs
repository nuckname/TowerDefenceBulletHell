using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPerformance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("OutOfZone"))
        {
            print("debug");
            Destroy(gameObject);
        }
    }
}
