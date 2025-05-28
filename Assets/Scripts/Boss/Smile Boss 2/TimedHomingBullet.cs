using UnityEngine;
using System.Collections;

public class TimedHomingBullet : MonoBehaviour
{
    //This is on snake btw
    [Header("Homing Settings")]
    public float homingDelay = 1f;       // Delay before homing starts
    public float homingDuration = 1f;    // How long the bullet homes in
    public float homingStrength = 5f;    // How aggressively it turns toward the target
    public float speed = 5f;            // Base speed of the bullet

    [Header("Target Settings")]
    private Rigidbody2D rb;
    private Transform target;
    private bool isHomingActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = FindClosestTarget("Player");

        StartCoroutine(HomingRoutine());
    }

    void FixedUpdate()
    {
        // If homing is active and the target exists, adjust velocity toward the target.
        if (isHomingActive && target != null)
        {
            Vector2 direction = ((Vector2)target.position - rb.position).normalized;
            Vector2 newVelocity = Vector2.Lerp(rb.linearVelocity, direction * speed, homingStrength * Time.fixedDeltaTime);
            rb.linearVelocity = newVelocity;
        }
        // Otherwise, the bullet continues in its current direction.
    }

    IEnumerator HomingRoutine()
    {
        // Wait for the initial delay before starting homing.
        yield return new WaitForSeconds(homingDelay);
        isHomingActive = true;

        // Homing is active for a set duration.
        yield return new WaitForSeconds(homingDuration);
        isHomingActive = false;
    }

    // Finds the closest target with the specified tag.
    private Transform FindClosestTarget(string tag)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject potentialTarget in targets)
        {
            float distance = Vector2.Distance(transform.position, potentialTarget.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = potentialTarget.transform;
            }
        }
        return closest;
    }
}
