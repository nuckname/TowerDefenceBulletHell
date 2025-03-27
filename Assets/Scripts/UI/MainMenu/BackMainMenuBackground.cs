using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackMainMenuBackground : MonoBehaviour
{
    public Image backgroundImage;
    public List<Sprite> backgroundSprites;

    private void Start()
    {
        if (backgroundSprites.Count > 0 && backgroundImage != null)
        {
            int randomIndex = Random.Range(0, backgroundSprites.Count);
            backgroundImage.sprite = backgroundSprites[randomIndex];
        }
    }

}
