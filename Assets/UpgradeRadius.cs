using System;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeRadius : MonoBehaviour
{
    private Collider2D circleCollider;
    public ContactFilter2D contactFilter; // Define what layers to detect
    
    //maybe make a list later. 
    private Collider2D[] results = new Collider2D[50];
    private int count;

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Turret"))
        {
             count = circleCollider.Overlap(contactFilter, results);

            for (int i = 0; i < count; i++)
            {
                Debug.Log("Detected: " + results[i].name);

                SpriteRenderer spriteRenderer = results[i].GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {

                    float distance = Vector3.Distance(transform.position, circleCollider.transform.position);

                    //If they press u for upgrade.
                    //opens a select screen
                    //pause game

                    //determine which game object in radius is closes 
                    //Arrow keys to select different red squares.

                    //closes game object should turn in red square.
                    //all radius should have light red or something. 
                    spriteRenderer.color = Color.red;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Turret"))
        {
            SpriteRenderer spriteRenderer = other.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.black;
            }
        }
    }
}