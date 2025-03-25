using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUiColourBackGround : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private List<Sprite> changeBackgroundColorApplyUpgrade;

    public void UpdateUiBackground(string selectedRarity)
    {
        switch (selectedRarity)
        {
            case "Legendary Rarity":
                image.sprite = changeBackgroundColorApplyUpgrade[2];
                break;
            case "Rare Rarity":
                image.sprite = changeBackgroundColorApplyUpgrade[1];
                break;
            case "Normal Rarity":
                image.sprite = changeBackgroundColorApplyUpgrade[0];
                break;
            default:
                Debug.LogWarning("Invalid rarity selected: " + selectedRarity);
                break;
        }
    }
}
