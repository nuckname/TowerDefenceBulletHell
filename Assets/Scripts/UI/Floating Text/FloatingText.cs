using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float lifetime = 1f;
    private TextMeshPro textMesh;

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshPro>();
        Destroy(gameObject, lifetime);
    }

    public void SetText(string text, Color color)
    {
        if (textMesh != null)
        {
            textMesh.text = text;
            textMesh.color = color;
        }
        else
        {
            Debug.LogError("No TextMeshPro found on FloatingText prefab!");
        }
    }

    private void Update()
    {
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        transform.LookAt(Camera.main.transform); // Always face the player
    }
}