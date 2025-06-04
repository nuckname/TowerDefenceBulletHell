using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class TutorialReworked : MonoBehaviour
{
    [Header("Shake text")]
    [SerializeField] private Color insufficientColor = Color.red;
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeMagnitude = 0.1f;
    
    private float shakeCooldown = 5f;  // 5 seconds cooldown
    private float lastShakeTime = -Mathf.Infinity;  // track last shake time, initialized to negative infinity so first shake is allowed

    
    [Header("UI")]
    public TMP_Text tutorialText;

    [SerializeField] private RoundStateManager roundStateManager;
    
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
        "Tutorial Step 11:\n\nGet Ready. Then press TAB to start the round.",
        // Step 8
        "Tutorial Step 9:\n\nHold Left Ctrl + Left Mouse Click during the round to apply an upgrade.",
    };

    void Start()
    {
        /*
        if (_tutorialStateSO.playerTutorial)
        {
            tutorialText.text = _messages[_currentStep];
            placeTurret.tutorialCannotPlaced = true;
            
            placeTurret.tutorialCannotBuyGhostTurret = true;
            
            placeTurret.tutorialCannotPressQ = true;
            
            roundStateManager.tutorialCantStartRound = true;
            print("Tutorial cant start round");
            print("cannot place turret");
        }
        else
        {
            tutorialText.text = "";
            placeTurret.tutorialCannotPlaced = false;
            
            placeTurret.tutorialCannotBuyGhostTurret = false;
            
            placeTurret.tutorialCannotPressQ = false;
            
            roundStateManager.tutorialCantStartRound = false;

            print("can place turret");
        }
        */
    }

    public void NextStepOnContinueButtonClick()
    {
        if (_tutorialStateSO.playerTutorial)
        {
            if (_currentStep == 11)
            {
                NextStep();
            }
        }
    }

    private bool isWASDPressed = false;

    
    void Update()
    {
        
        if (!_tutorialStateSO.playerTutorial)
            return;
        
        isWASDPressed = Input.GetKey(KeyCode.W) ||
                        Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) ||
                        Input.GetKey(KeyCode.D);

        switch (_currentStep)
        {
            case 0:
                // “Use WASD to move.”
                if (Input.GetKeyDown(KeyCode.W) ||
                    Input.GetKeyDown(KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.S) ||
                    Input.GetKeyDown(KeyCode.D))
                {

                    NextStep();
                }
                
                break;
            case 1: // “HOLD Left Mouse Button to shoot.”
                if (Input.GetMouseButtonDown(0))
                {
                    NextStep();
                }
                break;

            case 2: // “Press SPACE to spawn a ghost turret.”
                placeTurret.tutorialCannotBuyGhostTurret = false;
                placeTurret.tutorialCannotPlaced = false;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    placeTurret.tutorialCannotBuyGhostTurret = true;
                    placeTurret.tutorialCannotPlaced = true;
                    NextStep();
                }
                else if (Input.anyKeyDown)
                {
                    if (IsAllowedKeyPressed())
                        return;
    
                    ShakeText();
                }
                break;
            case 3: // “Use the SCROLL WHEEL to rotate your ghost turret.”

                if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0f)
                {
                    NextStep();
                    
                }
                else if (Input.anyKeyDown)
                {
                    if (IsAllowedKeyPressed())
                        return;
    
                    ShakeText();
                }
                break;

            case 4: // “Press R to snap-rotate your turret…”
                if (Input.GetKeyDown(KeyCode.R))
                {
                    NextStep();
                }
                else if (Input.anyKeyDown)
                {
                    if (IsAllowedKeyPressed())
                        return;
    
                    ShakeText();
                }
                break;
            case 5: // “Or just Press R to rotate 90 degrees.”
                if (Input.GetKeyDown(KeyCode.R))
                {
                    NextStep();
                }
                else if (Input.anyKeyDown)
                {
                    if (IsAllowedKeyPressed())
                        return;
    
                    ShakeText();
                }
                break;
            case 6: // “Left Mouse Button to confirm turret placement.”
                placeTurret.tutorialCannotPlaced = false;
                if (Input.GetMouseButtonDown(0) )
                {
                    //Stops the player from placing onto path and going to the next step.
                    if (placeTurret.GhostTurretHasBeenPlaced)
                    {
                        ShakeText();
                        return;
                    }                    
                    
                    NextStep();
                }
                break;

            case 7: // “Click on an existing turret to open its upgrade UI.”
                if (Input.GetMouseButtonDown(0))
                {
                    MustClickOnTurret();
                }
                else if (Input.anyKeyDown)
                {
                    if (IsAllowedKeyPressed())
                        return;
    
                    ShakeText();
                }
                break;
            case 8: // “Press TAB to start the round.”
                roundStateManager.tutorialCantStartRound = false;

                placeTurret.tutorialCannotBuyGhostTurret = false;
                placeTurret.tutorialCannotPressQ = false;
                
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    NextStep();
                }
                break;
            case 9: // “Hold Left Ctrl + Left Mouse Click during the round to apply an upgrade.”
                
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
                    MustClickOnTurret();
                break;
        }
    }
    
    private bool IsAllowedKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.W) ||
               Input.GetKeyDown(KeyCode.A) ||
               Input.GetKeyDown(KeyCode.S) ||
               Input.GetKeyDown(KeyCode.D) ||
               Input.GetKeyDown(KeyCode.Space);
    }

    private void MustClickOnTurret()
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


    private void NextStep()
    {
        _currentStep++;
        
        lastShakeTime = -Mathf.Infinity;
        
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

    private void ShakeText()
    {
        if (Time.time - lastShakeTime < shakeCooldown)
            return;
        
        lastShakeTime = Time.time;
        
        StartCoroutine(ShowCannotBuyFeedback(tutorialText));
        AudioManager.instance.GibberishSFX();
    }
    
    
    private IEnumerator ShowCannotBuyFeedback(TMP_Text priceText)
    {
        // 1) turn text red
        var originalColor = priceText.color;
        priceText.color = insufficientColor;

        // 2) shake
        var rt = priceText.rectTransform;
        Vector3 originalPos = rt.localPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = (Random.value * 2f - 1f) * shakeMagnitude;
            rt.localPosition = originalPos + new Vector3(offsetX, 0f, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 3) restore
        rt.localPosition = originalPos;
        priceText.color = originalColor;
    }
}
