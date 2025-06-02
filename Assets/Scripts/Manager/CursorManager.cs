using System;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D canClickTexture;

    
    
    private void Awake()
    {
        ChanceCursor(cursorTexture);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void ChanceCursor(Texture2D cursorType)
    {
        
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
}
