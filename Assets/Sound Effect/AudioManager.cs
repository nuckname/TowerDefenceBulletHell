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
    private int lastMusicClipIndex = -1; // -1 means no clip played yet
    [SerializeField] AudioSource musicSource;
    [SerializeField] List<AudioClip> musicClips = new List<AudioClip>();
    [SerializeField] List<AudioClip> musicAfterRound5Clips = new List<AudioClip>();
    [SerializeField] AudioClip roundZeroMusicClip;
    
    [SerializeField] AudioClip preSlimeBossMusicClip;
    [SerializeField] AudioClip preSnakeBossMusicClip;
    
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
    
    [Header("Slider sounds")]
    [SerializeField] private AudioSource sliderLoopSource;
    [SerializeField] private AudioClip masterSliderLoopClip;
    [SerializeField] private AudioClip musicSliderLoopClip;
    [SerializeField] private AudioClip sfxSliderLoopClip;
    [SerializeField] private AudioClip popSliderLoopClip;

    
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
    public void PlayRandomMusic(int currentRound)
    {
        if (musicClips.Count == 0) return;



        if (currentRound <= 4)
        {
            int newIndex;

            // Keep picking a random index until it's different from lastMusicClipIndex
            do
            {
                newIndex = Random.Range(0, musicClips.Count);
            } 
            while (musicClips.Count > 1 && newIndex == lastMusicClipIndex);
        
            lastMusicClipIndex = newIndex;
            
            //play pinao 
            AudioClip clip = musicClips[newIndex];
            FadeToMusic(clip, 2f);
            //musicSource.PlayOneShot(clip);
        }
        else
        {
            int newIndex;

            // Keep picking a random index until it's different from lastMusicClipIndex
            do
            {
                newIndex = Random.Range(0, musicAfterRound5Clips.Count);
            } 
            while (musicClips.Count > 1 && newIndex == lastMusicClipIndex);
        
            lastMusicClipIndex = newIndex;
            
            AudioClip clip = musicAfterRound5Clips[newIndex];
            FadeToMusic(clip, 2f);
            //musicSource.PlayOneShot(clip);
        }
    }
    
    public void PreSlimeBossMusic()
    {
        musicSource.PlayOneShot(preSlimeBossMusicClip);
    }
    
    public void PreSnakeBossMusic()
    {
        musicSource.PlayOneShot(preSnakeBossMusicClip);
    }


    public void SnakeBossMusic(int trackNumber)
    {
        if (trackNumber == 0)
        {
            AudioClip clip = snakeMusicClips[0];
            musicSource.PlayOneShot(clip);
        }

        if (trackNumber == 1)
        {
            AudioClip clip = snakeMusicClips[1];
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

    public float pitch;
    public void PlayerLostCoinSFX()
    {
        AudioClip clip = collectCoinClips[Random.Range(0, collectCoinClips.Count)];
        collectCoinSource.pitch = pitch; 
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
    
    //Slider
    public void SetSliderPitch(float value)
    {
        float pitch = Mathf.Lerp(0.8f, 1.2f, Mathf.Clamp01(value));
        sliderLoopSource.pitch = pitch;
    }
    
    public void PlaySliderLoop(string sliderName)
    {
        if (sliderLoopSource.isPlaying && sliderLoopSource.clip != GetSliderLoopClip(sliderName)) return;

        sliderLoopSource.clip = GetSliderLoopClip(sliderName);
        //sliderLoopSource.loop = true;
        sliderLoopSource.Play();
    }

    public void StopSliderLoop()
    {
        sliderLoopSource.Stop();
    }

    private AudioClip GetSliderLoopClip(string sliderName)
    {
        switch (sliderName)
        {
            case "music":
                return musicSliderLoopClip;
            case "sfx":
                return sfxSliderLoopClip;
            case "pop":
                return popSliderLoopClip;
            case "master":
                return masterSliderLoopClip;
            default:
                return null;
        }
    }
    
    //Fade music
    public void FadeToMusic(AudioClip newClip, float duration)
    {
        StartCoroutine(FadeToMusicCoroutine(newClip, duration));
    }

    private IEnumerator FadeToMusicCoroutine(AudioClip newClip, float duration)
    {
        if (musicSource.clip == newClip)
            yield break; // Already playing this clip, no fade needed

        float startVolume = musicSource.volume;
        float halfDuration = duration / 2f;

        // Fade out current music
        float elapsed = 0f;
        while (elapsed < halfDuration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / halfDuration);
            yield return null;
        }

        // Switch clip
        musicSource.clip = newClip;
        musicSource.Play();

        // Fade in new music
        elapsed = 0f;
        while (elapsed < halfDuration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0f, startVolume, elapsed / halfDuration);
            yield return null;
        }

        musicSource.volume = startVolume; // Ensure volume is reset exactly
    }
    
    public AudioClip GetSnakeBossMusic(int trackNumber)
    {
        if (trackNumber == 0)
        {
            return snakeMusicClips[0];
        }
        else if (trackNumber == 1)
        {
            return snakeMusicClips[1];
        }
        else
        {
            return null;
        }
    }

    
}
