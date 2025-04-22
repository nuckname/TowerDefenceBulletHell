using System.Collections;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [Header("Homing Settings")]
    public float homingDelay    = 0.0f;
    public float homingStrength = 100f;
    public float homingRadius   = 100f;
    public float speed          =   8f;

    [Tooltip("Set by the firing turret: does this shot home?")]
    public bool  useHoming      = true;

    private Rigidbody2D rb;
    private Transform   target;
    private bool        isHomingActive;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // First, fully clear any old homing state

//        StopAllCoroutines();
        isHomingActive = false;
        target         = null;

        if (useHoming)
        {
            // grab a fresh target
            target = FindClosestTarget("Enemy");
            // initial “lock‑on” direction (or fallback straight)
            Vector2 dir = target != null
                ? ((Vector2)target.position - (Vector2)transform.position).normalized
                : transform.right;
            rb.linearVelocity = dir * speed;

            // kick off the homing delay
            StartCoroutine(ActivateHoming());
        }
        else
        {
            // No homing: just fire straight
            rb.linearVelocity = transform.right * speed;
        }
    }

    private void OnDisable()
    {
        // ensure coroutines are dead when we drop back in the pool
        StopAllCoroutines();
    }

    private void FixedUpdate()
    {
        if (!useHoming || !isHomingActive)
            return;

        // if our lock died/out of range, re‑acquire
        if (target == null)
        {
            target = FindClosestTarget("Enemy");
            if (target == null)
                return;  // nothing to home on, keep current velocity
        }

        // steering
        Vector2 desired = ((Vector2)target.position - rb.position).normalized * speed;
        rb.linearVelocity     = Vector2.Lerp(rb.linearVelocity, desired, homingStrength * Time.fixedDeltaTime);
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
