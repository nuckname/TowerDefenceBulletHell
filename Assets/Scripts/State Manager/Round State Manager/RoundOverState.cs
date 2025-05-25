using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class RoundOverState : RoundBaseState
{
    public override void EnterState(RoundStateManager roundStateManager)
    {
        Debug.Log("Round Over State");
        
        
        roundStateManager.AllowTurretsToShoot(false);

        Music(roundStateManager);
        
        roundStateManager.selectTurret.AllowSelectingTurret = true;

        roundStateManager.StartCoroutine(RemoveAllCoinsAndHearts(roundStateManager));
    }
    
    public override void UpdateState(RoundStateManager roundStateManager)
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !roundStateManager.tutorialCantStartRound)
        {
            roundStateManager.SwitchState(roundStateManager.roundInProgressState);
        }
    }

    public override void OnCollisionEnter2D(RoundStateManager roundStateManager, Collision2D other)
    {
        
    }

    private void Music(RoundStateManager roundStateManager)
    {
        roundStateManager.StartCoroutine(roundStateManager.PlayMusicDelayed());
    }

    private IEnumerator RemoveAllCoinsAndHearts(RoundStateManager roundStateManager)
    {
        //So boss coins dont fade out
        if (roundStateManager.currentRound == 5 || roundStateManager.currentRound == 10)
        {
            yield return new WaitForSeconds(0f);
        }
        else
        {
            yield return new WaitForSeconds(1f);

            if (roundStateManager.currentRound < 5)
            {
                roundStateManager.StartCoroutine(DestoryGameObject( 1.25f,"Coin", roundStateManager)); 
            }
            
            if (roundStateManager.currentRound > 6 || roundStateManager.currentRound < 8)
            {
                roundStateManager.StartCoroutine(DestoryGameObject( 1.75f,"Coin", roundStateManager)); 
            }
            
            if (roundStateManager.currentRound > 9)
            {
                roundStateManager.StartCoroutine(DestoryGameObject( 2f,"Coin", roundStateManager)); 
            }
        }
    }
    
    private IEnumerator DestoryGameObject(float durationDuration, string tag, RoundStateManager roundStateManager)
    {
        GameObject[] gameObjectsToDestroy = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in gameObjectsToDestroy)
        {
            // Kick off a fade‑and‑destroy coroutine for each object
            roundStateManager.StartCoroutine(FadeAndDestroy(obj, durationDuration));
            // Optionally stagger them so they don’t all fade at once:
            // yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    private IEnumerator FadeAndDestroy(GameObject obj, float duration)
    {
        // Try SpriteRenderer (2D) first…
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        // …or UI Image
        UnityEngine.UI.Image uiImage = obj.GetComponent<UnityEngine.UI.Image>();

        float elapsed = 0f;
        Color initialColor = sr      != null ? sr.color
            : uiImage != null ? uiImage.color
            : Color.white;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, 0f, elapsed / duration);

            if (sr != null)
            {
                var c = sr.color;
                c.a = alpha;
                sr.color = c;
            }
            else if (uiImage != null)
            {
                var c = uiImage.color;
                c.a = alpha;
                uiImage.color = c;
            }

            yield return null;
        }

        // ensure fully transparent
        if (sr != null)
        {
            var c = sr.color; c.a = 0f; sr.color = c;
        }
        else if (uiImage != null)
        {
            var c = uiImage.color; c.a = 0f; uiImage.color = c;
        }

        GameObject.Destroy(obj);
    }

}