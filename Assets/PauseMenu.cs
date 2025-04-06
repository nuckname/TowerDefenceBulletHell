using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    [SerializeField] private GameObject pauseMenu;
    private bool PauseMenuOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
         {
            if (!PauseMenuOpen)
            {
                PauseMenuOpen = true;
                pauseMenu.SetActive(true);
            }
            else
            {
                PauseMenuOpen = false;
                pauseMenu.SetActive(false);
            }
        }
    }

    public void ResumedClick()
    {
        PauseMenuOpen = false;
    }
    

    public void QuitGame()
    {
        Application.Quit();
    }
}
