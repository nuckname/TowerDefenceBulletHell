using TMPro;
using UnityEngine;

public class GoldUi : MonoBehaviour
{
    public PlayerGoldScriptableObject playerGold;
    public TMP_Text goldText;

    private void Start()
    {
        if (playerGold == null)
        {
            Debug.LogError("PlayerGoldScriptableObject is not assigned in GoldUI.");
            return;
        }

        if (goldText == null)
        {
            Debug.LogError("TMP_Text is not assigned in GoldUI.");
            return;
        }

        UpdateGoldUI(playerGold.currentGold);

        // Subscribe to the gold change event
        playerGold.OnGoldChanged.AddListener(UpdateGoldUI);
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        if (playerGold != null)
        {
            playerGold.OnGoldChanged.RemoveListener(UpdateGoldUI);
        }
    }

    // Update the TMP_Text component when gold changes
    private void UpdateGoldUI(int goldAmount)
    {
        goldText.SetText("Gold: {0}", goldAmount);
    }

}
