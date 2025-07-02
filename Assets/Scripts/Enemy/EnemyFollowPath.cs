using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPath : MonoBehaviour, ISpeedModifiable
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;

    [Tooltip("If true, start at the last waypoint and march backwards")]
    public bool reverse = false;

    public Transform[] waypoints;
    public int currentWaypoint = 0;

    [HideInInspector]
    public bool skipInitialPositioning = false;

    private void Start()
    {
        // Load waypoints from your MapPath
        GetWaypointTransformFromGameObject();

        // If running in reverse, start at the end of the path
        if (reverse && waypoints != null && waypoints.Length > 0)
        {
            currentWaypoint = waypoints.Length - 1;
            if (!skipInitialPositioning)
                transform.position = waypoints[currentWaypoint].position;
        }
    }

    public void ModifySpeed(float multiplier)
    {
        moveSpeed *= multiplier;
    }

    private void GetWaypointTransformFromGameObject()
    {
        var mapPath = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MapPath>();
        if (mapPath == null || mapPath.waypointsDesretMediumMap == null)
        {
            Debug.LogError("MapPath or waypoint parent is missing.");
            return;
        }

        GameObject waypointParent = mapPath.waypointsDesretMediumMap;
        List<Transform> waypointList = new List<Transform>();
        foreach (Transform child in waypointParent.transform)
            waypointList.Add(child);

        waypoints = waypointList.ToArray();

        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoint children found under the waypoint parent object.");
        }
        else if (!skipInitialPositioning && !reverse)
        {
            // Initial positioning for forward movement
            transform.position = waypoints[currentWaypoint].position;
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        // Bound-check currentWaypoint
        if (currentWaypoint < 0 || currentWaypoint >= waypoints.Length)
            return;

        Vector3 target = waypoints[currentWaypoint].position;
        transform.position = Vector2.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, target) < 0.05f)
        {
            if (!reverse)
            {
                currentWaypoint++;
                // if past end, clamp to last
                if (currentWaypoint >= waypoints.Length)
                    currentWaypoint = waypoints.Length - 1;
            }
            else
            {
                currentWaypoint--;
                // if before start, clamp to zero
                if (currentWaypoint < 0)
                    currentWaypoint = 0;
            }
        }
    }

    /// <summary>
    /// Force-loads the waypoint array if needed, then picks the closest forward or backward index.
    /// </summary>
    public void SetWaypointIndexFromPosition(Vector3 position)
    {
        if (waypoints == null || waypoints.Length == 0)
            GetWaypointTransformFromGameObject();

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("Cannot set waypoint index - waypoints array is null or empty");
            return;
        }

        currentWaypoint = reverse
            ? GetClosestBackwardWaypointIndex(position)
            : GetClosestForwardWaypointIndex(position);

        Debug.Log($"Set enemy waypoint index to: {currentWaypoint}");
    }

    /// <summary>
    /// Standard forward: find the nearest waypoint, bump one ahead if very close.
    /// </summary>
    private int GetClosestForwardWaypointIndex(Vector3 position)
    {
        int bestIdx = 0;
        float bestDist = float.MaxValue;

        for (int i = 0; i < waypoints.Length; i++)
        {
            float d = Vector3.Distance(waypoints[i].position, position);
            if (d < bestDist)
            {
                bestDist = d;
                bestIdx = i;
            }
        }

        if (bestDist < 0.5f && bestIdx < waypoints.Length - 1)
            bestIdx++;

        return bestIdx;
    }

    /// <summary>
    /// Reverse: find the nearest waypoint going backward, bump one behind if very close.
    /// </summary>
    private int GetClosestBackwardWaypointIndex(Vector3 position)
    {
        int bestIdx = waypoints.Length - 1;
        float bestDist = float.MaxValue;

        for (int i = waypoints.Length - 1; i >= 0; i--)
        {
            float d = Vector3.Distance(waypoints[i].position, position);
            if (d < bestDist)
            {
                bestDist = d;
                bestIdx = i;
            }
        }

        if (bestDist < 0.5f && bestIdx > 0)
            bestIdx--;

        return bestIdx;
    }
}
