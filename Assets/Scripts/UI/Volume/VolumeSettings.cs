using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider popSlider;
    [SerializeField] private Slider masterSlider;

    public const string MIXER_MASTER = "Master Volume";
    public const string MIXER_MUSIC = "Music Volume";
    public const string MIXER_SFX = "SFX Volume";
    public const string MIXER_POP = "Pop Volume";
    private void Awake()
    {
        //PlayerShooting.disableShooting = true;
        
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        
        popSlider.onValueChanged.AddListener(SetPopVolume);
        
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
        masterSlider.value = PlayerPrefs.GetFloat(AudioManager.MASTER_KEY, 1f);
        popSlider.value = PlayerPrefs.GetFloat(AudioManager.POP_KEY, 1f);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.MASTER_KEY, masterSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
        PlayerPrefs.SetFloat(AudioManager.POP_KEY, popSlider.value);
    }
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat(MIXER_MASTER, Mathf.Log10(volume) * 20);
    }
    
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume) * 20);
    }
    
    public void SetPopVolume(float volume)
    {
        audioMixer.SetFloat(MIXER_POP, Mathf.Log10(volume) * 20);
    }
    
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(MIXER_SFX, Mathf.Log10(volume) * 20);
    }

    public void Resume()
    {
        PlayerShooting.disableShooting = false;
    }
}
