using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;
    public SpriteRenderer onDeathSpriteRenderer;
    
    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}