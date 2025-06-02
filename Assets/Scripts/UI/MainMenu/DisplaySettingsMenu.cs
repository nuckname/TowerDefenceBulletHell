using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DisplaySettingsMenu : MonoBehaviour
{
    public TMP_Dropdown rezDropdown;
    public Toggle _rezToggle;


    Resolution[] allResolutions;
    bool isFullScreen;
    int selectedResolutionIndex;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _rezToggle = GameObject.FindGameObjectWithTag("Toggle").GetComponent<Toggle>();
    }

    void Start()
    {
        isFullScreen = true;
        allResolutions = Screen.resolutions;
        
        List<string> resoltuinStringList = new List<string>();
        foreach (Resolution resolution in allResolutions)
        {
            resoltuinStringList.Add(resolution.ToString());
        }
        
        rezDropdown.AddOptions(resoltuinStringList);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
