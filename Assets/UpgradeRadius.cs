using System;
using UnityEngine;

public class UpgradeRadius : MonoBehaviour
{
    private Collider2D circleCollider;
    public ContactFilter2D contactFilter;

    private Collider2D[] results = new Collider2D[50];
    private int count;

    // Tracks the current selection index
    [SerializeField]
    private int UpgradeSwitchIndex = -1;

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        //forward switching
        if (Input.GetKeyDown(KeyCode.L))
        {
            SwitchSelection(1); 
        }

        //backward switching
        if (Input.GetKeyDown(KeyCode.J))
        {
            SwitchSelection(-1);
        }
    }

    private void SwitchSelection(int direction)
    {
        if (count > 0) 
        {
            // Reset the previous selection color to default
            if (UpgradeSwitchIndex >= 0 && UpgradeSwitchIndex < count)
            {
                SpriteRenderer previousSprite = results[UpgradeSwitchIndex]?.GetComponent<SpriteRenderer>();
                if (previousSprite != null)
                {
                    previousSprite.color = Color.red; 
                }
            }

            // Update the index to cycle through colliders
            UpgradeSwitchIndex = (UpgradeSwitchIndex + direction + count) % count;

            // Highlight the new selection
            SpriteRenderer currentSprite = results[UpgradeSwitchIndex]?.GetComponent<SpriteRenderer>();
            if (currentSprite != null)
            {
                currentSprite.color = Color.green;
            }
        }
        else
        {
            Debug.Log("Nothing to upgrade.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Turret"))
        {
            // Populate results array with colliders
            count = circleCollider.Overlap(contactFilter, results);

            for (int i = 0; i < count; i++)
            {
                Debug.Log("Detected: " + results[i].name);

                // Set all detected objects to default upgrade color
                SpriteRenderer spriteRenderer = results[i].GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    //Would use an arrow later.
                    spriteRenderer.color = Color.red; 
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Turret"))
        {
            SpriteRenderer spriteRenderer = other.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.black; 
            }

            // Update results array when an object exits
            for (int i = 0; i < count; i++)
            {
                if (results[i] == other)
                {
                    results[i] = null;
                    break;
                }
            }

            // Adjust count and index to avoid issues
            count--;
            if (UpgradeSwitchIndex >= count)
            {
                UpgradeSwitchIndex = count - 1;
            }
        }
    }
}
