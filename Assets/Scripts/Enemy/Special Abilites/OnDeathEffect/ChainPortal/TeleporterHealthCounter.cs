// TeleportPortal.cs

using System;
using UnityEngine;

public class TeleporterHealthCounter : MonoBehaviour
{
    public int tpRoundsLeft;
    public int maxTpLength;

    [SerializeField] private RoundStateManager _roundStateManager;

    private void Awake()
    {
        _roundStateManager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<RoundStateManager>();
    }

    public void UpdateTeleporterRounds()
    {
        tpRoundsLeft--;
        
        if (tpRoundsLeft == 0)
        {
            _roundStateManager.roundHasTeleporters = false;
            Destroy(gameObject);
        }
    }
}