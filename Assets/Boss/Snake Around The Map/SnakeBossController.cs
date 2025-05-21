using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SnakeBossState { Normal, Enraged, FollowPath, Dead }

public class SnakeBossController : MonoBehaviour
{
    [Header("State Machine Settings")]
    public SnakeBossState currentState = SnakeBossState.Normal;

    [Header("Follow Movement Speed")]
    public float normalMoveSpeed = 2f;
    public float enragedMoveSpeedMultiplier = 1.5f;
    public float followMoveSpeedMultiplier = 0.75f;
    
    [SerializeField]
    private float currentMoveSpeed;
    
    public int lapCount = 0;
    public float lastWaypointThreshold = 3f;
    private bool countedLastWaypointThisLap = false;

    [Header("Waypoints")]
    private Transform[] waypoints;  // Assign via Inspector
    public int currentWaypointIndex = 0;

    [Header("Following Body Segments")]
    public int numberOfBodySegments = 5;
    public GameObject bodySegmentPrefab;  // Prefab with shooting behavior & tag "SnakeBody"
    public float distanceBetweenSegments = 0.5f;
    public List<Transform> bodySegments = new List<Transform>();

    [Header("Static Body Segments (Tail)")]
    public float staticSegmentSpawnInterval = 3f;
    public List<Transform> staticBodySegments = new List<Transform>();
    private Coroutine staticSegmentSpawnCoroutine;

    [Header("Segment Follow Settings")]
    public float segmentFollowSpeed = 5f;
    public float stopFollowDistance = 0.1f;
    
    [Header("Timer")]
    [SerializeField] private GameObject snakeTimer;
    private GameObject spawnedSnakeTimer;
    [SerializeField] private BossDie bossDie;
    
    [SerializeField] private GameObject VictoryScreen;
    [SerializeField] private TutorialStateSO tutorialStateSO;

    [Header("Health")]
    [SerializeField] private int snakeBosshealth = 300;

    private int waypointIndex = 0;
    
    private EnemyFollowPath enemyFollowPath;
    private BossHealth bossHealth;
    
    [Header("SpawnPoint")]
    [SerializeField] private GameObject spawnPoint;
    
    private void Awake()
    {
        enemyFollowPath = GetComponent<EnemyFollowPath>();
        bossHealth = GetComponent<BossHealth>();

        GameObject waypointParent = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MapPath>().waypointsSnakeBoss;

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
    }


    private void Start()
    {
        currentState = SnakeBossState.Normal;
        currentMoveSpeed = normalMoveSpeed;
        SpawnBodySegments();  
        staticSegmentSpawnCoroutine = StartCoroutine(SpawnStaticSegmentRoutine());
        
        Canvas canvas = GameObject.FindGameObjectWithTag("canvas").GetComponent<Canvas>();
        spawnedSnakeTimer = Instantiate(snakeTimer, canvas.transform);

        enemyFollowPath.enabled = false;
        bossHealth.enabled = false;
    }

    private void Update()
    {
        switch (currentState)
        {
            case SnakeBossState.Normal:
                NormalUpdate();
                break;
            case SnakeBossState.Enraged:
                EnragedUpdate();
                break;
            case SnakeBossState.FollowPath:
                FollowPath();
                break;
            case SnakeBossState.Dead:
                // (Do nothing or add death behavior)
                break;
        }

        // Move the following segments regardless of state.
        MoveSegments();
    }

    // NORMAL STATE: Move at normal speed and spawn static segments.
    private void NormalUpdate()
    {
        MoveHead(currentMoveSpeed);
        
        // When reaching the last waypoint, transition to Enraged state.
        if (currentWaypointIndex == waypoints.Length - 1 && 
            Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < lastWaypointThreshold)
        {
            TransitionToEnraged();
        }
    }


    private void StartFollowPath()
    {
        //Spawn HP bar and stuf
        
        MoveHead(currentMoveSpeed * followMoveSpeedMultiplier);

        bossHealth.enabled = true;
        
        enemyFollowPath.enabled = enabled;

        bossHealth.SpawnHealthBar(snakeBosshealth);
        
        currentState = SnakeBossState.FollowPath;
    }
    
    private void FollowPath()
    {
        
    }
    
