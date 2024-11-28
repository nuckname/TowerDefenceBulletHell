using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateShopText : MonoBehaviour
{
    [SerializeField] private TMP_Text upgrade1Name;
    [SerializeField] private TMP_Text upgrade1Desc;
    
    [SerializeField] private TMP_Text upgrade2Name;
    [SerializeField] private TMP_Text upgrade2Desc;
    
    [SerializeField] private TMP_Text upgrade3Name;
    [SerializeField] private TMP_Text upgrade3Desc;

    private readonly string[] upgrades = { "BulletLife", "Projectile", "Damage" };
    

    private string PickUpgrade()
    {
        int randomIndex = Random.Range(0, upgrades.Length); 
        return upgrades[randomIndex];
    }

    private void SetUpgradeText(TMP_Text nameText, TMP_Text descText, string upgrade)
    {
        switch (upgrade)
        {
            case "BulletLife":
                nameText.text = "Bullet Life";
                descText.text = "Extends the bullet's lifetime. Useful for hitting far-away targets.";
                break;
            case "Projectile":
                nameText.text = "Projectile";
                descText.text = "Enhances projectile mechanics, increasing speed and precision.";
                break;
            case "Damage":
                nameText.text = "Damage";
                descText.text = "Increases damage dealt by each bullet.";
                break;
            default:
                nameText.text = "Unknown Upgrade";
                descText.text = "No description available.";
                break;
        }
    }

    public void GenerateUpgrades()
    {
        string upgrade1 = PickUpgrade();
        SetUpgradeText(upgrade1Name, upgrade1Desc, upgrade1);
        
        string upgrade2 = PickUpgrade();
        SetUpgradeText(upgrade2Name, upgrade2Desc, upgrade2);

        string upgrade3 = PickUpgrade();
        SetUpgradeText(upgrade3Name, upgrade3Desc, upgrade3);
    }
}
