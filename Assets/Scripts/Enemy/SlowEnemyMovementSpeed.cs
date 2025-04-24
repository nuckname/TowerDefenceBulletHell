using System.Collections;
using UnityEngine;

public class SlowEnemyMovementSpeed : MonoBehaviour
{
        
    public bool IsSlowed { get; private set; } = false;
    private float originalSpeed;
    private Coroutine slowCoroutine;
    private EnemyFollowPath enemyFollowPath;
    
    public void StartSlow(float slowAmount, float duration)
    {
        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine);

        slowCoroutine = StartCoroutine(SlowCoroutine(slowAmount, duration));
    }

    private IEnumerator SlowCoroutine(float slowAmount, float duration)
    {
        float moveSpeed = enemyFollowPath.moveSpeed;
        IsSlowed = true;
        originalSpeed = moveSpeed;
        moveSpeed *= (1f - slowAmount); // slowAmount should be 0.3f for 30%, etc.
    
        yield return new WaitForSeconds(duration);

        moveSpeed = originalSpeed;
        IsSlowed = false;
    }
}
