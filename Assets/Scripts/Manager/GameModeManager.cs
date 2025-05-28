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
    Unlucky,
    OneHp,
    Everything,
    FreePlay,
    ContinuousRounds
}

public class GameModeManager : MonoBehaviour
{
    [SerializeField] private LoadCorrectScene loadCorrectScene;
    public GamemodeSettingsSO gamemodeSettingsSO;
    public static GameModeManager Instance { get; private set; }
    public GameMode CurrentMode;

    public void ClassicMode() => SetGameMode(GameMode.Classic);
    public void TutorialMode() => SetGameMode(GameMode.Tutorial);
    //public void SetEndlessMode() => SetGameMode(GameMode.Endless);
    public void HalfCashMode() => SetGameMode(GameMode.HalfCash);
    public void DoubleHPMode() => SetGameMode(GameMode.DoubleHP);
    //public void SetOnlyNormalAndRareMode() => SetGameMode(GameMode.OnlyNormalAndRare);
    public void OneHpMode() => SetGameMode(GameMode.OneHp);
    public void BossEveryWaveMode() => SetGameMode(GameMode.OneHp);
    public void EverythingMode() => SetGameMode(GameMode.Everything);
    public void UnluckyMode() => SetGameMode(GameMode.Unlucky);
    public void EndlessMode() => SetGameMode(GameMode.Endless);

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
        print("Gamemode set to: " + mode.ToString());
        
        //Load from GamemodeSettingsSO
        loadCorrectScene.LoadScene();

        gamemodeSettingsSO.gameMode = mode;
        CurrentMode = mode;
    }

}
