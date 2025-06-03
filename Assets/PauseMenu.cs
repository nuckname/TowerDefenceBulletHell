using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private bool PauseMenuOpen;
    [SerializeField] private TutorialStateSO tutorialStateSO;

    public GameObject tutorialText;
    
    private void Start()
    {
        if (tutorialStateSO.playerTutorial)
        {
            print("Tutorial open menu");
            OpenPauseMenu(false, false);
            
            //Temp disable so we dont overlap.
            tutorialText.SetActive(false);
        }
        else
        {
            print("Tutorial dont menu");

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
                Debug.unityLogger.Log("Opening pause menu");
                OpenPauseMenu(true, true);
            }
            else
            {
                ClosePauseMenu();
            }
        }
    }

    public void OpenPauseMenu(bool allowPause, bool playSound)
    {
        if (playSound)
        {
            AudioManager.instance.PauseMusic();
        }

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
