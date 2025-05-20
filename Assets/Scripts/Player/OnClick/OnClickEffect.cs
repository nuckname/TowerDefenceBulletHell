using UnityEngine;

public class OnClickEffect : MonoBehaviour
{
    [SerializeField] private GameObject UpgradeUi; // UI panel for upgrades
    [SerializeField] private Transform CanvasTransform;
    [SerializeField] private float selectionRadius = 0.5f;
    private GameObject newUI;
    public Collider2D[] colliders;
    public bool UiOpenCantUpgradeTurret = false;
    
    public GameObject TurretSelected()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Turret"))
        {
            GameObject selectedTurret = hit.collider.gameObject;
            Debug.Log("Turret clicked: " + selectedTurret.name);

            if (newUI == null && !UiOpenCantUpgradeTurret)
            {

                Debug.Log("UI is null. Spawning new UI.");
                newUI = Instantiate(UpgradeUi, CanvasTransform);
                newUI.SetActive(true);
                
                UpgradeUiManager upgradeManager = newUI.GetComponent<UpgradeUiManager>();
                upgradeManager.SetDescriptionsForUpgrades(selectedTurret);
                upgradeManager.targetTurret = selectedTurret;

                print("BindingOfIsaacShooting.disableShooting = true;");
                PlayerShooting.disableShooting = true;
           
            }
            return selectedTurret;
        }
        return null;
    }
}
