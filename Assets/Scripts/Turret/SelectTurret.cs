using UnityEngine;

public class SelectTurret : MonoBehaviour
{
    [SerializeField] private GameObject UpgradeUi; // UI panel for upgrades
    private GameObject turretSelected;

    [SerializeField] private Transform CanvasTransform;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            turretSelected = TurretSelected();

            // Show UI only if a turret is selected, otherwise hide it
            //UpgradeUi.SetActive(turretSelected != null);
        }
    }

    GameObject TurretSelected()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider == null)
        {
            return null;
        }

//        if (hit.collider.CompareTag("Turret") || hit.collider.CompareTag("TurretPlacementCollider"))

        if (hit.collider.CompareTag("TurretPlacementCollider"))
        {
            hit.collider.gameObject.GetComponentInParent<UpgradeUiManager>();

        }
        
        
    if (hit.collider.CompareTag("Turret"))
        {
            Debug.Log("Turret clicked: " + hit.collider.name);
            //UpgradeUi.SetActive(hit.collider.gameObject);
            
            GameObject newUI = Instantiate(UpgradeUi, CanvasTransform);
            newUI.SetActive(true);
            
            Debug.Log("Turret clicked: " + hit.collider.name);
            UpgradeUiManager upgradeManager = newUI.GetComponent<UpgradeUiManager>();
            upgradeManager.SetDescriptionsForUpgrades(hit.collider.gameObject);
            
            //Sets a global Varible for the turret.
            //Fixes a bug where targetTurret was null
            Debug.Log("Turret clicked: " + hit.collider.name);
            upgradeManager.targetTurret = hit.collider.gameObject;
        
            //This might not be scalable.
            //allowTurretSwapping = false;
            //UpgradeRadiusOn = false;
            BindingOfIsaacShooting.disableShooting = true;
            return hit.collider.gameObject;
        }

        return null;
    }
}