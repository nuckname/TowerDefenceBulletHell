using UnityEngine;

public class OnClickEffect : MonoBehaviour
{
    [SerializeField] private GameObject UpgradeUi; // UI panel for upgrades
    [SerializeField] private Transform CanvasTransform;
    [SerializeField] private float selectionRadius = 0.5f;
    private GameObject newUI;
    public Collider2D[] colliders;
    public static bool UiOpenCantUpgradeTurret = false;
    
    public GameObject TurretSelected()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Turret"))
        {
            GameObject selectedTurret = hit.collider.gameObject;

            AudioManager.instance.SelectTurretSFX();
            
            if (newUI == null && !UiOpenCantUpgradeTurret)
            {
                UiOpenCantUpgradeTurret = true;
                
                newUI = Instantiate(UpgradeUi, CanvasTransform);
                newUI.SetActive(true);
                
                UpgradeUiManager upgradeManager = newUI.GetComponent<UpgradeUiManager>();
                upgradeManager.SetDescriptionsForUpgrades(selectedTurret);
                upgradeManager.targetTurret = selectedTurret;

                PlayerShooting.disableShooting = true;
           
            }
            return selectedTurret;
        }
        return null;
    }
}
