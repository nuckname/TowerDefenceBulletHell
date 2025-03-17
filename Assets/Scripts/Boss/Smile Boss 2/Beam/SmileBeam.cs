using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmileBeam : MonoBehaviour
{
public LineRenderer warningLine; // Telegraph indicator
    public LineRenderer laserBeam;   // Actual laser
    public float warningTime = 1.5f; // Time before firing
    public float beamDuration = 3f;  // Active beam time
    public float sweepSpeed = 90f;   // Degrees per second
    public float beamDamage = 10f;   // Damage per second

    private bool isFiring = false;
    private float currentAngle = 0f;
    private Vector3 pivotPoint;
    
    void Start()
    {
        pivotPoint = transform.position; // Set rotation pivot at boss position
        warningLine.enabled = false;
        laserBeam.enabled = false;
    }

    public void StartFrownBeam(float startAngle, float endAngle)
    {
        if (!isFiring)
        {
            currentAngle = startAngle;
            StartCoroutine(FireFrownBeam(startAngle, endAngle));
        }
    }   

    private IEnumerator FireFrownBeam(float startAngle, float endAngle)
    {
        isFiring = true;

        // Step 1: Show warning telegraph
        warningLine.enabled = true;
        SetLineRenderer(warningLine, startAngle);
        yield return new WaitForSeconds(warningTime);

        // Step 2: Fire the beam
        warningLine.enabled = false;
        laserBeam.enabled = true;
        SetLineRenderer(laserBeam, startAngle);

        float direction = Mathf.Sign(endAngle - startAngle); // Determine sweep direction
        float elapsed = 0f;

        while (elapsed < beamDuration)
        {
            currentAngle += direction * sweepSpeed * Time.deltaTime;
            currentAngle = Mathf.Clamp(currentAngle, Mathf.Min(startAngle, endAngle), Mathf.Max(startAngle, endAngle));

            SetLineRenderer(laserBeam, currentAngle);
            DealDamage(); // Check if player is hit

            if (Mathf.Approximately(currentAngle, endAngle)) break; // Stop at final angle

            elapsed += Time.deltaTime;
            yield return null;
        }

        laserBeam.enabled = false;
        isFiring = false;
    }

    private void SetLineRenderer(LineRenderer line, float angle)
    {
        Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
        line.SetPosition(0, pivotPoint);
        line.SetPosition(1, pivotPoint + direction * 10f); // Adjust laser length
    }

    private void DealDamage()
    {
        RaycastHit2D hit = Physics2D.Raycast(pivotPoint, laserBeam.GetPosition(1) - pivotPoint, 10f);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            
            // Apply damage over time
            //hit.collider.GetComponent<PlayerHealth>()?.TakeDamage(beamDamage * Time.deltaTime);
        }
    }

}
