using System;
using UnityEngine;
using TMPro;

public class TutorialReworked : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text tutorialText;

    [Header("Tutorial State")]
    [SerializeField]
    private TutorialStateSO _tutorialStateSO;

    private int _currentStep = 0;

    [SerializeField] 
    private PlaceTurret placeTurret;

    private GhostBlockPathCollision ghostBlockPathCollision;
    private readonly string[] _messages = new string[]
    {
        // Step 0
        "Tutorial Step 1:\n\nUse WASD to move.",
        // Step 1
        "Tutorial Step 2:\n\nHOLD Left Mouse Button to shoot.",
        // Step 2
        "Tutorial Step 3:\n\nPress SPACE to spawn a ghost turret.",
        // Step 3
        "Tutorial Step 4:\n\nUse the SCROLL WHEEL to rotate your ghost turret.",
        // Step 4
        "Tutorial Step 5:\n\nPress R to snap-rotate your turret to the nearest 90° increment.",
        // Step 5 (new)
        "Tutorial Step 6:\n\nOr just Press R to rotate 90 degrees.",
        // Step 6
        "Tutorial Step 7:\n\nLeft Mouse Button to confirm turret placement.",
        // Step 7
        "Tutorial Step 8:\n\nClick on a turret to upgrade it.",
        // Step 10
        "Tutorial Step 11:\n\nPress TAB to start the round.",
        // Step 8
        "Tutorial Step 9:\n\nHold Left Ctrl + Left Mouse Click during the round to apply an upgrade.",
    };

    void Start()
    {
        if (_tutorialStateSO.playerTutorial)
        {
            tutorialText.text = _messages[_currentStep];
            placeTurret.tutorialCannotPlaced = true;
            print("cannot place turret");
        }
        else
        {
            tutorialText.text = "";
            placeTurret.tutorialCannotPlaced = false;
            print("can place turret");
        }
        
        
    }

    void Update()
    {
        if (!_tutorialStateSO.playerTutorial)
            return;

        switch (_currentStep)
        {
            case 0: // “Use WASD to move.”
                if (Input.GetKeyDown(KeyCode.W) ||
                    Input.GetKeyDown(KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.S) ||
                    Input.GetKeyDown(KeyCode.D))
                    NextStep();
                break;

            case 1: // “HOLD Left Mouse Button to shoot.”
                if (Input.GetMouseButtonDown(0))
                    NextStep();
                break;

            case 2: // “Press SPACE to spawn a ghost turret.”
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    placeTurret.tutorialCannotPlaced = true;
                    NextStep();
                }
                break;
            case 3: // “Use the SCROLL WHEEL to rotate your ghost turret.”
                if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0f)
                {
                    placeTurret.tutorialCannotPlaced = true;
                    NextStep();
                    
                }
                break;

            case 4: // “Press R to snap-rotate your turret…”
                if (Input.GetKeyDown(KeyCode.R))
                {
                    placeTurret.tutorialCannotPlaced = true;
                    NextStep();
                }
                break;
            case 5: // “Or just Press R to rotate 90 degrees.”
                if (Input.GetKeyDown(KeyCode.R))
                {
                    placeTurret.tutorialCannotPlaced = true;
                    NextStep();
                }
                break;
            case 6: // “Left Mouse Button to confirm turret placement.”
                placeTurret.tutorialCannotPlaced = false;
                if (Input.GetMouseButtonDown(0))
                {
                    NextStep();
                }
                break;

            case 7: // “Click on an existing turret to open its upgrade UI.”
                if (Input.GetMouseButtonDown(0))
                {
                    // 1) convert screen→world
                    Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    // 2) shoot a zero‑length Raycast to see what’s under the cursor
                    RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        
                    // 3) if we hit something tagged “Turret”, advance
                    if (hit.collider != null && hit.collider.CompareTag("Turret"))
                    {
                        NextStep();
                    }
                }
                break;
            case 8: // “Press TAB to start the round.”
                if (Input.GetKeyDown(KeyCode.Tab))
                    NextStep();
                break;

            case 9: // “Hold Left Ctrl + Left Mouse Click during the round to apply an upgrade.”
                //10 second timer???
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
                    NextStep();
                break;
        }
    }


    private void NextStep()
    {
        _currentStep++;
        if (_currentStep < _messages.Length)
            tutorialText.text = _messages[_currentStep];
        else
            EndTutorial();
    }

    private void EndTutorial()
    {
        tutorialText.text = "";
        enabled = false;
    }
}
