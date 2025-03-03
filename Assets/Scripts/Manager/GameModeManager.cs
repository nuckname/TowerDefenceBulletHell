using System;
using UnityEngine.UI;
using UnityEngine;

public enum GameMode
{
    Classic,
    Tutorial,
    Endless,
    HalfCash,
    DoubleHP,
    OnlyNormalAndRare,
    OneHp,
    Everything
}

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance { get; private set; }
    //public GameMode CurrentMode { get; private set; }
    public GameMode CurrentMode;

    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject MainMenu;
        
    [SerializeField] private Button classicButton;
    [SerializeField] private Button endlessButton;
    [SerializeField] private Button halfCashButton;
    [SerializeField] private Button doubleHpButton;
    [SerializeField] private Button onlyNormalAndRareButton;
    [SerializeField] private Button oneHpButton;
    [SerializeField] private Button everythingButton;
    
    //Set gamemode
    //Use these on buttons
    public void SetClassicMode() => SetGameMode(GameMode.Classic);
    public void SetTutorialMode() => SetGameMode(GameMode.Tutorial);
    //public void SetEndlessMode() => SetGameMode(GameMode.Endless);
    public void SetHalfCashMode() => SetGameMode(GameMode.HalfCash);
    public void SetDoubleHPMode() => SetGameMode(GameMode.DoubleHP);
    //public void SetOnlyNormalAndRareMode() => SetGameMode(GameMode.OnlyNormalAndRare);
    public void SetOneHpMode() => SetGameMode(GameMode.OneHp);
    public void SetEverythingMode() => SetGameMode(GameMode.Everything);

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Ensure GameModeManager instance exists
        if (GameModeManager.Instance == null)
        {
            Debug.LogError("GameModeManager instance is missing in the scene!");
            return;
        }
    }

    public void SetGameMode(GameMode mode)
    {
        CurrentMode = mode;
        MainMenu.SetActive(false);
        Instantiate(player, spawnPoint.position, Quaternion.identity);
    }

}
