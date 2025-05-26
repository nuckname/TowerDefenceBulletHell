using UnityEngine;
using UnityEngine.UI;

public class ScreenFlashOnDamage : MonoBehaviour
{
    [Header("Flash Settings")]
    public float flashDuration = 2f;

    [Tooltip("UI Image on the left side of the screen")]
    public Image flashImage;

    private float flashTimer = 0f;

    void Start()
    {
        SetImageAlpha(flashImage, 0f);
    }

    void Update()
    {
        if (flashTimer > 0f)
        {
            flashTimer -= Time.deltaTime;

            float alpha = flashTimer / flashDuration;
            SetImageAlpha(flashImage, alpha);

            if (flashTimer <= 0f)
            {
                SetImageAlpha(flashImage, 0f);
            }
        }
    }

    public void TakeDamage()
    {
        flashTimer = flashDuration;

        SetImageAlpha(flashImage, 1f);
    }

    private void SetImageAlpha(Image img, float alpha)
    {
        if (img != null)
        {
            Color c = img.color;
            c.a = alpha;
            img.color = c;
        }
    }
}