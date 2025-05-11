using UnityEngine;

public class DeathScreenController : MonoBehaviour
{
    public static DeathScreenController Instance { get; private set; }

    [SerializeField] private GameObject _deathScreen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    /// <summary>
    /// Show the death screen UI.
    /// </summary>
    public void Show()
    {
        _deathScreen.SetActive(true);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);
    }

    /// <summary>
    /// Hide the death screen UI (if you ever need to).
    /// </summary>
    public void Hide()
    {
        _deathScreen.SetActive(false);
    }
}