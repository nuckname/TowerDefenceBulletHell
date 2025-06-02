using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            
            //Temp disable so we dont overlap.
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
             if (OnClickEffect.UiOpenCantUpgradeTurret)
             {
                 return;
             }
             
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

        AudioManager.instance.SelectTurretSFX();
        
        PlayerShooting.disableShooting = true;
        
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

        AudioManager.instance.backSFX();
        
        TurnOnTutorialText();

        PlayerShooting.disableShooting = false;
        
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
        //what is this for?
        AudioManager.instance.backSFX();
        PauseMenuOpen = false;
    }
    

    public void QuitGame()
    {
        Time.timeScale = 1;

        Application.Quit();
    }
    
    public void RestartScene()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
