using UnityEngine;
using System.Collections;

public class HomingBullet : MonoBehaviour
{
    public float homingDelay = 0.0f;
    public float homingStrength = 100f;
    public float homingRadius = 100f;
    public float speed = 8;  
    
    private Rigidbody2D rb;

    [SerializeField] private Transform target;
    [SerializeField] private bool isHomingActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = FindClosestTarget("Enemy");  
        if (target != null)
        {
            target = FindClosestTarget("Enemy"); 
        }

        StartCoroutine(ActivateHoming());  
    }

    void FixedUpdate()
    {
        if (!isHomingActive)
        {
            return;
        }
        
        if (target == null)
        {
            target = FindClosestTarget("Enemy"); 
            return;
        }

        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        Vector2 newVelocity = Vector2.Lerp(rb.linearVelocity, direction * speed, homingStrength * Time.fixedDeltaTime);
        rb.linearVelocity = newVelocity;
    }

    IEnumerator ActivateHoming()
    {
        print("Waiting for homing");
        yield return new WaitForSeconds(homingDelay);
        print("Activating homing");
        isHomingActive = true;
    }

    private Transform FindClosestTarget(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        Transform closest = null;
        float minDistance = homingRadius;
        
        if (enemies.Length == 0)
        {
            Debug.LogWarning("[HomingBullet] No enemies found in the scene.");
        }

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            Debug.Log($"[HomingBullet] Enemy found");
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }

        if (closest != null)
        {
            Debug.Log($"[HomingBullet] Enemy found");
        }
        else
        {
            Debug.LogWarning("[HomingBullet] No valid target within range.");
        }

        return closest;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, homingRadius);
    }
}
