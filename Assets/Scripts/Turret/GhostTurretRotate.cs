using UnityEngine;

public class GhostTurretRotate : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 180f; // degrees per scroll “notch”
    public Quaternion savedRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Start facing “up” (0° around Z)
        transform.rotation = Quaternion.Euler(0, 0, 0);
        savedRotation      = transform.rotation;
    }


    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.0001f)
        {
            Debug.Log($"Scroll delta: {scroll:F3}");

            transform.Rotate(0f, 0f, -scroll * scrollSpeed);

            savedRotation = transform.rotation;
        }
 
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Convert 0-360 range to -180 to +180 for easier comparisons
            float rawZ = transform.eulerAngles.z;
            float signedZ = rawZ > 180f ? rawZ - 360f : rawZ;

            if (signedZ == 0 || signedZ == 180 || signedZ == -90 || signedZ == 90)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, signedZ + -90f);
                return;
            }
            
            float snappedZ;
            // Snap ranges as specified:
            if (signedZ >= 18f  && signedZ <= 36f)  snappedZ = 0f;
            else if (signedZ >= 54f  && signedZ <= 72f)  snappedZ = 90f;
            else if (signedZ >= 108f && signedZ <= 126f) snappedZ = 90f;
            else if (signedZ >= 144f && signedZ <= 162f) snappedZ = 180f;
            else if (signedZ <= -18f  && signedZ >= -36f)  snappedZ = 0f;
            else if (signedZ <= -54f  && signedZ >= -72f)  snappedZ = -90f;
            else if (signedZ <= -108f && signedZ >= -126f) snappedZ = -90f;
            else if (signedZ <= -144f && signedZ >= -162f) snappedZ = 180f;
            // Fallback: nearest 90° multiple
            else snappedZ = Mathf.Round(signedZ / 90f) * 90f;

            transform.rotation = Quaternion.Euler(0f, 0f, snappedZ);
            savedRotation      = transform.rotation;
            Debug.Log($"Raw Z={signedZ:F1}°, snapped to {snappedZ}°");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            savedRotation = transform.rotation;
            Debug.Log("Rotation Saved: " + savedRotation.eulerAngles);
        }

    }
}
