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
            OpenPauseMenu(false);
            
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
            if (!PauseMenuOpen)
            {
                OpenPauseMenu(true);
            }
            else
            {
                ClosePauseMenu();
            }
        }
    }

    public void OpenPauseMenu(bool allowPause)
    {
        AudioManager.instance.PauseMusic();

        PlayerShooting.disableShooting = true;
        print("Player " +PlayerShooting.disableShooting);
        
        PauseMenuOpen = true;
        settingsMenu.SetActive(true);
                
        //pause
        if (allowPause)
        {
            Time.timeScale = 0;
        }
    }


    public void ClosePauseMenu()
    {
        AudioManager.instance.ResumeMusic();

        TurnOnTutorialText();

        PlayerShooting.disableShooting = false;
        print("Player " + PlayerShooting.disableShooting);

        
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
