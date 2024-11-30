using UnityEngine;
using System.Collections.Generic;

public class GeneratePath : MonoBehaviour
{
    [Header("Path Generation Parameters")]
    [SerializeField] private int minTurns = 3;    // Minimum number of turns
    [SerializeField] private int maxTurns = 10;   // Maximum number of turns
    [SerializeField] private float minSegmentLength = 2f; // Minimum segment length
    [SerializeField] private float maxSegmentLength = 10f; // Maximum segment length
    [SerializeField] private Vector2 startPoint = new Vector2(0, 0); // Start position
    [SerializeField] private Direction startDirection = Direction.North; // Starting direction
    [SerializeField] private Direction endDirection = Direction.South;   // Finishing direction

    [Header("Debugging")]
    [SerializeField] private bool drawPathInEditor = true;

    public List<Vector2> waypoints = new List<Vector2>();

    private void Start()
    {
        PathGeneration();
    }

    public void PathGeneration()
    {
        waypoints.Clear();

        // Start the path
        Vector2 currentPosition = startPoint;
        waypoints.Add(currentPosition);

        // Randomize the number of turns
        int turns = Random.Range(minTurns, maxTurns + 1);
        Direction currentDirection = startDirection;

        for (int i = 0; i < turns; i++)
        {
            // Determine segment length
            float segmentLength = Random.Range(minSegmentLength, maxSegmentLength);

            // Generate the next point
            Vector2 nextPosition = GetNextPosition(currentPosition, currentDirection, segmentLength);
            waypoints.Add(nextPosition);

            // Place enemy transform objects at corners
            CreateEnemyTransform(currentPosition);

            // Update position
            currentPosition = nextPosition;

            // Change direction randomly (avoiding doubling back)
            currentDirection = GetNewDirection(currentDirection);
        }

        // Add final point heading towards the end direction
        Vector2 finalPosition = GetNextPosition(currentPosition, endDirection, Random.Range(minSegmentLength, maxSegmentLength));
        waypoints.Add(finalPosition);

        // Create transform at the final point
        CreateEnemyTransform(finalPosition);

        Debug.Log("Path Generated!");
    }

    private Vector2 GetNextPosition(Vector2 current, Direction direction, float length)
    {
        switch (direction)
        {
            case Direction.North:
                return current + new Vector2(0, length);
            case Direction.South:
                return current + new Vector2(0, -length);
            case Direction.East:
                return current + new Vector2(length, 0);
            case Direction.West:
                return current + new Vector2(-length, 0);
            default:
                return current;
        }
    }

    private Direction GetNewDirection(Direction currentDirection)
    {
        Direction newDirection;

        do
        {
            newDirection = (Direction)Random.Range(0, 4);
        } while (newDirection == OppositeDirection(currentDirection));

        return newDirection;
    }

    private Direction OppositeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                return Direction.South;
            case Direction.South:
                return Direction.North;
            case Direction.East:
                return Direction.West;
            case Direction.West:
                return Direction.East;
            default:
                return Direction.North;
        }
    }

    private void CreateEnemyTransform(Vector2 position)
    {
        // Spawn an enemy transform object at this position
        GameObject enemyTransform = new GameObject("EnemyTransform");
        enemyTransform.transform.position = position;
        enemyTransform.transform.parent = this.transform;
    }

    private void OnDrawGizmos()
    {
        if (drawPathInEditor && waypoints.Count > 1)
        {
            Gizmos.color = Color.red;

            for (int i = 0; i < waypoints.Count - 1; i++)
            {
                Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
                Gizmos.DrawSphere(waypoints[i], 0.2f);
            }

            Gizmos.DrawSphere(waypoints[waypoints.Count - 1], 0.2f);
        }
    }

    public enum Direction
    {
        North,
        South,
        East,
        West
    }
}
