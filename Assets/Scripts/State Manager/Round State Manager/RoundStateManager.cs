using System;
using Unity.VisualScripting;
using System.Collections;
using TMPro;
using UnityEngine;

public class RoundStateManager : MonoBehaviour
{
    public SelectTurret selectTurret;
    
    public bool tutorialCantStartRound = false;
    
    public RoundBaseState currentState;
    
    public RoundOverState roundOverState = new RoundOverState();
    public RoundInProgressState roundInProgressState = new RoundInProgressState();
    
    private GameObject[] allTurrets;
    

    private SpawnEnemies spawnEnemies;
    private EnemyOnMapCounter enemyOnMapCounter;

    public int currentRound = 1;

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
    }
    
    public IEnumerator PlayMusicDelayed()
    {
        AudioManager.instance.StopMusic();
        
        yield return new WaitForSecondsRealtime(1.95f); 

        print("current round: " + currentRound);
        if (currentRound == 1)
        {
            AudioManager.instance.RoundZeroMusic();

        }
        else if (currentRound == 5)
        {
            print("JAZZ");
            AudioManager.instance.SlimeBossMusic();
        }
        else if (currentRound == 10)
        {
            print("SNAKE");
            AudioManager.instance.SnakeBossMusic(0);
        }
        else
        {
            print("random music");
            AudioManager.instance.PlayRandomMusic();
        }
        
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
        
        /*
        // If Free Play, show “∞” instead of max
        if (findObjectOfType<GameModeManager>().CurrentMode == GameMode.FreePlay)
            RoundDisplayText.text = $"Round: {currentRound} / ∞";
        else
            RoundDisplayText.text = $"Round: {currentRound} / {NormalModeMaxRounds}";
        */
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

        // PICK MUSIC CLIP
        /*
        AudioClip clipToPlay;
        if (currentRound == 5)
            clipToPlay = gameMusic[4];   // assuming 0‐based
        else if (currentRound == 10)
            clipToPlay = gameMusic[9];
        else
        {
            int idx = Mathf.Clamp(currentRound - 1, 0, gameMusic.Length - 1);
            clipToPlay = gameMusic[idx];
        }

        // PLAY & FADE IN
        musicSource.clip = clipToPlay;
        musicSource.volume = 0f;
        musicSource.loop = true;
        musicSource.Play();
        */
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

}
