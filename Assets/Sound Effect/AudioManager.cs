using System;
using System.Collections;
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
    [SerializeField] private AudioClip selectTurretClip;
    [SerializeField] private AudioClip cancelGhostTurretClip;
    [SerializeField] private AudioClip showTurretStatsClip;
    [SerializeField] List<AudioClip> reRollTurretClip = new List<AudioClip>();
    
    [SerializeField] private AudioClip goBack;

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

    [Header("Music")]
    [SerializeField] AudioSource buyUpgradeSource;
    [SerializeField] List<AudioClip> buyUpgradeClips = new List<AudioClip>();
    
    [Header("Gibberish")]
    [SerializeField] AudioSource gibberishSource;
    [SerializeField] List<AudioClip> gibberishClips = new List<AudioClip>();

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
    
    //or whatever the user has saved it too.
    private float currentMusicVolume;

    void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);
        float masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        
        audioMixer.SetFloat(VolumeSettings.MIXER_MUSIC,Mathf.Log10(musicVolume) * 20);
        audioMixer.SetFloat(VolumeSettings.MIXER_MUSIC,Mathf.Log10(sfxVolume) * 20);
        
        audioMixer.SetFloat(VolumeSettings.MIXER_MUSIC,Mathf.Log10(masterVolume) * 20);
        
        currentMusicVolume = musicVolume;
    }
    
    public void errorSFX()
    {
        playerShootSource.PlayOneShot(errorClip);
    }
    
    public void backSFX()
    {
        placeTurretSource.PlayOneShot(goBack);
    }
    
    public void GibberishSFX()
    {
        AudioClip clip = gibberishClips[Random.Range(0, gibberishClips.Count)];
        gibberishSource.PlayOneShot(clip);
    }
    
    public void BuyTurretUpgradeSFX()
    {
        AudioClip clip = buyUpgradeClips[Random.Range(0, buyUpgradeClips.Count)];
        buyUpgradeSource.PlayOneShot(clip);
    }
    
    //Music
    public void PlayMusic()
    {
        AudioClip clip = musicClips[Random.Range(0, musicClips.Count)];
        musicSource.PlayOneShot(clip);
    }
    
    public void FadeOutAndStopMusic(float fadeDuration = 1.5f)
    {
        StartCoroutine(FadeOutCoroutine(fadeDuration));
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        float startVolume = musicSource.volume;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume; // Reset for future use
    }

    
    public void StopMusic()
    {
        musicSource.Stop();
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
    
    public void TurretStatsButtonSFX()
    {
        placeTurretSource.PlayOneShot(showTurretStatsClip);
    }
    
    //Place turret
    
    
    public void PlaceGhostTurretSFX()
    {
        playerShootSource.PlayOneShot(placeGhostTurretClip);
    }
    
    public void SelectTurretSFX()
    {
        placeTurretSource.PlayOneShot(selectTurretClip);
    }
    
    public void RerollTurretSFX()
    {
        AudioClip clip = reRollTurretClip[Random.Range(0,reRollTurretClip.Count)];
        placeTurretSource.PlayOneShot(clip);
    }
    
    public void PlaceTurretClip()
    {
        playerShootSource.PlayOneShot(placeRealTurretClip);
    }
    
    public void CancelGhostTurretSFX()
    {
        playerShootSource.PlayOneShot(cancelGhostTurretClip);
    }
}
