using UnityEngine;

public class WinScreenController : MonoBehaviour
{
    
    [SerializeField] private GameObject _WinScreen;

    public static WinScreenController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    
    public void Show()
    {
        _WinScreen.SetActive(true);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);
    }

    /// <summary>
    /// Hide the death screen UI (if you ever need to).
    /// </summary>
    public void Hide()
    {
        _WinScreen.SetActive(false);
    }
}
