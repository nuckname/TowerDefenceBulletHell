using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBoss : MonoBehaviour
{
    public GameObject bodySegmentPrefab; // Prefab for body segments
    public GameObject projectilePrefab;  // Prefab for projectiles
    public int initialSegments = 5;      // Starting body count
    public float moveSpeed = 3f;
    public float turnSpeed = 90f;
    public float fireRate = 2f;
    public float segmentSpacing = 1.5f;  // Distance between segments
    public int segmentHealth = 3;

    private List<Transform> bodySegments = new List<Transform>();
    private Vector3 moveDirection = Vector3.forward; // Initial move direction
    private float nextFireTime = 0f;
    private List<Vector3> previousPositions = new List<Vector3>(); // Tracks movement for segments

    void Start()
    {
        CreateBody(initialSegments);
    }

    void Update()
    {
        Move();
        HandleShooting();
    }

    void Move()
    {
        // Save the current head position before moving
        previousPositions.Insert(0, transform.position);
        if (previousPositions.Count > bodySegments.Count + 1)
        {
            previousPositions.RemoveAt(previousPositions.Count - 1);
        }

        // Move the head forward
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Move each body segment to follow the previous one
        for (int i = 0; i < bodySegments.Count; i++)
        {
            bodySegments[i].position = Vector3.Lerp(bodySegments[i].position, previousPositions[i], 0.5f);
        }
    }

    void HandleShooting()
    {
        if (Time.time >= nextFireTime)
        {
            foreach (Transform segment in bodySegments)
            {
                //Instantiate(projectilePrefab, segment.position, Quaternion.identity);
            }
            nextFireTime = Time.time + fireRate;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // If hitting a wall, turn 90 degrees
        if (collision.gameObject.CompareTag("Wall"))
        {
            moveDirection = Quaternion.Euler(0, 90, 0) * moveDirection;
        }
    }

    public void DamageSegment(GameObject segment)
    {
        int index = bodySegments.IndexOf(segment.transform);
        if (index >= 0)
        {
            bodySegments.RemoveAt(index);
            Destroy(segment);
        }

        // Split if body is broken
        if (index > 0)
        {
            SplitSnake(index);
        }
    }

    void SplitSnake(int splitIndex)
    {
        GameObject newSnake = Instantiate(gameObject, bodySegments[splitIndex].position, Quaternion.identity);
        SnakeBoss newSnakeScript = newSnake.GetComponent<SnakeBoss>();

        // Assign half of the remaining body segments to the new snake
        newSnakeScript.bodySegments = bodySegments.GetRange(splitIndex, bodySegments.Count - splitIndex);
        bodySegments.RemoveRange(splitIndex, bodySegments.Count - splitIndex);
    }

    void CreateBody(int count)
    {
        Vector3 spawnPos = transform.position;
        for (int i = 0; i < count; i++)
        {
            spawnPos -= moveDirection * segmentSpacing;
            GameObject segment = Instantiate(bodySegmentPrefab, spawnPos, Quaternion.identity);
            bodySegments.Add(segment.transform);
        }
    }
}
