using UnityEngine;

public class SelectDifficulty : MonoBehaviour
{
    public GamemodeSettingsSO gamemodeSettingsSO;
    
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard,
        Impossible
    }
    
    public void EasyMapSelected()
    {
        gamemodeSettingsSO.selectedDifficulty = Difficulty.Easy;
    }

    public void MediumMapSelected()
    {
        gamemodeSettingsSO.selectedDifficulty = Difficulty.Medium;
    }

    public void HardMapSelected()
    {
        gamemodeSettingsSO.selectedDifficulty = Difficulty.Hard;
    }

    public void ImpossibleMapSelected()
    {
        gamemodeSettingsSO.selectedDifficulty = Difficulty.Impossible;
    }
}
