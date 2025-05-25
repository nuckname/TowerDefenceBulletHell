using System;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject containerSettingsMenu;
    [SerializeField] private bool PauseMenuOpen;
    [SerializeField] private TutorialStateSO tutorialStateSO;

    public GameObject tutorialText;
    
    
    private void Start()
    {
        SettingMenuStartUp();
        
        if (tutorialStateSO.playerTutorial)
        {
            settingsMenu.SetActive(true);
            
            PauseMenuOpen = true;
            
            tutorialText.SetActive(false);
            

        }
        else
        {
            PauseMenuOpen = false;

            settingsMenu.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
         {
             Debug.Log("Escape key pressed");
            if (!PauseMenuOpen)
            {
                Debug.Log("Opening pause menu");
                PauseMenuOpen = true;
                settingsMenu.SetActive(true);
                
                //pause
                Time.timeScale = 0;
            }
            else
            {
                ClosePauseMenu();
            }
        }
    }


    public void ClosePauseMenu()
    {
        Debug.Log("Closing pause menu");
        TurnOnTutorialText();

        PauseMenuOpen = false;
        settingsMenu.SetActive(false);
                
        //un pause
        Time.timeScale = 1;
    }

    private void SettingMenuStartUp()
    {
        settingsMenu.SetActive(false);
        containerSettingsMenu.SetActive(true);
    }
    
    //Only for the back button in settings menu
    public void TurnOnTutorialText()
    {
        if (tutorialStateSO.playerTutorial)
        {
            tutorialText.SetActive(true);
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
