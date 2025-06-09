using UnityEngine;

public class UpgradeRadius : MonoBehaviour
{
    [SerializeField] private GameObject facingUpwardsUiManager;
    [SerializeField] private GameObject facingDownUiManager;
    //split script. Changing 
    private Collider2D circleCollider;
    public ContactFilter2D contactFilter;

    
    public Collider2D[] results = new Collider2D[50];
    

    //[SerializeField] private List<Collider2D> results = new List<Collider2D>();
    
    private int colliderCount;

    // Tracks the current selection index
    [SerializeField]
    private int UpgradeSwitchIndex = -1;

    [SerializeField]
    private GameObject selectedGameObject;

    public bool allowTurretSwapping = false;
    public bool UpgradeRadiusOn = true;
    
    //Upgrade UI we spawn in.
    private GameObject instantiatedUi;


    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (allowTurretSwapping)
        {
            SwitchingTurretsInRadius();
        }
    }

    private void SwitchingTurretsInRadius()
    {
        //swaps between turrets in radius
        // Forward switching
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedGameObject = SwitchSelection(1); 
        }

        // Backward switching
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedGameObject = SwitchSelection(-1);
        }

        // Upgrade action
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedGameObject != null)
            {
                SelectedObject(selectedGameObject);
                
                TurnOffRadiusSelection();
            }
            else
            {
                Debug.Log("No turret selected to upgrade.");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            print("Rotate");
            RotateObject(selectedGameObject);
        }
    }

    private void SpawnUiUporDownFacing(GameObject turret)
    {
        if (turret.transform.position.y >= 2.95)
        {
            instantiatedUi = Instantiate(facingDownUiManager, new Vector3(
                turret.transform.position.x, 
                turret.transform.position.y - 3.22f,
                turret.transform.position.z), Quaternion.identity);
        }
        else
        {
            instantiatedUi = Instantiate(facingUpwardsUiManager, new Vector3(
                turret.transform.position.x,
                turret.transform.position.y + 3.22f, 
                turret.transform.position.z), Quaternion.identity);
        }
    }

 
    private void SelectedObject(GameObject turret)
    {
        SpawnUiUporDownFacing(turret);
        
        //UpgradeUiManager upgradeManager = UiManager.GetComponent<UpgradeUiManager>();
        UpgradeUiManager upgradeManager = instantiatedUi.GetComponent<UpgradeUiManager>();
        
        upgradeManager.SetupUpgradesForTurret(turret);

        //upgradeManager.SetDescriptionsForUpgrades(turret);
        //Sets a global Varible for the turret.
        //Fixes a bug where targetTurret was null
        upgradeManager.targetTurret = turret;
        
        //This might not be scalable.
        allowTurretSwapping = false;
        UpgradeRadiusOn = false;
        PlayerShooting.disableShooting = true;

    }

    private void RotateObject(GameObject turret)
    {
        turret.transform.Rotate(0, 0, -90);
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
            print("Collider2D: " + other);
            allowTurretSwapping = true;

            // Temporarily store all colliders detected
            Collider2D[] tempResults = new Collider2D[50];
            int totalColliders = circleCollider.Overlap(contactFilter, tempResults);

            // Filter only turrets into results[]
            colliderCount = 0;
            for (int i = 0; i < totalColliders; i++)
            {
                if (tempResults[i] != null && tempResults[i].CompareTag("Turret"))
                {
                    results[colliderCount] = tempResults[i]; // Store valid turrets
                    colliderCount++;
                }
            }

            HighlightFurthestTurret();
        }
    }


    public void HighlightFurthestTurret()
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

            if (colliderCount == 0)
            {
                allowTurretSwapping = false;
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
