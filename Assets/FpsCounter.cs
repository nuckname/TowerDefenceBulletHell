using TMPro;
using UnityEngine;
using System.Collections;

public class FPSCounter : MonoBehaviour
{
    //Check fps every two second becuase it annyoing flickering. 
    public TextMeshProUGUI fpsText;  
    private float fps;

    void Start()
    {
        StartCoroutine(UpdateFPS());
    }

    IEnumerator UpdateFPS()
    {
        while (true)
        {
            fps = 1.0f / Time.unscaledDeltaTime; 
            fpsText.text = Mathf.Ceil(fps) + " FPS";
            yield return new WaitForSeconds(2f); 
        }
    }
}