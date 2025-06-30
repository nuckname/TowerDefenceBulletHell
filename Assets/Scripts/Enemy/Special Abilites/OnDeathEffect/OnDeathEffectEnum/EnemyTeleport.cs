using UnityEngine;

public class EnemyTeleport : MonoBehaviour
{
    [SerializeField] private Transform _target;
    public void TeleportEnemyOnCollision()
    {
        if (_target == null)
        {
            GameObject receiver = GameObject.FindGameObjectWithTag("teleportReceiver");
            if (receiver != null)
            {
                _target = receiver.transform;
                Debug.Log("Teleport receiver found and cached.");
            }
            else
            {
                Debug.LogWarning("No object with tag 'teleportReceiver' found!");
                return;
            }
        }

        if (_target != null)
        {
            transform.root.position = _target.position;
            UpdateEnemyWayPoint(gameObject, _target);
            gameObject.GetComponentInParent<EnemyCollision>().enemyHasUsedTeleporter = true;
        }
        else
        {
            Debug.LogWarning("Teleport target was never set.");
        }
    }

    
    private void UpdateEnemyWayPoint(GameObject enemy, Transform location)
    {
        EnemyFollowPath path = enemy.GetComponentInParent<EnemyFollowPath>();
        if (path != null)
        {
            int updatedIndex = path.GetClosestForwardWaypointIndex(location.position);
            path.currentWaypoint = updatedIndex;
        }
        else
        {
            Debug.LogWarning("Enemy missing EnemyFollowPath component after teleport.");
        }

    }
}
