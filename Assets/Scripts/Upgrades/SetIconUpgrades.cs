using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetIconUpgrades : MonoBehaviour
{
    [SerializeField] private Image imageBoxTop;
    [SerializeField] private Image imageBoxMiddle;
    [SerializeField] private Image imageBoxBottom;

    [SerializeField] private GameObject turret;
    private UpgradeDataOnTurret upgradeDataOnTurret;
    
    private void Awake()
    {
        upgradeDataOnTurret = turret.GetComponent<UpgradeDataOnTurret>();

        // Ensure anchors are fixed to a single point to avoid layout stretching
        FixAnchors(imageBoxTop.rectTransform);
        FixAnchors(imageBoxMiddle.rectTransform);
        FixAnchors(imageBoxBottom.rectTransform);
    }

    /// <summary>
    /// Assigns upgrade icons, aligns them to the turret's rotation, and enforces square dimensions.
    /// </summary>
    public void SetIcons(string[] upgradeDescriptions, string raritySelected, float turretRotation)
    {
        // Desired icon size
        /*
        const float iconSize = 100f;
        SetSquareSize(imageBoxTop.rectTransform, iconSize);
        SetSquareSize(imageBoxMiddle.rectTransform, iconSize);
        SetSquareSize(imageBoxBottom.rectTransform, iconSize);

        // Rotate each icon to match turret
        Vector3 zRot = new Vector3(0f, 0f, turretRotation);
        imageBoxTop.rectTransform.localEulerAngles = zRot;
        imageBoxMiddle.rectTransform.localEulerAngles = zRot;
        imageBoxBottom.rectTransform.localEulerAngles = zRot;

        */
        
        // Helper to assign sprite based on description list
        void Assign(Image img, List<Upgrade> list, string desc)
        {
            foreach (var up in list)
                if (up.description == desc)
                {
                    img.sprite = up.upgradeIcon;
                    break;
                }
        }

        switch (raritySelected)
        {
            case "Legendary Rarity":
                Assign(imageBoxTop, upgradeDataOnTurret.legendaryUpgrades, upgradeDescriptions[0]);
                Assign(imageBoxMiddle, upgradeDataOnTurret.legendaryUpgrades, upgradeDescriptions[1]);
                Assign(imageBoxBottom, upgradeDataOnTurret.legendaryUpgrades, upgradeDescriptions[2]);
                break;
            case "Rare Rarity":
                Assign(imageBoxTop, upgradeDataOnTurret.rareUpgrades, upgradeDescriptions[0]);
                Assign(imageBoxMiddle, upgradeDataOnTurret.rareUpgrades, upgradeDescriptions[1]);
                Assign(imageBoxBottom, upgradeDataOnTurret.rareUpgrades, upgradeDescriptions[2]);
                break;
            case "Normal Rarity":
                Assign(imageBoxTop, upgradeDataOnTurret.normalUpgrades, upgradeDescriptions[0]);
                Assign(imageBoxMiddle, upgradeDataOnTurret.normalUpgrades, upgradeDescriptions[1]);
                Assign(imageBoxBottom, upgradeDataOnTurret.normalUpgrades, upgradeDescriptions[2]);
                break;
        }
    }

    // Utility: force anchors to center so parent layouts don't stretch the RectTransform
    private void FixAnchors(RectTransform rt)
    {
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
    }

    // Utility: set both width and height to the same fixed size
    private void SetSquareSize(RectTransform rt, float size)
    {
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
    }
}
