using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class RoundOverState : RoundBaseState
{
    public override void EnterState(RoundStateManager roundStateManager)
    {
        Debug.Log("Round Over State");
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
    private IEnumerator FadeOutAndDestroy(GameObject obj, float duration)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            float startAlpha = color.a;

            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                float normalizedTime = t / duration;
                color.a = Mathf.Lerp(startAlpha, 0, normalizedTime);
                spriteRenderer.color = color;
                yield return null;
            }

            color.a = 0;
            spriteRenderer.color = color;
        }

        GameObject.Destroy(obj);
    }

    private IEnumerator RemoveAllCoinsAndHearts(RoundStateManager roundStateManager)
    {
        yield return new WaitForSeconds(0.75f);

        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
            roundStateManager.StartCoroutine(FadeOutAndDestroy(coin, 1f)); // 1 second fade-out duration
        }

        GameObject[] hearts = GameObject.FindGameObjectsWithTag("Heart");
        foreach (GameObject heart in hearts)
        {
            roundStateManager.StartCoroutine(FadeOutAndDestroy(heart, 1f)); // 1 second fade-out duration
        }
    }
}