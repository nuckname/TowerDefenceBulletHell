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

    [Header("Music")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip[] gameMusic = new AudioClip[10];
    [SerializeField] private float maxMusicVolume = 1f;
    [SerializeField] private float musicFadeDuration = 1f;
    
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
        //Sets global variable. 
        currentRound = currentRoundIndex;
        
        DisplayRoundUi(currentRound);
        
        enemyOnMapCounter.MaxEnemiesOnMap = spawnEnemies.SpawnEnemiesPerRound(currentRoundIndex);
        
        //Music gets louder. 
    }

    public void DestroyAllPlayerBullets()
    {
        GameObject[] allBullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
        foreach (GameObject bullet in allBullets)
        {
            Destroy(bullet);
        }
    }
    
    

}
