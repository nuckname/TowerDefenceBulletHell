using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TutorialReworked : MonoBehaviour
{
    // Reference to your TextMeshPro text field in the UI
    public TMP_Text tutorialText;

    // Tracks the current step of the tutorial
    private int currentStep = 0;
    // Optional flag for turret placement cancellation
    [SerializeField] private TutorialStateSO tutorialStateSO;
    
    // Tutorial steps messages
    private string[] messages = new string[]
    {
        // Step 0: Move
        "Tutorial Step 1:\n\nUse WASD to move.",
        // Step 1: Shoot
        "Tutorial Step 2:\n\nLeft Mouse Button to shoot.",
        // Step 2: Spawn Ghost Turret
        "Tutorial Step 3:\n\nPress SPACE to spawn a ghost turret.",
        // Step 3: Rotate Turret
        "Tutorial Step 4:\n\nPress R to rotate the turret.",
        // Step 4: Confirm or Cancel Turret Placement
        "Tutorial Step 5:\n\nThen press Left Mouse to confirm where you want to place it.",
        // Step 5: Update Turret
        "Tutorial Step 6:\n\nClick on the turret to update it.",
        // Step 6: Start Round
        "Tutorial Step 7:\n\nPlace more turrets and upgrade them then press TAB to start the round."
    };

    void Start()
    {
        // Display the first message
        if (tutorialStateSO.playerTutorial)
        {
            tutorialText.text = messages[currentStep];
        }
        else
        {
            tutorialText.text = "";
        }
    }

    void Update()
    {
        if(!tutorialStateSO.playerTutorial)
        {
            return;
        }
        
        switch (currentStep)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    NextStep();
                }
                break;

            case 1:
                if (Input.GetMouseButtonDown(0))
                {
                    NextStep();
                }
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    NextStep();
                }
                break;

            case 3:
                if (Input.GetKeyDown(KeyCode.R))
                {
                    NextStep();
                }
                break;

            case 4:
                if (Input.GetMouseButtonDown(0))
                {
                    NextStep();
                }
                break;

            case 5:
                if (Input.GetMouseButtonDown(0))
                {
                    NextStep();
                }
                break;

            case 6:
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    EndTutorial();
                }
                break;
        }
    }

    void NextStep()
    {
        currentStep++;
        if (currentStep < messages.Length)
        {
            tutorialText.text = messages[currentStep];
        }
    }

    void EndTutorial()
    {
        tutorialText.text = "";
        enabled = false;
    }
}