    // ENRAGED STATE: Move and attack faster, and count laps.
    private void EnragedUpdate()
    {
        MoveHead(currentMoveSpeed * enragedMoveSpeedMultiplier);
        
        // Count laps: when near the last waypoint, increment the lap counter once.
        if (currentWaypointIndex == waypoints.Length - 1 &&
            Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < lastWaypointThreshold)
        {
            if (!countedLastWaypointThisLap)
            {
                lapCount++;
                countedLastWaypointThisLap = true;
                Debug.Log("Lap " + lapCount + " completed in Enraged state.");

                // After two laps, destroy the boss head.
                if (lapCount >= 2)
                {
                    
                    StartFollowPath();
                    

                }
            }
        }
        else
        {
            countedLastWaypointThisLap = false;
        }
    }

    // Moves the snake head toward the current waypoint using the provided speed.
    private void MoveHead(float speed)
    {
        if (waypoints.Length == 0)
            return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // When close to the target waypoint, move to the next one.
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    // Makes following segments smoothly follow the snake head.
    private void MoveSegments()
    {
        Vector3 previousPosition = transform.position;
        foreach (Transform segment in bodySegments)
        {
            if (Vector3.Distance(segment.position, previousPosition) > stopFollowDistance)
            {
                segment.position = Vector3.Lerp(segment.position, previousPosition, segmentFollowSpeed * Time.deltaTime);
            }
            previousPosition = segment.position;
        }
        // Note: Static body segments remain fixed.
    }
    

    // Transition from Normal to Enraged: stop spawning static segments and increase speed.
    private void TransitionToEnraged()
    {
        if (staticSegmentSpawnCoroutine != null)
        {
            StopCoroutine(staticSegmentSpawnCoroutine);
            staticSegmentSpawnCoroutine = null;
        }
        currentState = SnakeBossState.Enraged;
        Debug.Log("Snake Boss is enraged!");
    }

    // Destroys the snake boss head.
    public void DestroyBoss()
    {
        currentState = SnakeBossState.Dead;
        
        foreach (Transform snakebody in bodySegments)
        {
            Destroy(snakebody.gameObject); 
        }

        GameObject[] buggedLeftOverGameObjects = GameObject.FindGameObjectsWithTag("SnakeBody");
        
        //bug, doesnt clear all gameobjects
        foreach (GameObject snakebody in buggedLeftOverGameObjects)
        {
            Destroy(snakebody); 
        }

        bossDie.EnemyHasDied();
        /*
        GameObject canvas = GameObject.FindGameObjectWithTag("canvas");
        if (canvas != null)
        {
            GameObject victory = Instantiate(VictoryScreen, transform.position, Quaternion.identity);
            victory.transform.SetParent(canvas.transform, false);
            tutorialStateSO.playerTutorial = false;
        }
        */
        
        Destroy(gameObject);
    }

    // Spawns a new static body segment every staticSegmentSpawnInterval seconds.
    private IEnumerator SpawnStaticSegmentRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(staticSegmentSpawnInterval);
            SpawnStaticBodySegment();
        }
    }

    // Spawns a static body segment at the tail of the snake (only in Normal state).
    private void SpawnStaticBodySegment()
    {
        if (currentState != SnakeBossState.Normal)
            return;

        GameObject newSegment = Instantiate(bodySegmentPrefab, 
            new Vector3(bodySegments[bodySegments.Count - 1].position.x,
                bodySegments[bodySegments.Count - 1].position.y, 
                bodySegments[bodySegments.Count - 1].position.z + 1
                ), Quaternion.identity);
        
        newSegment.tag = "SnakeBody";
        staticBodySegments.Add(newSegment.transform);
    }

    // Spawns the initial following body segments behind the head.
    private void SpawnBodySegments()
    {
        if (bodySegmentPrefab == null)
        {
            Debug.LogError("Body segment prefab is not assigned!");
            return;
        }

        bodySegments.Clear();
        Vector3 spawnDirection = -transform.right;
        Vector3 lastPosition = transform.position;

        for (int i = 0; i < numberOfBodySegments; i++)
        {
            Vector3 spawnPos = lastPosition + spawnDirection * distanceBetweenSegments;
            GameObject segment = Instantiate(bodySegmentPrefab, spawnPos, Quaternion.identity);
            bodySegments.Add(segment.transform);
            lastPosition = spawnPos;
        }
    }

    // On collision: in Enraged state, if the snake head hits a snake body, destroy that body segment.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SnakeBody"))
        {
            Destroy(collision.gameObject);
            if (staticBodySegments.Contains(collision.transform))
                staticBodySegments.Remove(collision.transform);
            if (bodySegments.Contains(collision.transform))
                bodySegments.Remove(collision.transform);
        }
        
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (currentState == SnakeBossState.FollowPath)
            {
                bossHealth.TakeDamage(1);
                Destroy(collision.gameObject); 
            }
            
        }
    }
}
