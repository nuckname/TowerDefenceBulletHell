using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectVariationManager : MonoBehaviour
{
    [SerializeField] private SelectMapManager selectMapManager;

    [SerializeField] private Image[] imagesInCavas;
    
    [SerializeField] private Sprite[] desertThumbNails;
    [SerializeField] private Sprite[] ruinThumbNails;
    [SerializeField] private Sprite[] snowThumbNails;

    private void Start()
    {
        UpdateVariationMapSelector(selectMapManager.mapSelected);
    }

    public void UpdateVariationMapSelector(SelectMapManager.MapType mapSelected)
    {
        
        if (mapSelected == SelectMapManager.MapType.RuinMap)
        {
            UpdateThumbNailImages(ruinThumbNails);
        }
        
        else if (mapSelected == SelectMapManager.MapType.DesertMap)
        {
            UpdateThumbNailImages(desertThumbNails);
        }
        
        else if (mapSelected == SelectMapManager.MapType.SnowMap)
        {
            UpdateThumbNailImages(snowThumbNails);
        }
    }

    private void UpdateThumbNailImages(Sprite[] thumbnails)
    {
        for (int i = 0; i < thumbnails.Length; i++)
        {
            imagesInCavas[i].sprite = thumbnails[i];
        }
    }
}
