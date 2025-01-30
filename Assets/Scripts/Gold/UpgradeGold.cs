using TMPro;
using UnityEngine;

public class UpgradeGold : MonoBehaviour
{
    public TMP_Text DisplayUpgradeGoldAmount;

    [SerializeField] private int NormalUpgradePriece = 250;
    [SerializeField] private int RareUpgradePriece = 300;
    [SerializeField] private int LegendaryUpgradePriece = 450;
    
    public int DisplayGold(string rarity)
    {
        if (rarity == "Normal Rarity")
        {
            DisplayUpgradeGoldAmount.text = "$" + NormalUpgradePriece.ToString();
            return NormalUpgradePriece;
        }
        
        else if (rarity == "Rare Rarity")
        {
            DisplayUpgradeGoldAmount.text = "$" + RareUpgradePriece.ToString();
            return RareUpgradePriece;
        }
        
        else if (rarity == "Legendary Rarity")
        {
            DisplayUpgradeGoldAmount.text = "$" + LegendaryUpgradePriece.ToString();
            return LegendaryUpgradePriece;
        }

        Debug.LogError("Prie Error");
        return NormalUpgradePriece;
    }
}
