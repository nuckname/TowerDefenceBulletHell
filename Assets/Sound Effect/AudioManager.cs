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
    [SerializeField] AudioSource errorClipSource;
    [SerializeField] private AudioClip errorClip;

    [Header("Player Shoot")]
    [SerializeField] AudioSource playerShootSource;
    [SerializeField] List<AudioClip> playerShootClips = new List<AudioClip>();

    [Header("Place Turret")]
    [SerializeField] AudioSource placeTurretSource;
    [SerializeField] private AudioClip placeGhostTurretClip;
    [SerializeField] private AudioClip placeRealTurretClip;
    [SerializeField] private AudioClip selectTurretClip;
    [SerializeField] private AudioClip showTurretStatsClip;
    [SerializeField] List<AudioClip> reRollTurretClip = new List<AudioClip>();
    
    [SerializeField] private AudioClip cancelGhostTurretClip;
    
    [Header("Rotate Turret")]
    [SerializeField] AudioSource rotateTurretSource;
    [SerializeField] private AudioClip rotateGhostTurretClip;

    [Header("go back")]
    [SerializeField] private AudioClip goBack;

    [Header("Collect Coin")]
    [SerializeField] AudioSource collectCoinSource;
    [SerializeField] List<AudioClip> collectCoinClips = new List<AudioClip>();
    
    [Header("Player Hit")]
    [SerializeField] AudioSource playerHurtSource;
    [SerializeField] List<AudioClip> playerHurtClips = new List<AudioClip>();
    
    [Header("Pop sound/Enemy Hit")]
    [SerializeField] AudioSource popHitSource;
    [SerializeField] AudioClip popHitClip;
    
    [Header("Music")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] List<AudioClip> musicClips = new List<AudioClip>();
    [SerializeField] AudioClip roundZeroMusicClip;
    
    [Header("Slime Music")]
    [SerializeField] AudioClip slimeBossMusicClip;

    [Header("Snake Music")]
    [SerializeField] List<AudioClip> snakeMusicClips = new List<AudioClip>();
    
    [Header("Music")]
    [SerializeField] AudioSource buyUpgradeSource;
    [SerializeField] List<AudioClip> buyUpgradeClips = new List<AudioClip>();
    
    [Header("Gibberish")]
    [SerializeField] AudioSource gibberishSource;
    [SerializeField] List<AudioClip> gibberishClips = new List<AudioClip>();

    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";
    public const string MASTER_KEY = "masterVolume";
    public const string POP_KEY = "popVolume";
    
    public AudioMixerGroup popGroup;

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
        //popHitSource.outputAudioMixerGroup = popGroup;

    }
    
    //or whatever the user has saved it too.
    private float currentMusicVolume;

    void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);
        float masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float popSound = PlayerPrefs.GetFloat(POP_KEY, 1f);
        
        audioMixer.SetFloat(VolumeSettings.MIXER_MUSIC,Mathf.Log10(musicVolume) * 20);
        
        audioMixer.SetFloat(VolumeSettings.MIXER_SFX,Mathf.Log10(sfxVolume) * 20);
        
        audioMixer.SetFloat(VolumeSettings.MIXER_MASTER,Mathf.Log10(masterVolume) * 20);
        
        audioMixer.SetFloat(VolumeSettings.MIXER_POP,Mathf.Log10(popSound) * 20);
        
        currentMusicVolume = musicVolume;
    }
    
    public void errorSFX()
    {
        errorClipSource.PlayOneShot(errorClip);
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
    public void PlayRandomMusic()
    {
        AudioClip clip = musicClips[Random.Range(0, musicClips.Count)];
        musicSource.PlayOneShot(clip);
    }

    public void SnakeBossMusic(int trackNumber)
    {
        if (trackNumber == 0)
        {
            AudioClip clip = musicClips[0];
            musicSource.PlayOneShot(clip);
        }

        if (trackNumber == 1)
        {
            AudioClip clip = musicClips[1];
            musicSource.PlayOneShot(clip);
        }
    }

    public void RoundZeroMusic()
    {
        musicSource.PlayOneShot(roundZeroMusicClip);

    }

    
    public void SlimeBossMusic()
    {
        musicSource.PlayOneShot(slimeBossMusicClip);
    }
    
    public void FadeOutAndStopMusic()
    {
        float fadeDuration = 1.5f;
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

    public void PauseMusic()
    {
        musicSource.Pause();
    }
    public void ResumeMusic()
    {
        musicSource.UnPause();
    }
    
    private float lastPopTime = 0f;
    private float popCooldown = 0.05f;
    //Enemy
    public void enemyHitSFX()
    {
        if (Time.time - lastPopTime < popCooldown) return;
        
        popHitSource.pitch = Random.Range(0.9f, 1.1f);
        popHitSource.PlayOneShot(popHitClip);
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
    
    public void rotateTurretSFX()
    {
        rotateTurretSource.PlayOneShot(rotateGhostTurretClip);
    }

    
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
