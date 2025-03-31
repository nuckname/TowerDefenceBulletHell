using UnityEngine;

[CreateAssetMenu(fileName = "GameModeSettings", menuName = "GameMode/GameModeSettings")]
public class GamemodeSettingsSO : ScriptableObject
{
    public SelectMapManager.MapType mapSelected;
    public SelectDifficulty.Difficulty selectedDifficulty;
    //What is better?
    public GameMode gameMode;
    
}
