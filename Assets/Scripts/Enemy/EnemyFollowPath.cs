using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPath : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Transform[] waypoints; // Local cached waypoints array
    private int waypointIndex = 0;

    private void Start()
    {
        SetUpWaypointsDesret();
        //SetUpWaypointsBeachTutorial();
    }

    private void SetUpWaypointsDesret()
    {
        // Find MapPath and cache its waypoints
        MapPath mapPath = GameObject.FindObjectOfType<MapPath>();
        if (mapPath != null && mapPath.waypointsDesretMap.Length > 0)
        {
            waypoints = mapPath.waypointsDesretMap;
            transform.position = waypoints[waypointIndex].position;
        }
        else
        {
            Debug.LogError("MapPath or waypoints are not set up correctly.");
        }
    }
    
    private void SetUpWaypointsBeachTutorial()
    {
        // Find MapPath and cache its waypoints
        MapPath mapPath = GameObject.FindObjectOfType<MapPath>();
        if (mapPath != null && mapPath.waypointsBeachTutorial.Length > 0)
        {
            waypoints = mapPath.waypointsBeachTutorial;
            transform.position = waypoints[waypointIndex].position;
        }
        else
        {
            Debug.LogError("MapPath or waypoints are not set up correctly.");
        }
    }
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Move towards the current waypoint if there are waypoints defined
        if (waypoints != null && waypointIndex < waypoints.Length)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                waypoints[waypointIndex].position,
                moveSpeed * Time.deltaTime
            );

            if (transform.position.x == waypoints[waypointIndex].position.x)
            {
                if (transform.position.y == waypoints[waypointIndex].position.y)
                {
                    waypointIndex += 1;
                }
            }
        }
    }
}