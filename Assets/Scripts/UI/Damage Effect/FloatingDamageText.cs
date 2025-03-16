using TMPro;
using UnityEngine;

public class FloatingDamageText : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float fadeSpeed = 1f;
    private TextMeshProUGUI text;
    private Color originalColor;

    private RectTransform rectTransform;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        originalColor = text.color;
        
        rectTransform = this.GetComponent<RectTransform>();
        
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        rectTransform.localPosition += Vector3.up * moveSpeed * Time.deltaTime;
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, text.color.a - (fadeSpeed * Time.deltaTime)); // Fade out
    }
}
