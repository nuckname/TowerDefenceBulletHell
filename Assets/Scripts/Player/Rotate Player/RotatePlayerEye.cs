using System;
using UnityEngine;

public class RotatePlayerEye : MonoBehaviour
{
    void Update()
    {
        // Get mouse position in world space
        Vector3 taretDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(taretDir.y, taretDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
