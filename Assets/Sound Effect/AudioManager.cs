using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Error")]
    [SerializeField] private AudioClip errorClip;

    [Header("Player Shoot")]
    [SerializeField] AudioSource playerShootSource;
    [SerializeField] List<AudioClip> playerShootClips = new List<AudioClip>();

    [Header("Place Turret")]

    [SerializeField] AudioSource placeTurretSource;

    [SerializeField] private AudioClip placeGhostTurretClip;
    [SerializeField] private AudioClip placeRealTurretClip;
    
    [SerializeField] private AudioClip cancelGhostTurret;

    [Header("Collect Coin")]
    [SerializeField] AudioSource collectCoinSource;
    [SerializeField] List<AudioClip> collectCoinClips = new List<AudioClip>();
    
    [Header("Player Hit")]
    [SerializeField] AudioSource playerHurtSource;
    [SerializeField] List<AudioClip> playerHurtClips = new List<AudioClip>();
    
    [Header("Enemy Hit")]
    [SerializeField] AudioSource enemyHitSource;
    [SerializeField] AudioClip enemyHitClips;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void errorSFX()
    {
        playerShootSource.PlayOneShot(errorClip);
    }
    
    //Enemy
    public void enemyHitSFX()
    {
        enemyHitSource.PlayOneShot(enemyHitClips);
    }
    
    //Player
    
    public void PlayerHurtSFX()
    {
        AudioClip clip = playerHurtClips[Random.Range(0, playerHurtClips.Count)];
        playerHurtSource.PlayOneShot(clip);
    }
    
    public void PlayerShootSFX()
    {
        AudioClip clip = playerShootClips[Random.Range(0,playerShootClips.Count)];
        playerShootSource.PlayOneShot(clip);
    }
    
    //Money
    
    public void PlayerCollectCoinSFX()
    {
        AudioClip clip = collectCoinClips[Random.Range(0,collectCoinClips.Count)];
        collectCoinSource.PlayOneShot(clip);
    }
    
    //Place turret
    
    public void PlaceGhostTurretSFX()
    {
        playerShootSource.PlayOneShot(placeGhostTurretClip);
    }
    
    public void PlaceTurretClip()
    {
        playerShootSource.PlayOneShot(placeRealTurretClip);
    }
    
    public void CancelGhostTurretSFX()
    {
        playerShootSource.PlayOneShot(cancelGhostTurret);
    }
}
