using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceExplosionZone : MonoBehaviour
{
    [SerializeField] private float duration = 3f;
    [SerializeField] private float radius = 3f;

    private List<TurretStats> affectedTurrets = new List<TurretStats>();
    private List<PlayerMovement> affectedPlayers = new List<PlayerMovement>();

    private void Start()
    {
        // Scale the zone's visual and collider
        transform.localScale = new Vector3(radius * 2, radius * 2, 1);
        StartCoroutine(RemoveEffectsAfterDelay());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Turret"))
        {
            TurretStats turret = other.GetComponent<TurretStats>();
            if (turret != null && !affectedTurrets.Contains(turret))
            {
                turret.modifierFireRate *= 0.5f;
                affectedTurrets.Add(turret);
            }
        }
        else if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null && !affectedPlayers.Contains(player))
            {
                player.moveSpeed *= 0.5f;
                affectedPlayers.Add(player);
            }
        }
    }

    private IEnumerator RemoveEffectsAfterDelay()
    {
        yield return new WaitForSeconds(duration);

        foreach (var turret in affectedTurrets)
        {
            if (turret != null)
                turret.modifierFireRate *= 2f; 
        }

        foreach (var player in affectedPlayers)
        {
            if (player != null)
                player.moveSpeed *= 2f; // restore speed
        }

        Destroy(gameObject); // Remove the ice explosion zone
    }
}