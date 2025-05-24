using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetIconUpgrades : MonoBehaviour
{
    [SerializeField] private Image imageBoxTop;
    [SerializeField] private Image imageBoxMiddle;
    [SerializeField] private Image imageBoxBottom;

    private RotateTurretIcon rotateTurretIcon;
    
    [SerializeField] private List<string> descriptionsToNotRotate = new List<string>
    {
        "Gives 5 dollar per bullet hit.",
        "Greatly increase fire rate speed",
        "Greatly increase projectile speed",
        "Greating increase bullet speed",
        "Bullets home to a target",
        "Projectiles pierce through enemies"
    };
    
    [SerializeField] private GameObject turret;
    private UpgradeDataOnTurret upgradeDataOnTurret;
    
    private void Awake()
    {
        upgradeDataOnTurret = turret.GetComponent<UpgradeDataOnTurret>();
        rotateTurretIcon = GetComponent<RotateTurretIcon>();
        
        // Ensure anchors are fixed to a single point to avoid layout stretching
        FixAnchors(imageBoxTop.rectTransform);
        FixAnchors(imageBoxMiddle.rectTransform);
        FixAnchors(imageBoxBottom.rectTransform);
    }

    private void SetRectSize(RectTransform rt, float width, float height)
    {
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    private void RotateImage(float turretRotation)
    {

        
        
    }


    
    /// <summary>
    /// Assigns upgrade icons, aligns them to the turret's rotation, and enforces square dimensions.
    /// </summary>
    public void SetIcons(string[] upgradeDescriptions, string raritySelected, float turretRotation)
    {
        //Turret Rotation. Using mouse wheel also breaks it. 
        
        if (raritySelected != "Normal Rarity")
        {
            RotateImage(turretRotation);
        }
        
        
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