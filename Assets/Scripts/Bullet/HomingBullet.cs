using UnityEngine;
using System.Collections;

public class HomingBullet : MonoBehaviour
{
    public float homingDelay = 1f;
    public float homingStrength = 5f;  // Adjust how aggressively it turns
    public float speed = 10f;  // Bullet speed
    private Rigidbody2D rb;
    private Transform target;
    private bool isHomingActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = FindClosestTarget("Enemy");  // Find the closest enemy
        
        StartCoroutine(ActivateHoming());  // Start homing after delay
    }

    void FixedUpdate()
    {
        if (isHomingActive && target != null)
        {
            Vector2 direction = ((Vector2)target.position - rb.position).normalized;
            Vector2 newVelocity = Vector2.Lerp(rb.linearVelocity, direction * speed, homingStrength * Time.fixedDeltaTime);
            rb.linearVelocity = newVelocity;
        }
    }

    IEnumerator ActivateHoming()
    {
        yield return new WaitForSeconds(homingDelay);
        isHomingActive = true;
    }

    private Transform FindClosestTarget(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }
        return closest;
    }
}