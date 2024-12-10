using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class GoldMiner : MonoBehaviour
{
    [SerializeField] private int MinerGoldPerSeconds = 1;
    private AddGold _addGold;
    private bool mining;
    private bool isMiningCoroutineRunning;

    private void Awake()
    {
        _addGold = GetComponent<AddGold>();
    }

    private void Update()
    {
        // Update is no longer needed for this functionality.
    }

    private IEnumerator MiningGold()
    {
        isMiningCoroutineRunning = true;

        while (mining)
        {
            _addGold.AddGoldToDisplay(MinerGoldPerSeconds);
            yield return new WaitForSeconds(3f); 
        }

        isMiningCoroutineRunning = false; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gold Ore"))
        {
            print("Started mining gold.");
            mining = true;

            // Start the coroutine only if it's not already running
            if (!isMiningCoroutineRunning)
            {
                StartCoroutine(MiningGold());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Gold Ore"))
        {
            print("Stopped mining gold.");
            mining = false;
        }
    }
}