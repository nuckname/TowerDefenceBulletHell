using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPath : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Transform[] waypoints; 
    private int waypointIndex = 0;

    private void Start()
    {
        GetWaypointTransformFromGameObject();
    }

    public void SetUpMap()
    {
        //remove Start()
        //Pass in somehting not sure.
        //Then call GetWaypointTransformFromGameObject() and find correct game object.
        
    }

    private void GetWaypointTransformFromGameObject()
    {
        
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

            if (waypoints.Length > 0)
            {
                transform.position = waypoints[waypointIndex].position;
            }
            else
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
        if (waypoints != null && waypointIndex < waypoints.Length)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                waypoints[waypointIndex].position,
                moveSpeed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.05f)
            {
                waypointIndex++;
            }

/*            
            if (transform.position.x == waypoints[waypointIndex].position.x)
            {
                if (transform.position.y == waypoints[waypointIndex].position.y)
                {
                    waypointIndex += 1;
                }
            }
  */
        }
    }
}