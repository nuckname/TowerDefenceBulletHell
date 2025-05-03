using System;
using System.Collections;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [Header("Homing Settings")]
    public float homingDelay    = 0.5f;
    public float homingStrength = 10f;
    public float homingRadius   = 10f;
    public float speed          =   8f;

    [Tooltip("Set by the firing turret: does this shot home?")]
    public bool  useHoming      = true;

    private Rigidbody2D rb;
    private Transform   target;
    private bool        isHomingActive;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        isHomingActive = false;
        target         = null;
    }

    private void Start()
    {
        isHomingActive = false;
        target         = null;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        isHomingActive = false;

        if (useHoming)
            StartCoroutine(ActivateHoming());
    }

    private void OnDisable()
    {
        // ensure coroutines are dead when we drop back in the pool
        StopAllCoroutines();
    }

    private void FixedUpdate()
    {
        if (!useHoming || !isHomingActive || target == null)
            return;

        // steer toward the (possibly re-acquired) target
        Vector2 desired = ((Vector2)target.position - rb.position).normalized * speed;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, desired, homingStrength * Time.fixedDeltaTime);
    }

    private IEnumerator ActivateHoming()
    {
        yield return new WaitForSeconds(homingDelay);
        isHomingActive = true;
    }

    private Transform FindClosestTarget(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        Transform   best     = null;
        float       bestDist = homingRadius;

        foreach (var e in enemies)
        {
            float d = Vector2.Distance(transform.position, e.transform.position);
            if (d < bestDist)
            {
                bestDist = d;
                best     = e.transform;
            }
        }

        return best;
    }
}
