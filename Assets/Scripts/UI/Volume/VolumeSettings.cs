using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider masterSlider;

    const string MIXER_MASTER = "Master Volume";
    const string MIXER_MUSIC = "Music Volume";
    const string MIXER_SFX = "SFX Volume";
    private void Awake()
    {
        //PlayerShooting.disableShooting = true;
        
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat(MIXER_MASTER, Mathf.Log10(volume) * 20);
    }
    
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume) * 20);
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
