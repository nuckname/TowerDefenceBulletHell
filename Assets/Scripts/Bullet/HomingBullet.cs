// HomingBullet.cs

using System;
using System.Collections;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [Header("Debug Messages")]
    [SerializeField, TextArea]
    private string debug_message_active;
    [SerializeField, TextArea]
    private string debug_message_target;

    [Header("Homing Settings")]
    [Tooltip("Delay before homing starts (seconds)")]
    public float homingDelay = 0.5f;

    [Tooltip("Speed at which the bullet homes on the target")]
    public float homingSpeed = 4f;

    [Tooltip("Toggle homing behavior")]
    public bool useHoming = true;

    [Tooltip("Tag of targets to home in on")]
    [SerializeField]
    private string targetTag = "Enemy";

    [Tooltip("Should the bullet re-acquire the closest target each step?")]
    public bool allowRetargeting = false;

    private Rigidbody2D rb;
    private Transform target;
    private bool isHomingActive;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isHomingActive = false;
        target = null;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        isHomingActive = false;
        target = FindClosestTarget(targetTag);
        if (useHoming)
            StartCoroutine(ActivateHoming());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        rb.linearVelocity = Vector2.zero;
    }

    private void Update()
    {
        debug_message_active = isHomingActive.ToString();
        debug_message_target = target != null ? target.name : "null";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Assuming bullet "hits" and should find next closest target
            Transform secondClosest = FindSecondClosestTargetExcluding(targetTag, other.transform);
            if (secondClosest != null)
            {
                target = secondClosest;
                isHomingActive = true; // Continue homing on new target immediately
            }
            else
            {
                // No other targets found, maybe stop homing
                ResetHomingState();
            }
        }
    }
    
    private Transform FindSecondClosestTargetExcluding(string tag, Transform exclude)
    {
        GameObject[] candidates = GameObject.FindGameObjectsWithTag(tag);
        Transform closest = null;
        Transform secondClosest = null;
        float closestDist = float.MaxValue;
        float secondClosestDist = float.MaxValue;

        foreach (var obj in candidates)
        {
            if (obj.transform == exclude)
                continue;

            float dist = Vector2.Distance(transform.position, obj.transform.position);
            if (dist < closestDist)
            {
                secondClosest = closest;
                secondClosestDist = closestDist;

                closest = obj.transform;
                closestDist = dist;
            }
            else if (dist < secondClosestDist)
            {
                secondClosest = obj.transform;
                secondClosestDist = dist;
            }
        }
        return secondClosest;
    }

    private void FixedUpdate()
    {
        if (!useHoming || !isHomingActive)
            return;

        // Ensure we have a target
        if (target == null || allowRetargeting)
        {
            Transform newT = FindClosestTarget(targetTag);
            if (newT != null)
                target = newT;
        }
        if (target == null)
            return;

        // Steer directly towards the target by setting velocity
        Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * homingSpeed;
    }

    private IEnumerator ActivateHoming()
    {
        yield return new WaitForSeconds(homingDelay);
        isHomingActive = true;
    }

    /// <summary>
    /// Finds the closest Transform with the given tag.
    /// </summary>
    private Transform FindClosestTarget(string tag)
    {
        GameObject[] candidates = GameObject.FindGameObjectsWithTag(tag);
        Transform best = null;
        float minDist = float.MaxValue;

        foreach (var obj in candidates)
        {
            float d = Vector2.Distance(transform.position, obj.transform.position);
            if (d < minDist)
            {
                minDist = d;
                best = obj.transform;
            }
        }
        return best;
    }

    /// <summary>
    /// Resets homing state and stops motion.
    /// </summary>
    public void ResetHomingState()
    {
        StopAllCoroutines();
        isHomingActive = false;
        target = null;
        rb.linearVelocity = Vector2.zero;
    }
}
