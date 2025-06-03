using UnityEngine;

public class InGameUiManager : MonoBehaviour
{
    [SerializeField] private GameObject playerHitFlash;
    [SerializeField] private GameObject settingsMenu;
    void Start()
    {
        playerHitFlash.SetActive(true);
        //settingsMenu.SetActive(true);
    }

}
