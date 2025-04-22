using UnityEngine;

public class Figure8Bullet : MonoBehaviour
{
    [Header("Orbit Parameters")]
    [Tooltip("Center point to orbit around (turret position)")]
    public Vector3 pivot;

    [Tooltip("Horizontal loop radius")]
    public float radiusX = 2f;
    [Tooltip("Vertical loop radius")]
    public float radiusY = 1f;
    [Tooltip("How many loops per second")]
    public float loopsPerSecond = 0.5f;

    private float _t;

    void OnEnable()
    {
        // reset timer so each bullet starts at the same point
        _t = 0f;
    }

    void Update()
    {
        // advance parametric time
        _t += Time.deltaTime * loopsPerSecond * 2f * Mathf.PI;

        // Lemniscate (figure‑8) in parametric form:
        // x = radiusX * sin(t)
        // y = radiusY * sin(2t) / 2
        float x = Mathf.Sin(_t) * radiusX;
        float y = Mathf.Sin(2f * _t) * (radiusY * 0.5f);

        transform.position = pivot + new Vector3(x, y, 0f);
    }
}