using UnityEngine;

public class debugCameraPos : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        print(cam.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
