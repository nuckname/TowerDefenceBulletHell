using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Analytics;

public class RoundInProgressState : RoundBaseState
{
    
    private int roundCounter = 0;
    public override void EnterState(RoundStateManager roundStateManager)
    {
        roundStateManager.AllowTurretsToShoot(true);
        
        roundCounter++;
        
        AnalyticsResult ar = Analytics.CustomEvent("Rounds Completed:" + roundCounter);

        Debug.Log("Unity Analyistics: " + ar);

        roundStateManager.DestroyAllPlayerBullets();

        roundStateManager.MusicRoundInProgress();
        
        roundStateManager.selectTurret.AllowSelectingTurret = false;

        roundStateManager.SpawnBasicEnemies(roundCounter);
        
        if (roundCounter == 1)
        {
            roundStateManager.mapArrows[0].gameObject.SetActive(false);
            roundStateManager.mapArrows[1].gameObject.SetActive(false);
        }
        
    }

    public override void UpdateState(RoundStateManager roundStateManager)
    {

    }

    public override void OnCollisionEnter2D(RoundStateManager roundStateManager, Collision2D other)
    {

    }
}
