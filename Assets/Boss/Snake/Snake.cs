using System;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    public Transform segmentPrefab; // Prefab for the snake's body segments
    public float speed = 5f; // Speed of the snake
    public int initialSize = 4; // Initial number of body segments
    public bool moveThroughWalls = false; // Whether the snake can move through walls
    public float segmentSpacing = 0.5f; // Spacing between body segments
    public float minDistanceBeforeTurn = 3f; // Minimum distance to travel before turning
    public float maxDistanceBeforeTurn = 8f; // Maximum distance to travel before turning

    private readonly List<Transform> segments = new List<Transform>(); // List of body segments
    private readonly List<Vector3> positionsHistory = new List<Vector3>(); // History of positions for smooth movement
    private Vector2 direction = Vector2.left; // Current movement direction
    private float distanceTraveled = 0f; // Distance traveled in the current direction
    private float distanceBeforeTurn; // Distance to travel before turning
    private bool nextTurnIsUp = true; // Tracks whether the next turn should be up or down

    private void Start()
    {
        ResetState();
        // Set the initial distance before turning
        distanceBeforeTurn = Random.Range(minDistanceBeforeTurn, maxDistanceBeforeTurn);
        
        GetComponent<BoxCollider2D>().isTrigger = true;

    }

    private void Update()
    {
        // Move the snake's head smoothly
        transform.position += (Vector3)direction * (speed * Time.deltaTime);

        // Update the distance traveled
        distanceTraveled += speed * Time.deltaTime;

        // Check if it's time to change direction
        if (distanceTraveled >= distanceBeforeTurn)
        {
            ChangeDirection();
            distanceTraveled = 0f; // Reset distance traveled
            distanceBeforeTurn = Random.Range(minDistanceBeforeTurn, maxDistanceBeforeTurn); // Set new distance before turning
        }

        // Store the head's position in the history
        positionsHistory.Insert(0, transform.position);

        // Move the body segments to follow the head with a delay
        for (int i = 0; i < segments.Count; i++)
        {
            // Calculate the target position for each segment based on the history
            int index = Mathf.Min(i * Mathf.RoundToInt(segmentSpacing * 10), positionsHistory.Count - 1);
            segments[i].position = positionsHistory[index];
        }

        // Remove old positions from the history to keep it from growing indefinitely
        if (positionsHistory.Count > segments.Count * Mathf.RoundToInt(segmentSpacing * 10))
        {
            positionsHistory.RemoveAt(positionsHistory.Count - 1);
        }
    }

    private void ChangeDirection()
    {
        if (direction == Vector2.left)
        {
            // Alternate between up and down after moving left
            if (nextTurnIsUp)
            {
                direction = Vector2.up;
            }
            else
            {
                direction = Vector2.down;
            }
            nextTurnIsUp = !nextTurnIsUp; // Toggle for the next turn
        }
        else
        {
            // Always go left after moving up or down
            direction = Vector2.left;
        }
    }

    public void Grow()
    {
        // Add a new body segment
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    public void ResetState()
    {
        // Reset the snake to its initial state
        direction = Vector2.left;
        transform.position = Vector3.zero;

        // Destroy all body segments except the head
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        // Clear the list and add the head
        segments.Clear();
        segments.Add(transform);

        // Clear the positions history
        positionsHistory.Clear();
        positionsHistory.Add(transform.position);

        // Add initial body segments
        for (int i = 0; i < initialSize - 1; i++)
        {
            Grow();
        }

        // Reset the turn toggle
        nextTurnIsUp = true;
    }

    public bool Occupies(int x, int y)
    {
        // Check if the snake occupies a specific position
        foreach (Transform segment in segments)
        {
            if (Mathf.RoundToInt(segment.position.x) == x &&
                Mathf.RoundToInt(segment.position.y) == y)
            {
                return true;
            }
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("SnakeWall") || other.gameObject.CompareTag("WaterCollision"))
        {
            ChangeDirection();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*
        if (other.gameObject.CompareTag("Obstacle"))
        {
            ResetState();
        }
        */
        
        if (other.gameObject.CompareTag("SnakeWall") || other.gameObject.CompareTag("WaterCollision"))
        {
            ChangeDirection();
            
            /*
            print("yo");
            if (moveThroughWalls)
            {
                Traverse(other.transform);
            }
            else
            {
                ChangeDirection(); // Change direction when hitting a wall
            }
            */
        }
    }

    private void Traverse(Transform wall)
    {
        // Move the snake to the opposite side of the wall
        Vector3 position = transform.position;

        if (direction.x != 0f)
        {
            position.x = -wall.position.x + direction.x;
        }
        else if (direction.y != 0f)
        {
            position.y = -wall.position.y + direction.y;
        }

        transform.position = position;
    }
}