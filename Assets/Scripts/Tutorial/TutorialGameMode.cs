using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGameMode : MonoBehaviour
{
    [SerializeField] private GameObject[] popUps;
    private int popUpIndex = 0;

    // Booleans for WASD keys
    private bool usedW = false;
    private bool usedA = false;
    private bool usedS = false;
    private bool usedD = false;
    
    // Booleans for arrow keys
    private bool usedUp = false;
    private bool usedDown = false;
    private bool usedLeft = false;
    private bool usedRight = false;
    
    [SerializeField] private bool activeTutorial;

    [SerializeField] private TMP_Text tutorialText;

    private GameModeManager gameModeManager;
    private void Awake()
    {
        gameModeManager = GameObject.FindGameObjectWithTag("GameModeManager").GetComponent<GameModeManager>();

    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorialText.text = "";
        if (gameModeManager.CurrentMode == GameMode.Tutorial)
        {
            activeTutorial = true;
        }
        else
        {
            activeTutorial = false;
        }
    }

    // Checks if all WASD keys are pressed before advancing
    private void UseWASD()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            usedW = true;
        }
                
        if (Input.GetKeyDown(KeyCode.A))
        {
            usedA = true;
        }
                
        if (Input.GetKeyDown(KeyCode.S))
        {
            usedS = true;
        }
                
        if (Input.GetKeyDown(KeyCode.D))
        {
            usedD = true;
        }
        
        // Only advance once all keys have been pressed
        if (usedW && usedA && usedS && usedD)
        {
            popUpIndex++;
            ShowCurrentPopUp();
        }
    }
    
    // Checks if all arrow keys have been pressed before advancing
    private void UseArrowKeysToShoot()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            usedUp = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            usedDown = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            usedLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            usedRight = true;
        }

        if (usedUp && usedDown && usedLeft && usedRight)
        {
            popUpIndex++;
            ShowCurrentPopUp();
        }
    }

    void ShowCurrentPopUp()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].SetActive(i == popUpIndex);
        }
    }
    
    private bool turretMovedUp = false;
    private bool turretMovedDown = false;
    private bool turretMovedLeft = false;
    private bool turretMovedRight = false;


    private int turretPlacedNumerOfTimes = 0;
    private void PlaceDownTurret()
    {
        // Simulate turret placement: when the space key is pressed, toggle turret placement.
        if (Input.GetKeyDown(KeyCode.Space) && turretPlacedNumerOfTimes == 0)
        {
            turretPlacedNumerOfTimes = 1;
        }
        
        if (turretPlacedNumerOfTimes == 1)
        {
            tutorialText.text = "Use arrow keys to move the turret around";

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                turretMovedUp = true;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                turretMovedDown = true;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                turretMovedLeft = true;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                turretMovedRight = true;
            }

            // Only advance when all four arrow keys have been pressed
            if (turretMovedUp && turretMovedDown && turretMovedLeft && turretMovedRight)
            {
                turretPlacedNumerOfTimes = 2;
            }
        }

        if (turretPlacedNumerOfTimes == 2)
        {
            tutorialText.text = "Press R to rotate the turret";
            if (Input.GetKeyDown(KeyCode.R))
            {
                turretPlacedNumerOfTimes = 3;
            }
        }
        
        if (turretPlacedNumerOfTimes == 3)
        {
            tutorialText.text = "Press space again for the final turret placement";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;
                ShowCurrentPopUp();
            }
        }
    }

    private void SwitchBetweenTurrets()
    {
        tutorialText.text = "Walk up to the turrets\nGreen is selected and Red isn't selected\nYou can switch between them using the arrow keys";

        // Check for left/right arrow input to simulate turret switching.
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            popUpIndex++;
            ShowCurrentPopUp();
        }
    }

    private int upgradeIndex = 0;
    private void UpgradeTurret()
    {
        if (upgradeIndex == 0)
        {
            tutorialText.text = "Select a turret by using E";
            if (Input.GetKeyDown(KeyCode.E))
            {
                upgradeIndex++;
            }
        }

        if (upgradeIndex == 1)
        {
            tutorialText.text = "Switch between turret upgrades using the Left and Right arrow keys";
            if (Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.RightArrow))
            {
                upgradeIndex++;
            }
        }
        
        if (upgradeIndex == 2)
        {
            tutorialText.text = "Press E to upgrade turret";
            if (Input.GetKeyDown(KeyCode.E))
            {
                upgradeIndex++;
                popUpIndex++;
                ShowCurrentPopUp();
            }
        }
    }

    private void StartRound()
    {
        tutorialText.text = "To start the round press 'TAB'";

        // Press Tab to start the round.
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            activeTutorial = false;
            // Optionally hide all tutorial pop-ups.
            foreach (var popUp in popUps)
            {
                popUp.SetActive(false);
            }
            
            tutorialText.text = "";
        }
    }

    void Update()
    {
        if (!activeTutorial) return;

        switch (popUpIndex)
        {
            case 0:
                tutorialText.text = "use W A S D to move around. Press All Keys";
                UseWASD();
                break;
            case 1:
                tutorialText.text = "Use Arrow keys to shoot. Press All Keys";
                UseArrowKeysToShoot();
                break;
            case 2:
                tutorialText.text = "Press space to place down a ghost turret";
                PlaceDownTurret();
                break;
            case 3:
                Debug.Log("Switch between turrets");
                SwitchBetweenTurrets();
                break;
            case 4:
                Debug.Log("Upgrade Turret");
                UpgradeTurret();
                break;
            case 5:
                Debug.Log("Start round");
                StartRound();
                break;
            default:
                break;
        }
    }
}
