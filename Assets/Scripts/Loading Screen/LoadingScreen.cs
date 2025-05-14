using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoadingScreen : MonoBehaviour
{
    private Canvas loadingCanvas;
    private Image backgroundImage;
    private Image barBackground;
    private Image barFill;

    // Keep track of other canvases to re-enable later
    private List<Canvas> otherCanvases = new List<Canvas>();

    void Start()
    {
        // 1) Find and disable all existing Canvases (e.g. your game UI)
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            otherCanvases.Add(c);
            c.gameObject.SetActive(false);
        }

        // 2) Create our loading UI
        CreateLoadingUI();

        // 3) Begin loading (simulation or real async call)
        StartCoroutine(SimulateLoading());
    }

    private void CreateLoadingUI()
    {
        // Create Canvas
        loadingCanvas = new GameObject("LoadingCanvas").AddComponent<Canvas>();
        loadingCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        loadingCanvas.gameObject.AddComponent<CanvasScaler>();
        loadingCanvas.gameObject.AddComponent<GraphicRaycaster>();

        // Full-screen black background
        backgroundImage = new GameObject("Background").AddComponent<Image>();
        backgroundImage.transform.SetParent(loadingCanvas.transform, false);
        RectTransform bgRect = backgroundImage.rectTransform;
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.offsetMin = Vector2.zero;
        bgRect.offsetMax = Vector2.zero;
        backgroundImage.color = Color.black;

        // Loading bar background
        barBackground = new GameObject("BarBackground").AddComponent<Image>();
        barBackground.transform.SetParent(loadingCanvas.transform, false);
        RectTransform barBgRect = barBackground.rectTransform;
        barBgRect.sizeDelta = new Vector2(400f, 30f);
        barBgRect.anchorMin = barBgRect.anchorMax = new Vector2(0.5f, 0.5f);
        barBgRect.anchoredPosition = Vector2.zero;
        barBackground.color = new Color(1f, 1f, 1f, 0.3f);

        // Loading bar fill
        barFill = new GameObject("BarFill").AddComponent<Image>();
        barFill.transform.SetParent(barBackground.transform, false);
        RectTransform fillRect = barFill.rectTransform;
        fillRect.anchorMin = new Vector2(0f, 0f);
        fillRect.anchorMax = new Vector2(0f, 1f);
        fillRect.offsetMin = Vector2.zero;
        fillRect.offsetMax = Vector2.zero;
        barFill.color = Color.white;
    }

    private IEnumerator SimulateLoading()
    {
        float progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime * 0.4f;
            SetProgress(progress);
            yield return null;
        }

        // Loading complete: destroy loading UI
        Destroy(loadingCanvas.gameObject);

        //  Re-enable previous game UI
        foreach (Canvas c in otherCanvases)
        {
            if (c != null)
                c.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Call this to update the fill amount (0 to 1).
    /// </summary>
    public void SetProgress(float value)
    {
        float clamped = Mathf.Clamp01(value);
        barFill.rectTransform.anchorMax = new Vector2(clamped, 1f);
    }
}
