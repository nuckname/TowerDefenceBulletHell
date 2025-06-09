using UnityEngine;
using TMPro;

/// <summary>
/// Handles the visual display of upgrade options including text, icons, and UI colors.
/// </summary>
public class UpgradeDisplayController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject[] allUpgradeUis;
    [SerializeField] private TMP_Text[] allTextUis;
    
    [Header("Dependencies")]
    [SerializeField] private GenerateRarity generateRarity;
    [SerializeField] private SelectDescription selectDescription;
    
    private ChangeUiColourBackGround changeUiColourBackGround;
    private UpgradeGold upgradeGold;
    private SetIconUpgrades setIconUpgrades;
    
    public string[] DisplayedUpgrades { get; private set; }
    public string SelectedRarity { get; private set; }
    
    private void Awake()
    {
        setIconUpgrades = GetComponentInParent<SetIconUpgrades>();
        //upgradeGold = GetComponent<UpgradeGold>();
        upgradeGold = GetComponentInParent<UpgradeGold>();
        changeUiColourBackGround = GetComponentInParent<ChangeUiColourBackGround>();
    }

    public void SetupUpgrades(GameObject targetTurret)
    {
        var storageComponent = targetTurret.GetComponent<StoreTurretDescriptionAndRarity>();
        bool hasExistingUpgrades = storageComponent.CheckTurretDescription();

        if (!hasExistingUpgrades)
        {
            GenerateNewUpgrades(targetTurret, storageComponent);
        }
        else
        {
            LoadExistingUpgrades(targetTurret, storageComponent);
        }

        UpdateVisualDisplay(storageComponent);
    }

    private void GenerateNewUpgrades(GameObject targetTurret, StoreTurretDescriptionAndRarity storage)
    {
        TurretStats turretStats = targetTurret.GetComponent<TurretStats>();
        UpgradeDataOnTurret upgradeData = targetTurret.GetComponent<UpgradeDataOnTurret>();
        
        SelectedRarity = generateRarity.SelectRarity(SelectedRarity, turretStats);
        storage.storedTurretSelectedRarity = SelectedRarity;
        
        DisplayedUpgrades = selectDescription.Get3Descriptions(SelectedRarity, upgradeData);
        storage.storedTurretDescription = DisplayedUpgrades;
        
        int upgradePrice = upgradeGold.DisplayGold(SelectedRarity, turretStats.totalAmountOfUpgrades);
        storage.storeTurretPrice = upgradePrice;
    }

    private void LoadExistingUpgrades(GameObject targetTurret, StoreTurretDescriptionAndRarity storage)
    {
        DisplayedUpgrades = storage.storedTurretDescription;
        SelectedRarity = storage.storedTurretSelectedRarity;
        
        upgradeGold.HardCodedUpdateGoldAmount(storage.storeTurretPrice);
    }

    private void UpdateVisualDisplay(StoreTurretDescriptionAndRarity storage)
    {
        SetTextToUI(DisplayedUpgrades);
        setIconUpgrades.SetIcons(DisplayedUpgrades, SelectedRarity, storage.storeTurretRotation);
        UpdateBackgroundColor(storage);
    }

    public void RefreshDisplay(GameObject targetTurret)
    {
        var storage = targetTurret.GetComponent<StoreTurretDescriptionAndRarity>();
        UpdateVisualDisplay(storage);
    }

    private void SetTextToUI(string[] upgradeTexts)
    {
        for (int i = 0; i < upgradeTexts.Length && i < allTextUis.Length; i++)
        {
            allTextUis[i].text = upgradeTexts[i];
        }
    }

    private void UpdateBackgroundColor(StoreTurretDescriptionAndRarity storage)
    {
        changeUiColourBackGround.UpdateUiBackground(storage.storedTurretSelectedRarity);
    }

    public TMP_Text GetUpgradeText(int index)
    {
        if (index >= 0 && index < allTextUis.Length)
        {
            return allTextUis[index];
        }
        
        Debug.LogWarning($"Invalid upgrade text index: {index}");
        return null;
    }
}