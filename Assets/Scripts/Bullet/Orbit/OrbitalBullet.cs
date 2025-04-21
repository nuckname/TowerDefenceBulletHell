using UnityEngine;

public class OrbitalBullet : MonoBehaviour
{
    private CircleCollider2D orbitCollider;
    private Vector2 orbitCenter;
    private float   orbitRadius;
    private float   orbitSpeed;
    
    private bool isOrbiting  = false;
    private bool isCapturing = false;
    private float angle;       // For when we actually start orbiting
    
    [SerializeField] private float captureSpeed = 5f; 

    private void OnEnable()
    {
        isOrbiting        = false;
        isCapturing       = false;
        orbitCollider     = null;
    }
    
    // Called by OrbitTrigger when this bullet first enters
    public void StartOrbiting(CircleCollider2D newOrbitCollider, float speed, float distanceMultiplier = 1f)
    {
        orbitCollider = newOrbitCollider;
        orbitCenter   = orbitCollider.bounds.center;
        orbitRadius   = orbitCollider.radius * distanceMultiplier;
        orbitSpeed    = speed;
        
        isCapturing = true;
        isOrbiting  = false;
    }

    void Update()
    {
        if (isCapturing)
        {
            Vector2 dir  = ((Vector2)transform.position - orbitCenter).normalized;
            Vector2 goal = orbitCenter + dir * orbitRadius;

            transform.position = Vector2.MoveTowards(
                transform.position,
                goal,
                captureSpeed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, goal) < 0.01f)
            {
                isCapturing = false;
                BeginTrueOrbit(dir);
            }
        }
        else if (isOrbiting)
        {
            angle += orbitSpeed * Time.deltaTime;
            if (angle >= 360f) angle -= 360f;

            float   rad    = angle * Mathf.Deg2Rad;
            Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * orbitRadius;
            transform.position = orbitCenter + offset;
        }
    }

    private void BeginTrueOrbit(Vector2 entryDir)
    {
        angle      = Mathf.Atan2(entryDir.y, entryDir.x) * Mathf.Rad2Deg;
        isOrbiting = true;
    }

    public void StopOrbiting()
    {
        isOrbiting   = false;
        isCapturing  = false;
        orbitCollider = null;
    }
}
