using UnityEngine;
using UnityEngine.UI;

public class RandomBackgroundMainMenu : MonoBehaviour
{
    public Image image; 
    public Sprite[] backgroundImages; 

    void Start()
    {
        if (backgroundImages.Length > 0)
        {
            int randomIndex = Random.Range(0, backgroundImages.Length);
            image.sprite = backgroundImages[randomIndex]; 
        }
    }

}
