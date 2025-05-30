using System;
using Unity.VisualScripting;
using System.Collections;
using TMPro;
using UnityEngine;

public class RoundStateManager : MonoBehaviour
{

    public bool tutorialCantStartRound = false;
    
    public SelectTurret selectTurret;
    
    [SerializeField] private TutorialStateSO _tutorialStateSO;
    
    public RoundBaseState currentState;
    
    public RoundOverState roundOverState = new RoundOverState();
    public RoundInProgressState roundInProgressState = new RoundInProgressState();
    
    private GameObject[] allTurrets;
    

    private SpawnEnemies spawnEnemies;
    private EnemyOnMapCounter enemyOnMapCounter;

    public int currentRound = 0;

    [SerializeField] private TMP_Text RoundDisplayText;

    public GameObject[] mapArrows;

    [HideInInspector] public int initialEnemyCount;
    private void Awake()
    {
        spawnEnemies = GetComponent<SpawnEnemies>();
        enemyOnMapCounter = GetComponent<EnemyOnMapCounter>();
    }

    void Start()
    {
        currentState = roundOverState;
        currentState.EnterState(this);
        
        if (_tutorialStateSO.playerTutorial)
        {
            tutorialCantStartRound = true;
        }

        if (!_tutorialStateSO.playerTutorial)
        {
            tutorialCantStartRound = false;
        }
        
    }
    
    public IEnumerator PlayMusicDelayed()
    {
        yield return new WaitForSecondsRealtime(1.95f); 
        AudioManager.instance.RoundZeroMusic();
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentState.OnCollisionEnter2D(this, other);
    }

    public void SwitchState(RoundBaseState roundBaseState)
    {
        currentState = roundBaseState;
        roundBaseState.EnterState(this);
    }

    public void AllowTurretsToShoot(bool shouldTurretShoot)
    {
        allTurrets = GameObject.FindGameObjectsWithTag("Turret");
        TurretShoot turretShoot;
        foreach (GameObject turret in allTurrets)
        {
            turretShoot = turret.GetComponent<TurretShoot>();
            
            turretShoot.AllowTurretToShoot = shouldTurretShoot;
            turretShoot.fireCooldown = 0;
            
        }
    }

    public void DisplayRoundUi(int currentRound)
    {
        RoundDisplayText.text = "Round Number: " + currentRound.ToString() + "/10";

    }

    public void ButtonClickStartNextRound()
    {
        if (currentState == roundOverState && !tutorialCantStartRound)
        {
            SwitchState(this.roundInProgressState);
        }
    }

    public void SpawnBasicEnemies(int currentRoundIndex)
    {
        currentRound = currentRoundIndex;
        DisplayRoundUi(currentRound);

        // spawn enemies as before
        int spawned = spawnEnemies.SpawnEnemiesPerRound(currentRoundIndex);
        enemyOnMapCounter.MaxEnemiesOnMap = spawned;
        initialEnemyCount = spawned;

    }
    
    public void DestroyAllPlayerBullets()
    {
        GameObject[] allBullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
        foreach (GameObject bullet in allBullets)
        {
            Destroy(bullet);
        }
    }
    
    public void OnEnemyCountChanged(int newCount)
    {

    }

    //Music
    
    public void MusicRoundEnd()
    {
        Debug.Log("MusicRoundEnd(): "  + currentRound);

        AudioManager.instance.StopMusic();
        
        switch (currentRound)
        {
            case 0:
                StartCoroutine(PlayMusicDelayed());
                break;
            case 4:
                AudioManager.instance.PreSlimeBossMusic();
                break;
            case 9:
                print("snake: music: Pre Snake Boss");
                AudioManager.instance.PreSnakeBossMusic();
                break;
            case 10: //Played in RoundINProgressState
                break;
            default:
                print("random music");
                AudioManager.instance.PlayRandomMusic(currentRound);
                break;
        }
  
    }
    
    public void MusicRoundInProgress()
    {
        Debug.Log("MusicRoundInProgress(): "  + currentRound);

        switch (currentRound)
        {
            case 4: //This plays on round 5 idk why
                AudioManager.instance.StopMusic();
                AudioManager.instance.SlimeBossMusic();
                break;
            case 9:
                print("snake: play snake track 0.");
                
                AudioManager.instance.StopMusic();
                AudioClip snakeMusic = AudioManager.instance.GetSnakeBossMusic(0);
                AudioManager.instance.FadeToMusic(snakeMusic, 0f);
                break;
            default:
                break;
        }
  
    }
}
