using TMPro;
using UnityEngine;

public class UpgradeGold : MonoBehaviour
{
    public TMP_Text DisplayUpgradeGoldAmount;

    [SerializeField] private int NormalUpgradePrice = 250;
    [SerializeField] private int RareUpgradePriece = 300;
    [SerializeField] private int LegendaryUpgradePriece = 450;
    
    public int DisplayGold(string rarity, int totalUpgradeAmount)
    {
        if (rarity == "Normal Rarity")
        {
            DisplayUpgradeGoldAmount.text = "$" + Mathf.RoundToInt(NormalUpgradePrice + 5f * totalUpgradeAmount).ToString();

            return NormalUpgradePrice;
        }
        
        else if (rarity == "Rare Rarity")
        {
            DisplayUpgradeGoldAmount.text = "$" + Mathf.RoundToInt(RareUpgradePriece + 5f * totalUpgradeAmount).ToString();
            return RareUpgradePriece;
        }
        
        else if (rarity == "Legendary Rarity")
        {
            DisplayUpgradeGoldAmount.text = "$" + Mathf.RoundToInt(LegendaryUpgradePriece + 5f * totalUpgradeAmount).ToString();
            return LegendaryUpgradePriece;
        }

        Debug.LogError("Prie Error");
        return NormalUpgradePrice;
    }
}
