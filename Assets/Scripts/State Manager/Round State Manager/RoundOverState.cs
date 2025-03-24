using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class RoundOverState : RoundBaseState
{
    public override void EnterState(RoundStateManager roundStateManager)
    {
        Debug.Log("Round Over State");
        roundStateManager.AllowTurretsToShoot(false);

        roundStateManager.selectTurret.AllowSelectingTurret = true;

        roundStateManager.StartCoroutine(RemoveAllCoinsAndHearts(roundStateManager));
    }

    public override void UpdateState(RoundStateManager roundStateManager)
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            roundStateManager.SwitchState(roundStateManager.roundInProgressState);
        }
    }

    public override void OnCollisionEnter2D(RoundStateManager roundStateManager, Collision2D other)
    {
        
    }

    private IEnumerator RemoveAllCoinsAndHearts(RoundStateManager roundStateManager)
    {
        
        //So boss coins dont fade out
        if (roundStateManager.currentRound == 5)
        {
            yield return new WaitForSeconds(0f);
        }

        yield return new WaitForSeconds(2f);

        if (roundStateManager.currentRound == 5)
        {
            //Dont clear coins            
        }
        else
        {
            roundStateManager.StartCoroutine(DestoryGameObject( 1.75f, "Coin")); 
        }
        //roundStateManager.StartCoroutine(DestoryGameObject( 1.5f, "Heart")); // 1 second fade-out duration
    }
    
    private IEnumerator DestoryGameObject(float duration, string tag)
    {
        GameObject[] gameObjectsToDestory = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject gameObject in gameObjectsToDestory)
        {
            GameObject.Destroy(gameObject);
            yield return null;
        }

    }
}