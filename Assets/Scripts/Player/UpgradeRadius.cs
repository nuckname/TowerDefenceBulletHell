using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeRadius : MonoBehaviour
{
    private Collider2D circleCollider;
    public ContactFilter2D contactFilter;

    
    [SerializeField]
    private Collider2D[] results = new Collider2D[15];
    

    //[SerializeField] private List<Collider2D> results = new List<Collider2D>();
    
    private int colliderCount;

    // Tracks the current selection index
    [SerializeField]
    private int UpgradeSwitchIndex = -1;

    [SerializeField]
    private GameObject selectedGameObject;

    private bool UpgradeRadiusOn = true;
    
    

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        // Forward switching
        if (Input.GetKeyDown(KeyCode.L))
        {
            selectedGameObject = SwitchSelection(1); 
        }

        // Backward switching
        if (Input.GetKeyDown(KeyCode.J))
        {
            selectedGameObject = SwitchSelection(-1);
        }

        // Upgrade action
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedGameObject != null)
            {
                Debug.Log("Upgrade selected turret: " + selectedGameObject.name);
                ApplyUpgrade(selectedGameObject);
                
                TurnOffRadiusSelection();
            }
            else
            {
                Debug.Log("No turret selected to upgrade.");
            }
        }
    }
    
    private void ApplyUpgrade(GameObject turret)
    {
        UpgradeTower upgradeTower = turret.GetComponent<UpgradeTower>();
        upgradeTower.SelectedUpgrade();
        
        //these do the same thing. I should use a state machine for this. for upgrades
        upgradeTower.allowSwappingBetweenUpgrade = true;
        UpgradeRadiusOn = false;

        //turn back on wear?

        //opens UI

        //Selects Upgrade

        //Passes it into

        //turret.GetComp<Upgrade>
    }

    private void TurnOffRadiusSelection()
    {
        for (int i = 0; i <= results.Length - 1; i++)
        {
            if (results[i] != null)
            {
                results[i].gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            }
        }
    }


    private GameObject SwitchSelection(int direction)
    {
        if (colliderCount > 0)
        {
            // Reset the previous selection color to default
            if (UpgradeSwitchIndex >= 0 && UpgradeSwitchIndex < colliderCount)
            {
                SpriteRenderer previousSprite = results[UpgradeSwitchIndex]?.GetComponent<SpriteRenderer>();
                if (previousSprite != null)
                {
                    previousSprite.color = Color.red;
                }
            }

            // Update the index to cycle through colliders
            UpgradeSwitchIndex = (UpgradeSwitchIndex + direction + colliderCount) % colliderCount;

            // Highlight the new selection
            SpriteRenderer currentSprite = results[UpgradeSwitchIndex]?.GetComponent<SpriteRenderer>();
            if (currentSprite != null)
            {
                currentSprite.color = Color.green;
                return results[UpgradeSwitchIndex].gameObject; 
            }
        }
        else
        {
            Debug.Log("Nothing to upgrade.");
        }

        return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Turret") && UpgradeRadiusOn)
        {
            // Populate results array with colliders
            colliderCount = circleCollider.Overlap(contactFilter, results);

            HighlightFurthestTurret();
        }
    }

    private void HighlightFurthestTurret()
    {
        if (colliderCount > 0)
        {
            float furthestDistance = 0f;  // Start with a distance of 0
            int furthestIndex = -1;

            for (int i = 0; i < colliderCount; i++)
            {
                if (results[i] != null)
                {
                    // Calculate the distance between the current object and the turret
                    float distance = Vector2.Distance(transform.position, results[i].transform.position);

                    // Check for the furthest turret (change to `>` instead of `<`)
                    if (distance > furthestDistance)
                    {
                        furthestDistance = distance;
                        furthestIndex = i;
                    }

                    // Reset all to red initially
                    SpriteRenderer spriteRenderer = results[i]?.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.color = Color.red;
                    }
                }
            }

            // Highlight the furthest turret in green
            if (furthestIndex != -1)
            {
                UpgradeSwitchIndex = furthestIndex;
                SpriteRenderer furthestSprite = results[furthestIndex]?.GetComponent<SpriteRenderer>();

                if (furthestSprite != null)
                {
                    selectedGameObject = results[furthestIndex].gameObject;
                    furthestSprite.color = Color.green;
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
            for (int i = 0; i < colliderCount; i++)
            {
                if (results[i] == other)
                {
                    results[i] = null;
                    break;
                }
            }

            // Adjust count and re-highlight the closest turret
            colliderCount--;
            
            //new
            if (selectedGameObject == other.gameObject)
            {
                selectedGameObject = null;
                UpgradeSwitchIndex = -1;

                // Highlight a new turret if any remain
                if (colliderCount > 0)
                {
                    HighlightFurthestTurret();
                }
            }
            
            /*
            if (colliderCount > 0)
            {
                HighlightFurthestTurret();
            }
            */
        }
    }
}
