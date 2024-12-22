using System;
using UnityEngine;

public class UpgradeRadius : MonoBehaviour
{
    private Collider2D circleCollider;
    public ContactFilter2D contactFilter;

    private Collider2D[] results = new Collider2D[50];
    private int colliderCount;

    // Tracks the current selection index
    [SerializeField]
    private int UpgradeSwitchIndex = -1;

    [SerializeField]
    private GameObject selectedGameObject;

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
                //ApplyUpgrade(selectedGameObject);
            }
            else
            {
                Debug.Log("No turret selected to upgrade.");
            }
        }
    }
    /*
    private void ApplyUpgrade(GameObject turret)
    {
        // Example logic: Apply the first upgrade from the list
        if (availableUpgrades.Count > 0)
        {
            Upgrade upgrade = availableUpgrades[0]; // Pick the first upgrade for simplicity
            Debug.Log($"Applying {upgrade.name} to {turret.name}");

            TurretController turretController = turret.GetComponent<TurretController>();
            if (turretController != null)
            {
                turretController.UpgradeTurret(upgrade);
            }
        }
        else
        {
            Debug.Log("No upgrades available.");
        }
    }
    */


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
        if (other.CompareTag("Turret"))
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
            if (colliderCount > 0)
            {
                HighlightFurthestTurret();
            }
        }
    }
}
