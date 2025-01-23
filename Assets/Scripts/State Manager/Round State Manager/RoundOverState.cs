using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class RoundOverState : RoundBaseState
{
    public override void EnterState(RoundStateManager roundStateManager)
    {
        Debug.Log("Round Over State");
        Debug.Log("false");
        roundStateManager.AllowTurretsToShoot(false);

        roundStateManager.StartCoroutine(RemoveAllCoinsAndHearts(roundStateManager));
    }

    public override void UpdateState(RoundStateManager roundStateManager)
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            roundStateManager.SwitchState(roundStateManager.roundInProgressState);
        }
    }

    public override void OnCollisionEnter2D(RoundStateManager roundStateManager, Collision2D other)
    {
    }

    //Fade out chat bulletshit
    private IEnumerator FadeOutAndDestroy(float duration, string tag)
    {
        //what is obj?
        GameObject[] gameObjectsToDestory = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject gameObject in gameObjectsToDestory)
        {
            /*
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                float startAlpha = color.a;

                for (float t = 0; t < duration; t += Time.deltaTime)
                {
                    float normalizedTime = t / duration;
                    color.a = Mathf.Lerp(startAlpha, 0, normalizedTime);
                    spriteRenderer.color = color;
                }

                color.a = 0;
                spriteRenderer.color = color;
            }
            */

            GameObject.Destroy(gameObject);
            yield return null;
        }

    }


private IEnumerator RemoveAllCoinsAndHearts(RoundStateManager roundStateManager)
    {
        yield return new WaitForSeconds(0.75f);
       
        roundStateManager.StartCoroutine(FadeOutAndDestroy( 1f, "Coin")); // 1 second fade-out duration
        
        roundStateManager.StartCoroutine(FadeOutAndDestroy( 1f, "Heart")); // 1 second fade-out duration


    }
}