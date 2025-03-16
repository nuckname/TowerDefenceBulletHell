using UnityEngine;

public class OnClickEffect : MonoBehaviour
{
    [SerializeField] private GameObject UpgradeUi; // UI panel for upgrades
    [SerializeField] private Transform CanvasTransform;
    [SerializeField] private float selectionRadius = 0.5f; // Radius around the mouse click to detect turrets

    public GameObject TurretSelected()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get all colliders within the small radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePos, selectionRadius);

        // List to store all found turrets
        GameObject closestTurret = null;
        float closestDistance = Mathf.Infinity;

        // Iterate through colliders to find turrets
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Turret"))
            {
                float distance = Vector2.Distance(mousePos, collider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTurret = collider.gameObject;
                }
            }
        }

        // If a turret is found, show the UI
        if (closestTurret != null)
        {
            Debug.Log("Closest turret clicked: " + closestTurret.name);

            GameObject newUI = Instantiate(UpgradeUi, CanvasTransform);
            newUI.SetActive(true);

            UpgradeUiManager upgradeManager = newUI.GetComponent<UpgradeUiManager>();
            upgradeManager.SetDescriptionsForUpgrades(closestTurret);
            upgradeManager.targetTurret = closestTurret;

            // Disable shooting while upgrading
            BindingOfIsaacShooting.disableShooting = true;
        }

        return closestTurret;
    }

    // Debugging: Draw the selection radius in the scene view
    private void OnDrawGizmos()
    {
        if (Camera.main == null) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(mousePos, selectionRadius);
    }
}
