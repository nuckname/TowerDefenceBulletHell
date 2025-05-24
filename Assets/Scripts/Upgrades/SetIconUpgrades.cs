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
        print("turretRotation: " + turretRotation);
        Debug.LogWarning("RotateImage Code Here");
        float rawZ = transform.eulerAngles.z;
        float signedZ = rawZ > 180f ? rawZ - 360f : rawZ;
        Quaternion rot;

        print("t: " + turretRotation);
        if (turretRotation == 90 || turretRotation == 180 || turretRotation == -90 || turretRotation == 0)
        {
            print("yo");
        }
        
        if (signedZ == 0 || signedZ == 180 || signedZ == -90 || signedZ == 90)
        {
            Debug.LogWarning("Set rotation image");
            rot = Quaternion.Euler(0f, 0f, turretRotation);
            
            imageBoxTop.rectTransform.localRotation    = rot;
            imageBoxMiddle.rectTransform.localRotation = rot;
            imageBoxBottom.rectTransform.localRotation = rot;
        
            float wideW = 1079f, wideH = 14f;
            SetRectSize(imageBoxTop.rectTransform,    wideW, wideH);
            SetRectSize(imageBoxMiddle.rectTransform, wideW, wideH);
            SetRectSize(imageBoxBottom.rectTransform, wideW, wideH);
        }
    }

    
    /// <summary>
    /// Assigns upgrade icons, aligns them to the turret's rotation, and enforces square dimensions.
    /// </summary>
    public void SetIcons(string[] upgradeDescriptions, string raritySelected, float turretRotation)
    {
        //Turret Rotation. Using mouse wheel also breaks it. 
        
        if (raritySelected != "Normal Rarity"
            && !descriptionsToNotRotate.Contains(upgradeDescriptions[0])
            && !descriptionsToNotRotate.Contains(upgradeDescriptions[1])
            && !descriptionsToNotRotate.Contains(upgradeDescriptions[2]))
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