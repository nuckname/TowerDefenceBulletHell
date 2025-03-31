using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCorrectScene : MonoBehaviour
{
    [SerializeField] private GamemodeSettingsSO loadMapSettingsSO; 

    public void LoadScene()
    {
        switch (loadMapSettingsSO.mapSelected)
        {
            case SelectMapManager.MapType.DesertMap:
                DesertLoad(loadMapSettingsSO.selectedDifficulty);
                break;
            case SelectMapManager.MapType.SnowMap:
                SnowLoad(loadMapSettingsSO.selectedDifficulty);
                break;
            case SelectMapManager.MapType.RuinMap:
                RuinLoad(loadMapSettingsSO.selectedDifficulty);
                break;
            default:
                Debug.LogError("Error loading map");
                break;
        }
    }
    
    private void DesertLoad(SelectDifficulty.Difficulty selectedDifficulty)
    {
        switch (selectedDifficulty)
        {
            case SelectDifficulty.Difficulty.Easy:
                SceneManager.LoadScene("DesertEasy");
                break;
            case SelectDifficulty.Difficulty.Medium:
                SceneManager.LoadScene("DesertMedium");
                break;
            case SelectDifficulty.Difficulty.Hard:
                SceneManager.LoadScene("DesertHard");
                break;
            case SelectDifficulty.Difficulty.Impossible:
                SceneManager.LoadScene("DesertImpossible");
                break;
            default:
                Debug.LogError("Error loading difficulty");
                break;
        }
    }
    
    private void SnowLoad(SelectDifficulty.Difficulty selectedDifficulty)
    {
        switch (selectedDifficulty)
        {
            case SelectDifficulty.Difficulty.Easy:
                SceneManager.LoadScene("SnowEasy");
                break;
            case SelectDifficulty.Difficulty.Medium:
                SceneManager.LoadScene("SnowMedium");
                break;
            case SelectDifficulty.Difficulty.Hard:
                SceneManager.LoadScene("SnowHard");
                break;
            case SelectDifficulty.Difficulty.Impossible:
                SceneManager.LoadScene("SnowImpossible");
                break;
            default:
                Debug.LogError("Error loading difficulty");
                break;
        }
    }

    private void RuinLoad(SelectDifficulty.Difficulty selectedDifficulty)
    {
        switch (selectedDifficulty)
        {
            case SelectDifficulty.Difficulty.Easy:
                SceneManager.LoadScene("RuinEasy");
                break;
            case SelectDifficulty.Difficulty.Medium:
                SceneManager.LoadScene("RuinMedium");
                break;
            case SelectDifficulty.Difficulty.Hard:
                SceneManager.LoadScene("RuinHard");
                break;
            case SelectDifficulty.Difficulty.Impossible:
                SceneManager.LoadScene("RuinImpossible");
                break;
            default:
                Debug.LogError("Error loading difficulty");
                break;
        }
    }
}
