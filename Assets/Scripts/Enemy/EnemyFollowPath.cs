using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPath : MonoBehaviour, ISpeedModifiable
{
    public float moveSpeed = 2f;
    private Transform[] waypoints; 
    public int currentWaypoint = 0;

    [HideInInspector]
    public bool skipInitialPositioning = false;

    private void Start()
    {
        GetWaypointTransformFromGameObject();
    }
    
    public void ModifySpeed(float multiplier)
    {
        moveSpeed *= multiplier;
    }

    private void GetWaypointTransformFromGameObject()
    {
        Debug.Log("Only works for DesretMediumMap");
        
        GameObject waypointParent = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MapPath>().waypointsDesretMediumMap;

        if (waypointParent != null)
        {
            // Get all children transforms, but skip the parent itself
            List<Transform> waypointList = new List<Transform>();
            foreach (Transform child in waypointParent.transform)
            {
                waypointList.Add(child);
            }

            waypoints = waypointList.ToArray();

            // Only set initial position if we're not skipping initial positioning (for respawned enemies)
            if (waypoints.Length > 0 && !skipInitialPositioning)
            {
                transform.position = waypoints[currentWaypoint].position;
            }
            else if (waypoints.Length == 0)
            {
                Debug.LogError("No waypoint children found under the waypoint parent object.");
            }
        }
        else
        {
            Debug.LogError("waypointsDesretMediumMap is null on MapPath.");
        }
    }
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Move towards the current waypoint if there are waypoints defined
        if (waypoints != null && currentWaypoint < waypoints.Length)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                waypoints[currentWaypoint].position,
                moveSpeed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, waypoints[currentWaypoint].position) < 0.05f)
            {
                currentWaypoint++;
            }
        }
    }
    
    public void SetWaypointIndexFromPosition(Vector3 position)
    {
        // Ensure waypoints are initialized before trying to use them
        if (waypoints == null)
        {
            GetWaypointTransformFromGameObject();
        }
        
        if (waypoints != null && waypoints.Length > 0)
        {
            currentWaypoint = GetClosestForwardWaypointIndex(position);
            Debug.Log($"Set enemy waypoint index to: {currentWaypoint}");
        }
        else
        {
            Debug.LogError("Cannot set waypoint index - waypoints array is null or empty");
        }
    }

    /// <summary>
    /// Returns the index of the waypoint that the enemy should move to next based on position.
    /// Always ensures forward progress along the path.
    /// </summary>
    public int GetClosestForwardWaypointIndex(Vector3 position)
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("Cannot get closest waypoint - waypoints array is null or empty");
            return 0;
        }

        int bestIndex = 0;
        float closestDistance = float.MaxValue;

        // Find the closest waypoint to our position
        for (int i = 0; i < waypoints.Length; i++)
        {
            float distance = Vector3.Distance(waypoints[i].position, position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                bestIndex = i;
            }
        }

        // If we found a waypoint that's very close (enemy is basically on it), 
        // move to the next waypoint to ensure forward progress
        if (closestDistance < 0.5f && bestIndex < waypoints.Length - 1)
        {
            bestIndex++;
        }

        Debug.Log($"Death position: {position}, Closest waypoint: {bestIndex} at {waypoints[bestIndex].position}, Distance: {closestDistance}");
        return bestIndex;
    }
    
    
    /// <summary>
    /// Finds the closest waypoint *at or after* minIndex.
    /// If nothing is closer, returns minIndex+1 (to guarantee forward progress).
    /// </summary>
    public int GetClosestForwardWaypointIndexFrom(int minIndex, Vector3 position)
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("Waypoints array is empty!");
            return 0;
        }

        Debug.Log($"GetClosestForwardWaypointIndexFrom → minIndex={minIndex}, deathPos={position}");
        for (int i = 0; i < waypoints.Length; i++)
        {
            Debug.Log($"  waypoint[{i}] @ {waypoints[i].position}");
        }

        // clamp
        minIndex = Mathf.Clamp(minIndex, 0, waypoints.Length - 1);
        Debug.Log($"  clamped minIndex → {minIndex}");

        int best = -1;
        float bestDist = float.MaxValue;

        // **only search from minIndex onward** (or minIndex+1, see next section)
        for (int i = minIndex; i < waypoints.Length; i++)
        {
            float d = Vector3.Distance(waypoints[i].position, position);
            Debug.Log($"    dist to waypoint[{i}] = {d}");
            if (d < bestDist)
            {
                bestDist = d;
                best = i;
            }
        }

        Debug.Log($"  raw best → index={best}, dist={bestDist}");
        if (best == minIndex && best < waypoints.Length - 1)
        {
            best++;
            Debug.Log($"  bumped forward to {best}");
        }

        return best >= 0 ? best : 0;
    }

}