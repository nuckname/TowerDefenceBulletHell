using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnEffectOnEnemyGroundPosition : MonoBehaviour
{
    [Header("Effect Settings")]
    [Tooltip("Prefab to be spawned")]
    [SerializeField] protected GameObject effectPrefab;

    [Tooltip("Seconds between spawns")]
    [SerializeField] protected float spawnInterval = 1f;

    [Tooltip("Delay after interval before effect actually appears")]
    [SerializeField] protected float spawnDelay = 0.25f;
    
    protected virtual List<GameObject> SpawnedEffects { get; } = new();
    
    private float _timer;

    /// <summary>
    /// Subclasses can override to shift the spawn's Z position
    /// </summary>
    protected virtual float ZOffset => 0f;

    /// <summary>
    /// Called when the interval elapses; kicks off the delayed spawn coroutine.
    /// </summary>
    protected virtual void Spawn()
    {
        StartCoroutine(SpawnAfterDelay());
    }

    private IEnumerator SpawnAfterDelay()
    {
        Vector3 origin = transform.position + Vector3.up * 1f;
        Vector3 spawnPos = transform.position;

        if (Physics.Raycast(origin, Vector3.down, out var hit, 5f))
            spawnPos = hit.point;

        spawnPos.z += ZOffset;

        yield return new WaitForSeconds(spawnDelay);
        
        GameObject spawnedEffect = Instantiate(effectPrefab, spawnPos, Quaternion.identity);
        SpawnedEffects.Add(spawnedEffect);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= spawnInterval)
        {
            _timer -= spawnInterval;
            Spawn();
        }
    }
}