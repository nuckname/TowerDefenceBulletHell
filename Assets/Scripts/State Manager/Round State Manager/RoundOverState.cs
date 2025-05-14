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

        if (roundStateManager.currentRound == 10)
        {
            yield return new WaitForSeconds(0f);
           
        }
  
        yield return new WaitForSeconds(2f);
        
        roundStateManager.StartCoroutine(DestoryGameObject( 1.8f, "Coin", roundStateManager)); 
        //roundStateManager.StartCoroutine(DestoryGameObject( 1.5f, "Heart")); // 1 second fade-out duration
    }
    
    private IEnumerator DestoryGameObject(float duration, string tag, RoundStateManager roundStateManager)
    {
        GameObject[] gameObjectsToDestroy = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in gameObjectsToDestroy)
        {
            // Kick off a fade‑and‑destroy coroutine for each object
            roundStateManager.StartCoroutine(FadeAndDestroy(obj, duration));
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