using UnityEngine;

public class ScreenFlashOnDamage : MonoBehaviour
{
    [Header("Flash Settings")]
    [Tooltip("How long the flash stays visible (seconds)")]
    public float flashDuration = 2f;
    [Tooltip("What fraction of screen width each bar covers")]
    [Range(0.02f, 0.3f)]
    public float barWidthPercent = 0.1f;

    private float flashTimer = 0f;
    private Texture2D redTex;

    void Awake()
    {
        // Create a 1×1 red texture for drawing
        redTex = new Texture2D(1, 1);
        redTex.SetPixel(0, 0, Color.red);
        redTex.Apply();
    }

    void Update()
    {
        // Countdown the flash timer
        if (flashTimer > 0f)
            flashTimer -= Time.deltaTime;
    }

    void OnGUI()
    {
        if (flashTimer <= 0f) 
            return;

        // Alpha fades out over time
        float alpha = flashTimer / flashDuration;

        Color prevColor = GUI.color;
        GUI.color = new Color(1f, 0f, 0f, alpha);

        float w = Screen.width * barWidthPercent;
        float h = Screen.height;

        // Left bar
        GUI.DrawTexture(new Rect(0, 0, w, h), redTex);
        // Right bar
        GUI.DrawTexture(new Rect(Screen.width - w, 0, w, h), redTex);

        GUI.color = prevColor;
    }

    /// <summary>
    /// Call this from your damage-taking code.
    /// </summary>
    public void TakeDamage()
    {
        flashTimer = flashDuration;
    }
}