using System.Collections;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [SerializeField, TextArea]
    private string debug_message_active;
    
    [SerializeField, TextArea]
    private string debug_message_target;
    
    [Header("Homing Settings")]
    public float homingDelay    = 0.5f;
    public float homingStrength = 10f;
    public float homingRadius   = 10f;
    public float speed          = 8f;

    [Tooltip("Set by the firing turret: does this shot home?")]
    public bool useHoming = true;

    private Rigidbody2D rb;
    private Transform   target;
    private bool        isHomingActive;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isHomingActive = false;
        target         = null;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        isHomingActive = false;

        if (useHoming)
        {
            // 1) Actually assign the closest target here:
            target = FindClosestTarget("Enemy");
            StartCoroutine(ActivateHoming());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        debug_message_active = isHomingActive.ToString();
        debug_message_target = target != null ? target.name : "target is null";
    }

    private void FixedUpdate()
    {
        if (!useHoming || !isHomingActive)
            return;

        // 2) Re-acquire (and assign) the closest enemy each physics step
        Transform newTarget = FindClosestTarget("Enemy");
        if (newTarget != null)
            target = newTarget;

        if (target == null)
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

    /// <summary>
    /// Returns the closest Transform with the given tag within homingRadius, or null if none found.
    /// </summary>
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
    
    public void ResetHomingState()
    {
        StopAllCoroutines();
        isHomingActive = false;
        target         = null;
        rb.linearVelocity    = Vector2.zero;
    }

}
