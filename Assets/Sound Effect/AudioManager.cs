using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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
    
    [Header("Music")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] List<AudioClip> musicClips = new List<AudioClip>();

    

    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";
    public const string MASTER_KEY = "masterVolume";
    
    [SerializeField] private AudioMixer audioMixer;
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
        
        LoadVolume();
    }

    void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);
        float masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        
        audioMixer.SetFloat(VolumeSettings.MIXER_MUSIC,Mathf.Log10(musicVolume) * 20);
        audioMixer.SetFloat(VolumeSettings.MIXER_MUSIC,Mathf.Log10(sfxVolume) * 20);
        
        audioMixer.SetFloat(VolumeSettings.MIXER_MUSIC,Mathf.Log10(masterVolume) * 20);
    }
    
    public void errorSFX()
    {
        playerShootSource.PlayOneShot(errorClip);
    }
    
    //Music
    public void PlayMusic()
    {
        AudioClip clip = musicClips[Random.Range(0, musicClips.Count)];
        musicSource.PlayOneShot(clip);
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
