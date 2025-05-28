using System;
using UnityEngine;
using UnityEngine.EventSystems;
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
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        
        popSlider.onValueChanged.AddListener(SetPopVolume);
        
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        
        // Add pointer event listeners for Volume settings so that they make a sound
        AddPointerEvents(musicSlider, "music");
        AddPointerEvents(sfxSlider, "sfx");
        AddPointerEvents(popSlider, "pop");
        AddPointerEvents(masterSlider, "master");
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 0.2f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 0.5f);
        masterSlider.value = PlayerPrefs.GetFloat(AudioManager.MASTER_KEY, 0.5f);
        popSlider.value = PlayerPrefs.GetFloat(AudioManager.POP_KEY, 0.5f);
    }
    
    private bool isDraggingSlider = false;
    private bool anySliderBeingDragged;
    private Slider currentDraggingSlider = null;

    private void AddPointerEvents(Slider slider, string sliderName)
    {
        EventTrigger trigger = slider.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = slider.gameObject.AddComponent<EventTrigger>();

        // Pointer Down
        var pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((data) => {
            isDraggingSlider = true;
            currentDraggingSlider = slider;
            AudioManager.instance.PlaySliderLoop(sliderName);
            AudioManager.instance.SetSliderPitch(slider.value);
        });
        trigger.triggers.Add(pointerDown);

        // Pointer Up
        var pointerUp = new EventTrigger.Entry();
        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerUp.callback.AddListener((data) => {
            isDraggingSlider = false;
            currentDraggingSlider = null;
            AudioManager.instance.StopSliderLoop();
        });
        trigger.triggers.Add(pointerUp);
    }
    
    /*
    void Update()
    {
        anySliderBeingDragged = musicSlider.interactable || sfxSlider.interactable || popSlider.interactable || masterSlider.interactable;
        
        if (!anySliderBeingDragged)
        {
            return;
        }
        // Check if any slider is currently being interacted with (dragged)
        bool musicDragged = musicSlider.interactable && EventSystem.current.currentSelectedGameObject == musicSlider.gameObject;
        bool sfxDragged = sfxSlider.interactable && EventSystem.current.currentSelectedGameObject == sfxSlider.gameObject;
        bool popDragged = popSlider.interactable && EventSystem.current.currentSelectedGameObject == popSlider.gameObject;
        bool masterDragged = masterSlider.interactable && EventSystem.current.currentSelectedGameObject == masterSlider.gameObject;

        Slider draggedSlider = null;
        string sliderName = "";

        if (musicDragged) { draggedSlider = musicSlider; sliderName = "music"; }
        else if (sfxDragged) { draggedSlider = sfxSlider; sliderName = "sfx"; }
        else if (popDragged) { draggedSlider = popSlider; sliderName = "pop"; }
        else if (masterDragged) { draggedSlider = masterSlider; sliderName = "master"; }

        if (draggedSlider != null)
        {
            if (!isDraggingSlider || currentDraggingSlider != draggedSlider)
            {
                isDraggingSlider = true;
                currentDraggingSlider = draggedSlider;
                AudioManager.instance.PlaySliderLoop(sliderName);
            }
            AudioManager.instance.SetSliderPitch(draggedSlider.value);
        }
        else if (isDraggingSlider)
        {
            isDraggingSlider = false;
            currentDraggingSlider = null;
            AudioManager.instance.StopSliderLoop();
        }
    }
    */



    void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.MASTER_KEY, masterSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
        
        PlayerPrefs.SetFloat(AudioManager.POP_KEY, popSlider.value);
        
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        popSlider.onValueChanged.AddListener(SetPopVolume);

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
