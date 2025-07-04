using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUiColourBackGround : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private List<Sprite> changeBackgroundColorApplyUpgrade;

    public void UpdateUiBackground(TurretRarity selectedRarity)
    {
        switch (selectedRarity)
        {
            case TurretRarity.Legendary:
                image.sprite = changeBackgroundColorApplyUpgrade[2];
                break;
            case TurretRarity.Rare:
                image.sprite = changeBackgroundColorApplyUpgrade[1];
                break;
            case TurretRarity.Normal:
                image.sprite = changeBackgroundColorApplyUpgrade[0];
                break;
            default:
                Debug.LogWarning("Invalid rarity selected: " + selectedRarity);
                break;
        }
    }
}
